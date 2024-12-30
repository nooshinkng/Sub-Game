using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Function to start the game
    public void PlayGame()
    {
        Debug.Log("Play button clicked! Loading sub2 scene...");
        SceneManager.LoadScene("sub2"); // Ensure "sub2" is added to Build Settings
    }

    // Function to quit the game
    public void QuitGame()
    {
        Debug.Log("Quit button clicked! Exiting game...");
        Application.Quit(); // This will work only in a built application
    }
}
