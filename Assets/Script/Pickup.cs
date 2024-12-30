using UnityEngine;
using TMPro;

public class Pickup : MonoBehaviour
{
    public TextMeshProUGUI coinText; // Drag and drop your TextMeshPro UI element here
    public AudioClip pickupSound; // Drag and drop the coin pickup sound here
    private AudioSource audioSource; // Audio source for playing sound
    private int _coinCount = 0;

    public int CoinCount
    {
        get { return _coinCount; }
        private set { _coinCount = value; }
    }

    void Start()
    {
        // Add an AudioSource component if not already attached
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; // Ensure the sound doesn't play automatically
        UpdateCoinUI(); // Initialize the UI with the current coin count

        Debug.Log("Pickup script initialized. Starting coin count: " + CoinCount);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin")) // Check if the object is a coin
        {
            CoinCount++; // Increment the coin count
            Debug.Log("Coin picked up! New coin count: " + CoinCount);

            UpdateCoinUI(); // Update the UI text
            PlayPickupSound(); // Play the pickup sound
            Destroy(other.gameObject); // Destroy the coin
        }
        else
        {
            Debug.Log("Triggered with non-coin object: " + other.gameObject.name);
        }
    }

    void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + CoinCount; // Display the current coin count
            Debug.Log("Coin UI updated: " + coinText.text);
        }
        else
        {
            Debug.LogWarning("Coin Text UI element is not assigned in the inspector!");
        }
    }

    void PlayPickupSound()
    {
        if (pickupSound != null) // Check if the sound is assigned
        {
            audioSource.clip = pickupSound; // Set the clip
            audioSource.Play(); // Play the sound
            Debug.Log("Playing pickup sound: " + pickupSound.name);
        }
        else
        {
            Debug.LogWarning("No pickup sound assigned in the inspector!");
        }
    }
}