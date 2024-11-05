using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private float Move;
    public float jump;
    public bool isJumping;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector3 startPosition;
    public GameManager gameManager; // Reference to the GameManager
    private int jumpCount;
    public int maxJumpCount = 2;

    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip jumpSound2;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip deathByEnemySound;
    [SerializeField] private AudioClip deathDoorSound;
    [SerializeField] private AudioClip hackerSound;
    [SerializeField] private AudioClip finishSound;
    [SerializeField] private AudioClip runningSound; // Running sound

    private AudioSource audioSource; // AudioSource for the running sound

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        jumpCount = 0;

        gameManager = FindObjectOfType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }

        // Set up the AudioSource component for the running sound
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = runningSound;
        audioSource.loop = true; // Loop the running sound
        audioSource.playOnAwake = false;
        audioSource.volume = 0.1f;
    }

    void Update()
    {
        Move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * Move, rb.velocity.y);

        // Set animator parameters
        animator.SetBool("isRunning", Mathf.Abs(Move) > 0.1f);
        animator.SetFloat("horizontalSpeed", Move);

        // Define running state based on input and jump state
        bool isRunning = Mathf.Abs(Move) > 0.1f && !isJumping;

        // Handle running sound logic
        if (isRunning)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        // Jump logic
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumpCount)
        {
            AudioSource.PlayClipAtPoint(jumpSound, transform.position, 1f);

            // Apply jump force and update states
            rb.velocity = new Vector2(rb.velocity.x, jump);
            jumpCount++;
            isJumping = true;
            animator.SetBool("isJumping", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
            isJumping = false;
            animator.SetBool("isJumping", false);
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            jumpCount = 1;
            isJumping = true;
            animator.SetBool("isJumping", true);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            if (rb.velocity.y < 0 && IsLandingOnTop(other))
            {
                Debug.Log("Landed on Enemy: " + other.gameObject.name);
                rb.velocity = new Vector2(rb.velocity.x, jump * 0.5f);
                Destroy(other.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Die();
            AudioSource.PlayClipAtPoint(deathByEnemySound, transform.position, 1f);
        }

        if (other.gameObject.CompareTag("Spike"))
        {
            Die();
            AudioSource.PlayClipAtPoint(deathSound, transform.position, 1f);
        }

        if (other.CompareTag("Hacker"))
        {
            if (gameManager != null)
            {
                gameManager.CollectHacker();
                AudioSource.PlayClipAtPoint(hackerSound, transform.position, 1f);
            }
            else
            {
                Debug.LogError("GameManager reference is missing!");
            }
        }

        if (other.CompareTag("DeathWall"))
        {
            Die();
            AudioSource.PlayClipAtPoint(deathDoorSound, transform.position, 1f);
        }

        if (other.CompareTag("Finish"))
        {
            AudioSource.PlayClipAtPoint(finishSound, transform.position, 1f);
            LevelTimerAndScore levelTimerAndScore = FindObjectOfType<LevelTimerAndScore>();
            if (levelTimerAndScore != null)
            {
                levelTimerAndScore.DisplayCurrentScore();
                levelTimerAndScore.CompleteLevel();
            }
        }
    }

    private bool IsLandingOnTop(Collision2D collision)
    {
        Vector2 playerPosition = transform.position;
        Vector2 enemyPosition = collision.transform.position;

        BoxCollider2D enemyCollider = collision.collider as BoxCollider2D;
        if (enemyCollider != null)
        {
            float enemyTopY = enemyPosition.y + enemyCollider.size.y / 2;
            return playerPosition.y < enemyTopY && playerPosition.y > enemyPosition.y;
        }

        return false;
    }

    public void Die()
    {
        transform.position = startPosition;
        animator.SetBool("isJumping", false);
        isJumping = false;
        jumpCount = 0;
        Debug.Log("You Died!!!");
    }
}