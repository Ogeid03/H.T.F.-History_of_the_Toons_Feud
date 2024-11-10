using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleManager : MonoBehaviour
{
    public GameObject bossProjectilePrefab;       // Prefab del colpo del boss
    public List<Transform> projectileSpawnPoints;  // Lista di punti di spawn per i colpi del boss
    public float attackInterval = 3f;              // Intervallo di tempo tra i colpi
    public float projectileFallSpeed = 10f;        // Velocità di caduta dei colpi

    public List<Transform> spawnPoints;            // Lista di punti di spawn per i nemici
    public List<GameObject> enemyPrefabs;          // Lista di prefab dei nemici
    public float spawnInterval = 10f;              // Intervallo di tempo per spawnare i nemici

    private bool isBattleActive = false;           // Stato della battaglia

    public void StartBossBattle()
    {
        isBattleActive = true;
        StartCoroutine(BossAttackRoutine());
        StartCoroutine(EnemySpawnRoutine());
    }

    private IEnumerator BossAttackRoutine()
    {
        while (isBattleActive)
        {
            // Istanziamo un proiettile da ogni punto di spawn con un lieve ritardo tra un colpo e l'altro
            foreach (Transform spawnPoint in projectileSpawnPoints)
            {
                // Aggiungi un piccolo ritardo casuale tra i colpi
                float randomDelay = Random.Range(0f, 0.5f); // Ritardo casuale tra 0 e 0.5 secondi (puoi modificare la durata)
                yield return new WaitForSeconds(randomDelay);

                GameObject projectile = Instantiate(bossProjectilePrefab, spawnPoint.position, Quaternion.identity);
                Rigidbody rb = projectile.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    // Imposta la velocità di caduta verso il basso
                    rb.velocity = Vector3.down * projectileFallSpeed;
                }

                // Imposta la scala del proiettile per renderlo molto più grande
                projectile.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f); // Tre volte più grande (esempio)
            }

            // Aspetta prima del prossimo attacco
            yield return new WaitForSeconds(attackInterval);
        }
    }


    private IEnumerator EnemySpawnRoutine()
    {
        while (isBattleActive)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
                Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            }

            yield return new WaitForSeconds(spawnInterval); // Aspetta prima di spawnare nuovi nemici
        }
    }

    public void StopBossBattle()
    {
        isBattleActive = false;
    }
}
