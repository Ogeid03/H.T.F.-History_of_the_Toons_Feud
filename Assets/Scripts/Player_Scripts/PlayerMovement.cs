using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;        // Velocità di movimento laterale
    public float jumpForce = 10f;       // Forza del salto
    public LayerMask groundLayer;       // Layer del terreno per verificare se è a terra
    public Transform groundCheck;       // Oggetto per verificare se il giocatore è a terra
    public float groundCheckRadius = 0.2f; // Raggio per il controllo del terreno
    public int maxJumps = 2;            // Numero massimo di salti (doppio salto)
    public float stairHeight = 1f;      // Altezza dello scalino che il giocatore può superare
    public float stairCheckDistance = 0.1f; // Distanza per controllare se ci sono scalini

    private Rigidbody2D rb;             // Riferimento al rigidbody del giocatore
    private Animator animator;          // Riferimento all'animator
    private bool isGrounded;            // Controlla se il giocatore è a terra
    private int jumpCount;              // Contatore dei salti

    // Variabili per i tasti mobili
    private bool moveLeft = false;
    private bool moveRight = false;
    private bool isJumping = false;
    private bool isAttacking = false;
    private bool isThrowing = false;

    void Start()
    {
        // Ottieni il Rigidbody2D e l'Animator dal giocatore
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Movimento laterale
        float moveInput = 0f;
        if (moveLeft) moveInput = -1f; // Solo movimento a sinistra
        if (moveRight) moveInput = 1f; // Potresti mantenere il movimento a destra se necessario

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Imposta l'animazione di corsa
        animator.SetFloat("Speed", Mathf.Abs(moveInput));

        // Flip del personaggio in base alla direzione
        if (moveInput < 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Personaggio rivolto a destra
        }
        else if (moveInput > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Personaggio rivolto a sinistra
        }

        // Controlla se il giocatore è a terra
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        animator.SetBool("IsGrounded", isGrounded);

        if (isGrounded)
        {
            jumpCount = 0; // Resetta il numero di salti quando il giocatore è a terra
        }

        // Salto o doppio salto
        if (isJumping && (isGrounded || jumpCount < maxJumps))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++; // Incrementa il contatore di salti
            animator.SetTrigger("Jump");
            isJumping = false;  // Reset il flag di salto
        }

        // Controllo della salita sulle scale
        if (moveInput != 0 && IsNearStairs())
        {
            rb.velocity = new Vector2(rb.velocity.x, stairHeight); // Salta sopra lo scalino
        }

        // Azione attacco corpo a corpo (da implementare)
        if (isAttacking)
        {
            Debug.Log("Attacco corpo a corpo eseguito!");
            isAttacking = false;
        }

        // Azione lancio proiettile (da implementare)
        if (isThrowing)
        {
            Debug.Log("Proiettile lanciato!");
            isThrowing = false;
        }
    }

    private bool IsNearStairs()
    {
        // Controlla se ci sono scalini di fronte al giocatore
        Vector2 position = transform.position;
        Vector2 direction = new Vector2(transform.localScale.x, 0); // Direzione orizzontale
        RaycastHit2D hit = Physics2D.Raycast(position, direction, stairCheckDistance, groundLayer);
        return hit.collider != null && hit.collider.transform.position.y <= position.y + stairHeight;
    }

    // Metodi per i tasti mobili
    public void OnMoveLeft()
    {
        Debug.Log("Moving Left");
        moveLeft = true;
    }

    public void StopMoveLeft()
    {
        Debug.Log("Stopped Moving Left");
        moveLeft = false;
    }


    public void OnMoveRight() // Se necessario per il movimento a destra
    {
        moveRight = true;
    }

    public void StopMoveRight() // Se necessario per fermare il movimento a destra
    {
        moveRight = false;
    }

    public void OnJump()
    {
        isJumping = true;
    }

    public void OnAttack()
    {
        isAttacking = true;
    }

    public void OnThrow()
    {
        isThrowing = true;
    }

    // Visualizza il raggio di controllo a terra (opzionale, utile per debug)
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(transform.localScale.x * stairCheckDistance, 0, 0));
    }
}
