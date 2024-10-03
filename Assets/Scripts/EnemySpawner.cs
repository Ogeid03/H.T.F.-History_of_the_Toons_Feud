using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;   // Array di prefabs dei nemici
    public int numberOfEnemies = 5;     // Numero di nemici da spawnare
    public float spawnRadius = 5f;      // Raggio in cui spawnare i nemici
    public float spawnDelay = 1f;       // Ritardo tra ogni spawn

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Scegli un tipo di nemico casuale dall'array dei prefabs
            int randomIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyToSpawn = enemyPrefabs[randomIndex];

            // Genera una posizione casuale all'interno di un cerchio di raggio `spawnRadius`
            Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;

            // Instanzia il nemico alla posizione calcolata
            Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);

            // Attende un breve ritardo prima di spawnare il prossimo nemico
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
