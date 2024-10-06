using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;           // Velocità di movimento del nemico
    public float attackRange = 2.5f;       // Distanza alla quale il nemico può attaccare
    public float attackInterval = 1f;      // Intervallo tra un attacco e l'altro
    public float knockbackForce = 5f;      // Forza del knockback
    public int attackDamage = 10;          // Danno inflitto al giocatore

    private Transform player;               // Riferimento al giocatore
    private bool isAttacking = false;       // Controlla se il nemico sta già attaccando

    private PlayerHealth playerHealth;      // Riferimento alla salute del giocatore
    private Rigidbody2D playerRb;           // Riferimento al Rigidbody2D del giocatore

    void Start()
    {
        // Trova l'oggetto con il tag "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();  // Ottieni il componente PlayerHealth
        playerRb = player.GetComponent<Rigidbody2D>();       // Ottieni il componente Rigidbody2D del giocatore
    }

    void Update()
    {
        if (player != null)
        {
            // Calcola la distanza tra il nemico e il giocatore
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Se il nemico è fuori dal raggio di attacco, avvicinati
            if (distanceToPlayer > attackRange)
            {
                MoveTowardsPlayer();
            }
            // Se il nemico è nel raggio di attacco, inizia ad attaccare
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
        if (playerHealth != null && playerRb != null)
        {
            playerHealth.TakeDamage(attackDamage);  // Infliggi danno al giocatore

            // Calcola la direzione del knockback (dall'attaccante verso il giocatore)
            Vector2 knockbackDirection = (player.position - transform.position).normalized;

            // Applica il knockback al giocatore
            playerRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }

        // Attende l'intervallo di attacco prima di poter attaccare di nuovo
        yield return new WaitForSeconds(attackInterval);

        isAttacking = false;
    }
}
