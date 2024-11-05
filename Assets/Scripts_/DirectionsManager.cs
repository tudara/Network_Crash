using UnityEngine;
using UnityEngine.SceneManagement;

public class DirectionsManager : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main_Menu"); // Change this to the name of your main menu scene
   }
}

