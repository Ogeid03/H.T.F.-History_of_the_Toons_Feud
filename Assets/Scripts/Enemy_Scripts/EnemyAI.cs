using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;           // Velocità di movimento
    public float jumpForce = 5f;           // Forza del salto
    public float detectionRange = 10f;     // Raggio di rilevamento del giocatore
    public float attackRange = 2f;         // Raggio di attacco del giocatore
    public float attackInterval = 1f;      // Intervallo tra gli attacchi
    public int attackDamage = 10;          // Danno inflitto al giocatore

    private Rigidbody2D rb;
    private Transform player;              // Riferimento al giocatore
    private bool isAttacking = false;      // Controlla se il nemico sta attaccando
    private bool isGrounded = true;        // Controlla se il nemico è a terra
    public LayerMask groundLayer;          // Layer per il terreno
    public Transform groundCheck;          // Punto di controllo per verificare se il nemico è a terra
    public float groundCheckRadius = 0.2f; // Raggio di controllo per il terreno

    private PlayerHealth playerHealth;     // Riferimento alla salute del giocatore

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Ottieni il Rigidbody2D del nemico
        player = GameObject.FindGameObjectWithTag("Player").transform; // Trova il giocatore
        playerHealth = player.GetComponent<PlayerHealth>();  // Ottieni il componente PlayerHealth

        if (player == null)
        {
            Debug.LogError("Giocatore non trovato! Assicurati che l'oggetto Player abbia il tag 'Player'.");
        }
    }

    void Update()
    {
        // Controlla se il nemico è a terra
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            Debug.Log($"Distanza dal giocatore: {distanceToPlayer}");

            // Muovi verso il giocatore solo se è entro il raggio di rilevamento ma fuori dal raggio di attacco
            if (distanceToPlayer <= detectionRange && distanceToPlayer > attackRange)
            {
                MoveTowardsPlayer();
            }

            // Attacca il giocatore se è entro il raggio d'attacco
            if (distanceToPlayer <= attackRange && !isAttacking)
            {
                StartCoroutine(AttackPlayer());
            }
        }
    }

    void MoveTowardsPlayer()
    {
        // Calcola la direzione verso il giocatore
        Vector2 direction = (player.position - transform.position).normalized;

        // Muovi il nemico a destra o sinistra
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        Debug.Log($"Direzione: {direction}");

        // Salta se c'è un ostacolo (se necessario, puoi migliorare la logica di salto)
        if (IsObstacleInFront() && isGrounded)
        {
            Jump();
        }

        // Controlla la direzione e flippa se necessario
        if (direction.x < 0 && transform.localScale.x > 0)
        {
            Flip();
        }
        else if (direction.x > 0 && transform.localScale.x < 0)
        {
            Flip();
        }
    }

    bool IsObstacleInFront()
    {
        // Puoi implementare un raycast o trigger per verificare se c'è un ostacolo davanti al nemico
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 0.5f, groundLayer);
        return hit.collider != null;
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    void Flip()
    {
        // Cambia la direzione del nemico
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    IEnumerator AttackPlayer()
    {
        isAttacking = true;

        // Infliggi danno al giocatore
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }

        // Attende l'intervallo di attacco prima di poter attaccare di nuovo
        yield return new WaitForSeconds(attackInterval);

        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        // Gizmo per il controllo del terreno
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
