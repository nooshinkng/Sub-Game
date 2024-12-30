using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu; // Reference to the Pause Menu UI

    void Start()
    {
        // Ensure the pause menu is hidden at the start
        pauseMenu.SetActive(false);
    }

    // Pauses the game and shows the menu
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0; // Freeze the game
    }

    // Resumes the game and hides the menu
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1; // Unfreeze the game
    }

    // Restarts the current scene
    public void Restart()
    {
        Time.timeScale = 1; // Ensure time is running before restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload current scene
    }

    // Loads the main menu or a specific scene (optional)
    public void Home()
{
    Debug.Log("Home button clicked!"); // Debug message to confirm button press
    Time.timeScale = 1; // Ensure time is unfrozen
    SceneManager.LoadScene("Main Menu"); // Load the scene with the exact name
}

}
