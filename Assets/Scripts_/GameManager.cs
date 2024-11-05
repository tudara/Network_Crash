using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int totalHackers = 3;
    private int hackersCollected = 0;
    public GameObject firewall;

    public GameObject pauseMenuUI;
    private bool isPaused = false;

    public GameObject player;
    private LevelTimerAndScore levelTimerAndScore;
    private int deaths = 0;
    private int fart = 1;
    [SerializeField] private AudioClip pauseSound;
    [SerializeField] private AudioClip hackerSound;
    [SerializeField] private AudioClip finishSound;
    [SerializeField] private AudioClip resumeSound;
    [SerializeField] private AudioClip exitSound;
    [SerializeField] private AudioClip mainMenuSound;

    void Start()
    {
        pauseMenuUI.SetActive(false);

        levelTimerAndScore = FindObjectOfType<LevelTimerAndScore>();
        if (levelTimerAndScore == null)
        {
            Debug.LogError("LevelTimerAndScore script not found in the scene!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void CollectHacker()
    {
        hackersCollected++;
        if (levelTimerAndScore != null)
        {
            levelTimerAndScore.AddHacker();
        }

        if (hackersCollected >= totalHackers)
        {
            DisableFirewall();
        }
    }

    private void DisableFirewall()
    {
        firewall.SetActive(false);
        AudioSource.PlayClipAtPoint(hackerSound, transform.position, 1f);
        Debug.Log("All hackers collected, firewall disabled");
    }

    public void PlayerDied()
    {
        deaths++;
        if (levelTimerAndScore != null)
        {
            levelTimerAndScore.AddDeath();
        }
    }

    public void Resume()
    {

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        if (levelTimerAndScore != null)
        {
            AudioSource.PlayClipAtPoint(resumeSound, transform.position, 1f);
            levelTimerAndScore.ResumeTimer();
        }
    }

    public void Pause()
    {
        AudioSource.PlayClipAtPoint(pauseSound, transform.position, 1f);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        if (levelTimerAndScore != null)
        {
            levelTimerAndScore.PauseTimer();
        }
    }

    public void LoadMainMenu()
    {

        Vector3 playerPosition = player.transform.position;
        PlayerPrefs.SetFloat("PlayerPosX", playerPosition.x);
        PlayerPrefs.SetFloat("PlayerPosY", playerPosition.y);
        PlayerPrefs.SetFloat("PlayerPosZ", playerPosition.z);

        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
        Time.timeScale = 1f;
        if (fart == 1)
        {
            SceneManager.LoadScene("Main_Menu");
            AudioSource.PlayClipAtPoint(mainMenuSound, transform.position, 1f);
        }
    }

    public void CompleteLevel()
    {

        if (levelTimerAndScore != null)
        {
            AudioSource.PlayClipAtPoint(finishSound, transform.position, 1f);
            levelTimerAndScore.CompleteLevel();
        }
    }

    public void QuitGame()
    {
        AudioSource.PlayClipAtPoint(finishSound, transform.position, 1f);
        Application.Quit();
    }



}