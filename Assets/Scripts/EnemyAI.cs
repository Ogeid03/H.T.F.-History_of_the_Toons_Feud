using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;        // Velocità di movimento del nemico
    public float attackRange = 1.5f;    // Distanza alla quale il nemico può attaccare
    public float attackInterval = 1f;    // Intervallo tra un attacco e l'altro
    public LayerMask groundLayer;       // Layer del terreno
    private Transform player;            // Riferimento al giocatore
    private bool isAttacking = false;    // Controlla se il nemico sta già attaccando
    public int health = 50;              // Salute del nemico
    public int damageToPlayer = 10;      // Danno inflitto al giocatore

    private PlayerHealth playerHealth;    // Riferimento alla salute del giocatore

    void Start()
    {
        // Trova l'oggetto con il tag "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>(); // Ottieni il componente PlayerHealth
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

        // Infliggi danno al giocatore
        playerHealth.TakeDamage(damageToPlayer);

        // Attende l'intervallo di attacco prima di poter attaccare di nuovo
        yield return new WaitForSeconds(attackInterval);

        isAttacking = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy health: " + health);

        if (health <= 0)
        {
            Die(); // Gestisci la morte dell'enemy
        }
    }

    private void Die()
    {
        // Logica per la morte dell'enemy (es. distruzione, animazione, ecc.)
        Destroy(gameObject);
    }

    private bool IsGrounded()
    {
        // Controlla se il nemico è a terra utilizzando un overlap circle
        return Physics2D.OverlapCircle(transform.position, 0.1f, groundLayer) != null;
    }

    private void OnDrawGizmosSelected()
    {
        // Visualizza il cerchio di controllo a terra nell'editor per il debug
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}
