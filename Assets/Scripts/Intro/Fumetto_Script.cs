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

    // Reference to the spawned object
    private GameObject spawnedObject;

    void Start()
    {
        // Find the player by tag
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player object not found. Make sure the player has the tag 'Player'.");
        }

        // Find the canvas named "TopoPensieroView"
        GameObject canvasObject = GameObject.Find("TopoPensieroView");
        if (canvasObject != null)
        {
            guiCanvas = canvasObject.transform;
        }
        else
        {
            Debug.LogError("Canvas named 'TopoPensieroView' not found. Make sure it exists in the scene.");
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
                // Spawn the prefab if it hasn't been spawned yet
                if (spawnedObject == null)
                {
                    spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

                    // Set the spawned object as a child of the canvas
                    spawnedObject.transform.SetParent(guiCanvas, false);
                    Debug.Log("Prefab spawned at " + spawnPosition);
                }

                // Flip the spawned object based on the player's position
                FlipObjectBasedOnPlayer();
            }
            else
            {
                // Destroy the spawned object if it exists and the player is out of range
                if (spawnedObject != null)
                {
                    Destroy(spawnedObject);
                    spawnedObject = null; // Reset the reference
                    Debug.Log("Prefab destroyed");
                }
            }
        }
    }

    void FlipObjectBasedOnPlayer()
    {
        if (spawnedObject != null)
        {
            // Trova il figlio "nuvoletta"
            Transform nuvolettaTransform = spawnedObject.transform.Find("nuvoletta");

            if (nuvolettaTransform != null)
            {
                Debug.Log("Found child 'nuvoletta' in spawnedObject");

                // Ottieni la scala dell'oggetto figlio "nuvoletta"
                Vector3 scale = nuvolettaTransform.localScale;

                // Verifica la posizione del giocatore rispetto all'oggetto
                bool isPlayerToLeft = player.transform.position.x < spawnedObject.transform.position.x;

                // Inverti la scala X in base alla posizione del giocatore
                if (isPlayerToLeft)
                {
                    // Se il giocatore è a sinistra, inverto la scala X (flipping)
                    scale.x = Mathf.Abs(scale.x) * -1;
                }
                else
                {
                    // Se il giocatore è a destra, ripristino la scala originale
                    scale.x = Mathf.Abs(scale.x);
                }

                // Applica la nuova scala
                nuvolettaTransform.localScale = scale;

                // Debug per vedere se il flip è stato applicato correttamente
                Debug.Log($"Flipping nuvoletta: {scale.x} (Player position: {player.transform.position.x}, SpawnedObject position: {spawnedObject.transform.position.x})");
            }
            else
            {
                Debug.LogError("Figlio 'nuvoletta' non trovato nell'oggetto spawnato.");
            }
        }
    }
}
