using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject enemyPrefab;  // Prefab del nemico che vuoi spawnare
    public Transform spawnParent;   // Se desideri che il prefab venga spawnato come figlio di un oggetto

    // Metodo pubblico per far spawnare un prefab con un delay
    public void RespawnPrefab(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        // Chiama la coroutine che gestisce il respawn con il delay
        StartCoroutine(RespawnCoroutine(spawnPosition, spawnRotation));
    }

    // Coroutine che esegue il respawn dopo un delay di 5 secondi
    private System.Collections.IEnumerator RespawnCoroutine(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        // Attendi 5 secondi prima di spawnare il prefab
        yield return new WaitForSeconds(5f);

        // Istanzia il prefab del nemico alla posizione specificata con la stessa rotazione
        if (enemyPrefab != null)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, spawnRotation);

            // Se c'è un oggetto "spawnParent", rendilo il genitore del nuovo nemico
            if (spawnParent != null)
            {
                newEnemy.transform.SetParent(spawnParent);
            }

            Debug.Log("Prefab spawned at position: " + spawnPosition + " with rotation: " + spawnRotation);
        }
        else
        {
            Debug.LogError("Enemy prefab not assigned in Respawn script!");
        }
    }
}
