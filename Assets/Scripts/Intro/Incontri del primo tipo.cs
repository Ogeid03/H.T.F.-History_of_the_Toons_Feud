using UnityEngine;
using UnityEngine.UI; // Per lavorare con i componenti UI come il Button

public class ProjectileSpawner : MonoBehaviour
{
    // Prefab del proiettile
    public GameObject projectilePrefab;

    // Posizione da cui il proiettile verrà lanciato
    public Transform spawnPosition;

    // Il Button che attiverà la funzione
    public Button spawnButton;

    // Riferimento al player
    private Transform playerTransform;

    void Start()
    {
        // Trova il player nella scena (assumiamo che il player abbia il tag "Player")
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (playerTransform == null)
        {
            Debug.LogError("Player non trovato! Assicurati che il giocatore abbia il tag 'Player'.");
        }

        // Verifica se il bottone è stato assegnato
        if (spawnButton != null)
        {
            // Aggiungi un listener al bottone che chiama il metodo SpawnProjectile() quando viene cliccato
            spawnButton.onClick.AddListener(SpawnProjectile);
        }
        else
        {
            Debug.LogError("Il bottone non è stato assegnato!");
        }
    }

    void SpawnProjectile()
    {
        // Se il prefab del proiettile è stato assegnato
        if (projectilePrefab != null && spawnPosition != null)
        {
            // Crea il proiettile alla posizione di spawn
            GameObject projectile = Instantiate(projectilePrefab, spawnPosition.position, Quaternion.identity);

            // Scala il proiettile di 7 volte
            projectile.transform.localScale *= 7f;

            // Disabilita la gravità sul Rigidbody2D
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 0f; // Disabilita la gravità

                // Calcola la direzione verso il player
                if (playerTransform != null)
                {
                    Vector3 direction = (playerTransform.position - spawnPosition.position).normalized;

                    // Imposta la velocità del proiettile verso il player
                    rb.velocity = direction * 300f; // Cambia 10f con la velocità desiderata
                }
                else
                {
                    Debug.LogWarning("Il player non è stato trovato.");
                }
            }
            else
            {
                Debug.LogWarning("Il proiettile non ha un Rigidbody2D per il movimento.");
            }

            // Disabilita l'oggetto a cui è associato questo script (l'oggetto che possiede il pulsante)
            gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Prefab del proiettile o posizione di spawn non assegnati.");
        }
    }
}
