using UnityEngine;

public class GroupMovement : MonoBehaviour
{
    public GameObject emperorAngelfishPrefab; // Drag your EmperorAngelfish prefab here
    public int numberOfFish = 10; // Number of fish to spawn
    public Vector3 spawnArea = new Vector3(10, 5, 10); // Size of the spawn area
    public float speed = 2f; // Speed of fish movement

    private class FishData
    {
        public GameObject fishObject; // Reference to the fish GameObject
        public bool moveLeft; // Whether the fish moves left
        public bool moveRight; // Whether the fish moves right
    }

    private FishData[] fishArray;

    void Start()
    {
        SpawnRandomFish();
    }

    void Update()
    {
        MoveFish(); // Call the movement logic every frame
    }

    void SpawnRandomFish()
    {
        if (emperorAngelfishPrefab == null)
        {
            Debug.LogError("No EmperorAngelfish prefab assigned to the GroupMovement script.");
            return;
        }

        // Initialize the fish data array
        fishArray = new FishData[numberOfFish];

        for (int i = 0; i < numberOfFish; i++)
        {
            // Generate a random position within the spawn area
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
                Random.Range(-spawnArea.y / 2, spawnArea.y / 2),
                Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
            );

            // Instantiate a fish at the random position
            GameObject fish = Instantiate(emperorAngelfishPrefab, transform.position + randomPosition, Quaternion.identity);

            // Decide if the fish should move left or right
            bool moveLeft = Random.value > 0.5f; // Randomly decide direction
            bool moveRight = !moveLeft; // Opposite of moveLeft

            if (moveRight)
            {
                fish.transform.rotation = Quaternion.Euler(0, 180f, 0); // Rotate to face left
            }
            if (moveLeft)
            {
                fish.transform.rotation = Quaternion.Euler(0, 0f, 0); // Rotate to face right
            }

            // Save the fish and its movement direction in the array
            fishArray[i] = new FishData
            {
                fishObject = fish,
                moveLeft = moveLeft,
                moveRight = moveRight
            };

            // Parent the fish to this group for organization
            fish.transform.SetParent(transform);
        }
    }

    void MoveFish()
    {
        foreach (var fishData in fishArray)
        {
            if (fishData.fishObject != null) // Ensure the fish exists
            {
                // Move fish based on its direction
                if (fishData.moveRight)
                {
                    fishData.fishObject.transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
                }
                else if (fishData.moveLeft)
                {
                    fishData.fishObject.transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
                }
            }
        }
    }
}
