using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    // Reference to level buttons
    public Button[] levelButtons;
    public TMP_Text menuTitle;

    private void Start()
    {
        // Initialize the level buttons based on the player’s progress
        InitializeLevelButtons();
    }

    private void InitializeLevelButtons()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            // Enable the button if the level is unlocked
            if (PlayerPrefs.GetInt("Level_" + (i + 1) + "_Unlocked", i == 0 ? 1 : 0) == 1)
            {
                levelButtons[i].interactable = true;
            }
            else
            {
                levelButtons[i].interactable = false;
            }

            // Set button text (optional if you want the buttons numbered)
            //levelButtons[i].GetComponentInChildren<TMP_Text>().text = "Level " + (i + 1);
        }

        // Set the main menu title text (optional)
        if (menuTitle != null)
        {
            menuTitle.text = "Select Level";
        }
    }

    // Call this method when a level button is clicked
    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene("Level_" + levelIndex);
    }

    // This method can be called from a level’s finish line or GameManager when the level is completed
    public void UnlockNextLevel(int levelIndex)
    {
        if (levelIndex < levelButtons.Length)
        {
            // Unlock the next level and save it
            PlayerPrefs.SetInt("Level_" + (levelIndex + 1) + "_Unlocked", 1);
            PlayerPrefs.Save();
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}