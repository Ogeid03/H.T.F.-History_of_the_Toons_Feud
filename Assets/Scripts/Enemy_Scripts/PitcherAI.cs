using System.Collections;
using UnityEngine;

public class EnemyLauncherAI : MonoBehaviour
{
    public float attackRange = 5f;            // Distanza alla quale il nemico può lanciare
    public float attackInterval = 2f;         // Intervallo tra i lanci
    public GameObject projectilePrefab;       // Prefab del proiettile
    public float projectileSpeed = 10f;       // Velocità del proiettile

    public Transform launchPoint;             // Punto di lancio per il proiettile

    private Transform player;                 // Riferimento al giocatore
    private bool facingRight = true;          // Controlla la direzione in cui il nemico sta affrontando

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

            // Flip del nemico in base alla posizione del giocatore
            Flip((player.position - transform.position).x);
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

    private void Launch()
    {
        Debug.Log("Creazione proiettile..."); // Log per confermare la creazione

        // Crea un nuovo proiettile alla posizione del punto di lancio
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);

        if (projectile != null)
        {
            Debug.Log("Proiettile creato!"); // Log di successo

            // Scala il proiettile di 8 volte
            projectile.transform.localScale *= 8;
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
        }
        else
        {
            Debug.LogError("Errore: Rigidbody2D mancante nel proiettile!");
        }
    }

    private void Flip(float direction)
    {
        // Flip del nemico in base alla direzione
        if (direction < 0 && !facingRight)
        {
            // Ruota di 180 gradi sull'asse Z
            transform.Rotate(0f, 180f, 0f);
            facingRight = true;
        }
        else if (direction > 0 && facingRight)
        {
            // Ruota di 180 gradi sull'asse Z
            transform.Rotate(0f, 180f, 0f);
            facingRight = false;
        }
    }

    // Funzione di ritirata disabilitata come richiesto
    /*
    private IEnumerator RetreatAndAttack()
    {
        isRetreating = true;
        Vector2 direction = (transform.position - player.position).normalized;
        float retreatStartTime = Time.time;

        while (Time.time < retreatStartTime + retreatDuration)
        {
            transform.position = Vector2.MoveTowards(transform.position, transform.position + (Vector3)direction, retreatSpeed * Time.deltaTime);
            Flip(direction.x);
            yield return null;
        }

        Launch();
        isRetreating = false;
    }
    */
}