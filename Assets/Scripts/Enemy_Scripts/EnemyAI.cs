using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 5f;           // Velocit� di movimento del nemico
    public float attackRange = 3f;       // Distanza alla quale il nemico pu� attaccare
    public float attackInterval = 1f;      // Intervallo tra un attacco e l'altro
    public float knockbackForce = 5f;      // Forza del knockback
    public int attackDamage = 10;          // Danno inflitto al giocatore
    public float startDelay = 0f;          // Ritardo prima che il nemico possa muoversi

    private Transform player;              // Riferimento al giocatore
    private bool isAttacking = false;      // Controlla se il nemico sta gi� attaccando
    private bool canMove = false;          // Controlla se il nemico pu� muoversi

    private PlayerHealth playerHealth;     // Riferimento alla salute del giocatore
    private Rigidbody2D playerRb;          // Riferimento al Rigidbody2D del giocatore

    void Start()
    {
        // Trova l'oggetto con il tag "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();  // Ottieni il componente PlayerHealth
        playerRb = player.GetComponent<Rigidbody2D>();       // Ottieni il componente Rigidbody2D del giocatore

        // Avvia la coroutine che aspetta il ritardo iniziale
        StartCoroutine(StartDelay());
    }

    IEnumerator StartDelay()
    {
        // Aspetta per i secondi specificati (startDelay)
        yield return new WaitForSeconds(startDelay);
        canMove = true; // Dopo il ritardo, permetti al nemico di muoversi
    }

    void Update()
    {
        if (player != null && canMove)  // Controlla se il nemico pu� muoversi
        {
            // Calcola la distanza tra il nemico e il giocatore
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Se il nemico � fuori dal raggio di attacco, avvicinati
            if (distanceToPlayer > attackRange)
            {
                MoveTowardsPlayer();
            }
            // Se il nemico � nel raggio di attacco, inizia ad attaccare
            else if (!isAttacking)
            {
                StartCoroutine(AttackPlayer());
            }
        }
    }

    void MoveTowardsPlayer()
    {
        // Muovi il nemico verso il giocatore
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    IEnumerator AttackPlayer()
    {
        isAttacking = true;

        // Simula l'attacco (ad esempio infliggere danno al giocatore)
        Debug.Log("Attacco al giocatore!");

        // Infliggi danno al giocatore e applica il knockback
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);  // Infliggi danno al giocatore

            // Calcola la direzione del knockback (dall'attaccante verso il giocatore)
            Vector2 knockbackDirection = (player.position - transform.position).normalized;

            // Applica il knockback al giocatore
            playerRb.AddForce(-knockbackDirection * knockbackForce, ForceMode2D.Impulse); // Usare il segno negativo per applicare il knockback nella direzione opposta
        }

        // Attende l'intervallo di attacco prima di poter attaccare di nuovo
        yield return new WaitForSeconds(attackInterval);

        isAttacking = false;
    }

    // Gestione della collisione con il collider di attacco
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Se il nemico entra in contatto con il giocatore
        {
            // Solo se non stai gi� attaccando, chiama AttackPlayer
            if (!isAttacking)
            {
                StartCoroutine(AttackPlayer());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Se il giocatore esce dall'area di attacco
        {
            StopAllCoroutines(); // Ferma gli attacchi
            isAttacking = false; // Ripristina lo stato di attacco
        }
    }
}
