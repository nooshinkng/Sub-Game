using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartFinishUIHandler : MonoBehaviour
{
    public GameObject uiPanel;         // The UI Panel that will be shown after crash or finish
    public Button tryAgainButton;      // The Try Again button
    public AudioClip finishSound;      // Finish sound to play
    public AudioClip startSound;       // Start sound to play

    private AudioSource audioSource;   // AudioSource for playing sounds

    void Start()
    {
        // Initialize audio source
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Hide the UI at the start
        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("UI Panel is not assigned in the Inspector!");
        }

        // Set up Try Again button listener
        if (tryAgainButton != null)
        {
            tryAgainButton.onClick.AddListener(ReloadScene);
        }
        else
        {
            Debug.LogError("Try Again Button is not assigned in the Inspector!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player collides with the "Start" object
        if (other.gameObject.CompareTag("Start"))
        {
            Debug.Log("Game Started: Player reached the Start point.");
            PlaySound(startSound); // Play the start sound
        }

        // Check if the player collides with the "Finish" object
        else if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Game Finished: Showing UI for Try Again...");
            PlaySound(finishSound); // Play the finish sound
            ShowUI(); // Show the UI with Try Again button
        }
    }

    void ShowUI()
    {
        // Show the UI panel and disable the player controls if needed
        if (uiPanel != null)
        {
            uiPanel.SetActive(true);
        }
        Time.timeScale = 0f; // Pause the game while showing the UI
    }

    void ReloadScene()
    {
        // Reload the current scene
        Time.timeScale = 1f; // Unpause the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the scene
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip); // Play the provided sound
        }
    }
}
