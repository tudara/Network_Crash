using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public Button continueButton;
    public Button selectLevelButton;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI startButtonText;

    void Start()
    {
        // Enable the Continue button if there is saved progress
        continueButton.interactable = PlayerPrefs.HasKey("CurrentLevel");
        selectLevelButton.gameObject.SetActive(true);
        titleText.text = "Main Menu";
    }

    public void OnStartGame()
    {
        // Load Level 1 and ensure the Continue button is enabled
        SceneManager.LoadScene("Level_1");
    }

    public void OnContinueGame()
    {
        // Load the saved level if it exists
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            string levelToLoad = PlayerPrefs.GetString("CurrentLevel");
            SceneManager.LoadScene(levelToLoad);
        }
    }

    public void OnSelectLevel()
    {
        SceneManager.LoadScene("Level_Select");
    }

    public void OnDirections()
    {
        SceneManager.LoadScene("Directions");
    }

    public void OnExitGame()
    {
        Application.Quit(); // Quits the game
    }

    public void OnHighScore()
    {
        SceneManager.LoadScene("High_Scores");
    }
}