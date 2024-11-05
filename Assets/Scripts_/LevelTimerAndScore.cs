using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelTimerAndScore : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;
    public float scoreDisplayDuration = 3f;

    private float elapsedTime = 0f;
    private bool timerRunning = true;
    private int hackersCollected = 0;
    private int deaths = 0;
    private int totalScore;
    private string levelName;
    private bool isLevelComplete = false;
    public int timeWeight = 1000;
    public int hackerPenalty = 300;
    public int deathPenalty = 200;
    [SerializeField] private string sceneToLoad;

    void Start()
    {
        elapsedTime = 0f;
        levelName = SceneManager.GetActiveScene().name;
        UpdateTimerDisplay();

        if (scoreText != null) scoreText.gameObject.SetActive(false);
        if (currentScoreText != null) currentScoreText.gameObject.SetActive(true);
        if (highScoreText != null) highScoreText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (timerRunning && !isLevelComplete)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay();
            DisplayCurrentScore();
        }
    }

    public void PauseTimer() => timerRunning = false;

    public void ResumeTimer() => timerRunning = true;

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(elapsedTime / 60F);
            int seconds = Mathf.FloorToInt(elapsedTime % 60F);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void CompleteLevel()
    {
        if (isLevelComplete) return;

        timerRunning = false;
        isLevelComplete = true;
        CalculateScore();

        if (scoreText != null) scoreText.gameObject.SetActive(true);
        if (currentScoreText != null) currentScoreText.gameObject.SetActive(false);

        SaveHighScore();

        StartCoroutine(DisplayHighScoreAndDelayTransition());
    }

    private void CalculateScore()
    {
        totalScore = Mathf.Max(0,
            timeWeight - (int)elapsedTime - (hackersCollected * hackerPenalty) - (deaths * deathPenalty));
        if (scoreText != null) scoreText.text = "Final Score: " + totalScore;
    }

    private void SaveHighScore()
    {
        int highScore = PlayerPrefs.GetInt(levelName + "_HighScore", 0);
        if (totalScore > highScore)
        {
            PlayerPrefs.SetInt(levelName + "_HighScore", totalScore);
            PlayerPrefs.Save();
        }
    }

    public void AddHacker()
    {
        if (!isLevelComplete)
        {
            hackersCollected++;
            DisplayCurrentScore();
        }
    }

    public void AddDeath()
    {
        if (!isLevelComplete)
        {
            deaths++;
            DisplayCurrentScore();
        }
    }

    public void DisplayCurrentScore()
    {
        if (currentScoreText != null)
        {
            CalculateScore();
            currentScoreText.text = "Score: " + totalScore;
        }
    }

    private IEnumerator DisplayHighScoreAndDelayTransition()
    {
        if (highScoreText != null)
        {
            int highScore = PlayerPrefs.GetInt(levelName + "_HighScore", 0);
            highScoreText.text = "High Score: " + highScore;
            highScoreText.gameObject.SetActive(true);
        }

        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        yield return new WaitForSecondsRealtime(scoreDisplayDuration);

        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        LoadNextScene();
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}