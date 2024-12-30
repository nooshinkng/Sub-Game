using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // Reference to the particle system
    public ParticleSystem sprays_2;

    // Reference for crash sound (SFX)
    public AudioClip crashSound; // Drag your crash sound into this field in the Inspector
    public AudioClip spraysSound; // Drag your sprays sound into this field in the Inspector

    private AudioSource audioSource; // General audio source for crash sound
    private AudioSource spraysAudioSource; // Separate audio source for sprays sound

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the particle system is not playing initially
        if (sprays_2 != null)
        {
            sprays_2.Stop();
        }

        // Initialize the AudioSource for the crash sound
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Add an AudioSource dynamically if not present
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configure general audio source settings
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f; // 2D sound for SFX

        // Initialize a separate AudioSource for sprays_2
        spraysAudioSource = gameObject.AddComponent<AudioSource>();
        spraysAudioSource.playOnAwake = false;
        spraysAudioSource.spatialBlend = 0f; // 2D sound for sprays
        spraysAudioSource.loop = true; // Loop the sprays sound while the particle system is active
    }

    // Called when the collider on this GameObject starts touching another collider
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is tagged as "Terrain"
        if (collision.gameObject.CompareTag("Terrain"))
        {
            // Play the particle effect
            if (sprays_2 != null)
            {
                sprays_2.Play();
                PlaySpraysSound();
            }
            else
            {
                Debug.LogWarning("ParticleSystem 'sprays_2' is not assigned!");
            }
        }
        // Check if the collided object is tagged as "Rock"
        else if (collision.gameObject.CompareTag("Rock"))
        {
            // Play the crash sound effect
            PlayCrashSound();

            // Call method to reload the scene
            ReloadLevel();
        }
    }

    // Called when the collider on this GameObject stops touching another collider
    void OnCollisionExit(Collision collision)
    {
        // Check if the object leaving the collision is tagged as "Terrain"
        if (collision.gameObject.CompareTag("Terrain"))
        {
            // Stop the particle effect
            if (sprays_2 != null)
            {
                sprays_2.Stop();
                StopSpraysSound();
            }
        }
    }

    // Method to reload the current scene
    void ReloadLevel()
    {
        // Reload the current active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Method to play the crash sound effect
    void PlayCrashSound()
    {
        if (audioSource != null && crashSound != null)
        {
            // Play the sound effect once
            audioSource.PlayOneShot(crashSound);
        }
        else
        {
            Debug.LogWarning("AudioSource or crashSound is not assigned!");
        }
    }

    // Method to play the sprays sound effect
    void PlaySpraysSound()
    {
        if (spraysAudioSource != null && spraysSound != null)
        {
            // Play the sprays sound (loop enabled)
            spraysAudioSource.clip = spraysSound;
            spraysAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("SpraysAudioSource or spraysSound is not assigned!");
        }
    }

    // Method to stop the sprays sound effect
    void StopSpraysSound()
    {
        if (spraysAudioSource != null && spraysAudioSource.isPlaying)
        {
            spraysAudioSource.Stop();
        }
    }
}
