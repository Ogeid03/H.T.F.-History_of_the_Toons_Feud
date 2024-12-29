using UnityEngine;

public class SpawnOnProximity : MonoBehaviour
{
    // Prefab to spawn
    public GameObject prefabToSpawn;

    // Coordinates where the prefab should be spawned
    public Vector3 spawnPosition;

    // Distance threshold for spawning
    public float triggerDistance = 5.0f;

    // Canvas to parent the spawned prefab
    private Transform guiCanvas;

    private GameObject player;

    void Start()
    {
        // Find the player by tag
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player object not found. Make sure the player has the tag 'Player'.");
        }

        // Find the canvas named "GUI"
        GameObject canvasObject = GameObject.Find("GUI");
        if (canvasObject != null)
        {
            guiCanvas = canvasObject.transform;
        }
        else
        {
            Debug.LogError("Canvas named 'GUI' not found. Make sure it exists in the scene.");
        }
    }

    void Update()
    {
        if (player != null && guiCanvas != null)
        {
            // Calculate distance between this object and the player
            float distance = Vector3.Distance(transform.position, player.transform.position);

            // Check if the distance is within the threshold
            if (distance <= triggerDistance)
            {
                // Spawn the prefab at the specified position
                GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

                // Set the spawned object as a child of the canvas
                spawnedObject.transform.SetParent(guiCanvas, false);

                // Optionally, destroy this script or object after spawning to prevent repeated spawns
                Destroy(this);
            }
        }
    }
}
