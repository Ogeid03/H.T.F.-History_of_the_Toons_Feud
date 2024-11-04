using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleManager : MonoBehaviour
{
    // Variabili per gli attacchi del boss
    public GameObject bossProjectilePrefab;    // Prefab del colpo del boss
    public Transform projectileSpawnPoint;     // Punto di spawn dei colpi del boss
    public float attackInterval = 3f;          // Intervallo di tempo tra i colpi
    public float projectileFallSpeed = 10f;    // Velocità di caduta dei colpi

    // Variabili per la generazione dei nemici
    public List<Transform> spawnPoints;        // Lista di punti di spawn per i nemici
    public List<GameObject> enemyPrefabs;      // Lista di prefab dei nemici
    public float spawnInterval = 10f;          // Intervallo di tempo per spawnare i nemici

    // Controlli interni
    private bool isBattleActive = false;       // Stato della battaglia

    // Metodo per avviare la boss battle
    public void StartBossBattle()
    {
        isBattleActive = true;
        StartCoroutine(BossAttackRoutine());   // Avvia la routine degli attacchi del boss
        StartCoroutine(EnemySpawnRoutine());   // Avvia la routine per spawnare i nemici
    }

    // Routine per far cadere i colpi del boss
    private IEnumerator BossAttackRoutine()
    {
        while (isBattleActive)
        {
            // Crea un colpo dal prefab e lo posiziona nel punto di spawn
            GameObject projectile = Instantiate(bossProjectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

            // Imposta la velocità di caduta verso il basso
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.down * projectileFallSpeed;
            }

            yield return new WaitForSeconds(attackInterval); // Aspetta prima del prossimo attacco
        }
    }

    // Routine per spawnare i nemici dai punti di spawn
    private IEnumerator EnemySpawnRoutine()
    {
        while (isBattleActive)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                // Scegli casualmente un prefab di nemico dalla lista
                GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

                // Instanzia il nemico al punto di spawn
                Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            }

            yield return new WaitForSeconds(spawnInterval); // Aspetta prima di spawnare nuovi nemici
        }
    }

    // Metodo per fermare la boss battle
    public void StopBossBattle()
    {
        isBattleActive = false;
    }
}
