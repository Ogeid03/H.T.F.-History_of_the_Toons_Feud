using System.Collections;
using UnityEngine;

public class EnemyLauncherAI : MonoBehaviour
{
    public float moveSpeed = 3f;              // Velocità di movimento del nemico
    public float attackRange = 5f;            // Distanza alla quale il nemico può lanciare
    public float minDistanceToPlayer = 2f;    // Distanza minima per l'allontanamento
    public float attackInterval = 2f;         // Intervallo tra i lanci
    public GameObject projectilePrefab;        // Prefab del proiettile
    public float projectileSpeed = 10f;        // Velocità del proiettile

    public Transform launchPoint;              // Punto di lancio per il proiettile

    private Transform player;                  // Riferimento al giocatore

    void Start()
    {
        // Trova l'oggetto con il tag "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(LaunchProjectile());
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Se il nemico è troppo vicino, allontanati dal giocatore
            if (distanceToPlayer < minDistanceToPlayer)
            {
                MoveAwayFromPlayer();
            }
            // Se il nemico è troppo lontano, avvicinati al giocatore
            else if (distanceToPlayer > attackRange)
            {
                MoveTowardsPlayer();
            }
        }
    }

    private IEnumerator LaunchProjectile()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackInterval);

            if (player != null)
            {
                float distanceToPlayer = Vector2.Distance(transform.position, player.position);

                if (distanceToPlayer <= attackRange)
                {
                    Launch();
                }
            }
        }
    }

    void MoveTowardsPlayer()
    {
        // Muovi il nemico verso il giocatore
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    void MoveAwayFromPlayer()
    {
        // Muovi il nemico nella direzione opposta al giocatore
        Vector2 direction = (transform.position - player.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, transform.position + (Vector3)direction, moveSpeed * Time.deltaTime);
    }

    private void Launch()
    {
        Debug.Log("Creazione proiettile..."); // Log per confermare la creazione

        // Crea un nuovo proiettile alla posizione del punto di lancio
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);

        // Controlla se il proiettile è stato creato
        if (projectile != null)
        {
            Debug.Log("Proiettile creato!"); // Log di successo
        }
        else
        {
            Debug.LogError("Errore: proiettile non creato!");
        }

        // Calcola la direzione verso il giocatore
        Vector2 direction = (player.position - launchPoint.position).normalized;

        // Aggiungi una forza al proiettile per lanciarlo
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(direction.x * projectileSpeed, direction.y * projectileSpeed);
            rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse); // Modifica il valore a seconda di quanto alto vuoi che vada
        }
        else
        {
            Debug.LogError("Errore: Rigidbody2D mancante nel proiettile!");
        }
    }
}
