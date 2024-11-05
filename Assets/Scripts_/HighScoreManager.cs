using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HighScoreManager : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    [SerializeField] private AudioClip buttonClickSound;

    // Array of level names
    private string[] levelNames = { "Level_1 - Basics", "Level_2 - Submarine", "Level_3 - " + 
                                    "Corporate HQ", "Level_4 - AirForce 1" };

    void Start()
    {
        DisplayHighScores();
    }

    private void DisplayHighScores()
    {
        string highScores = "";

        // Loop through each level name and retrieve the high score
        foreach (string levelName in levelNames)
        {
            int highScore = PlayerPrefs.GetInt(levelName + "_HighScore", 0);
            highScores += levelName + " : " + highScore + "\n";
        }

        highScoreText.text = highScores;
    }

    public void MainMenu()
    {
        AudioSource.PlayClipAtPoint(buttonClickSound, transform.position, 2f); 
        SceneManager.LoadScene("Main_Menu");

    }
}

