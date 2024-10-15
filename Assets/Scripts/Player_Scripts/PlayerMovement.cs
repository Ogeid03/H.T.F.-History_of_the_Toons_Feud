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

        // Controllo del movimento da tastiera (WASD o frecce)
        moveInput = Input.GetAxisRaw("Horizontal");  // Questo rileva i tasti A/D o le frecce sinistra/destra

        // Aggiunta del movimento da tasti GUI
        if (moveLeft)
        {
            moveInput = -1f; // Movimento a sinistra
        }
        else if (moveRight)
        {
            moveInput = 1f; // Movimento a destra
        }

        // Aggiorna la velocità del rigidbody
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

        // Resetta il numero di salti se il giocatore è a terra
        if (isGrounded)
        {
            jumpCount = 0; // Resetta il numero di salti quando il giocatore è a terra
        }

        // Salto o doppio salto
        if ((isJumping || Input.GetButtonDown("Jump")) && (isGrounded || jumpCount < maxJumps))
        {
            // Salta
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++; // Incrementa il contatore di salti
            animator.SetTrigger("Jump");
            isJumping = false;  // Reset il flag di salto
        }
    }


    // Metodi per i tasti mobili
    public void OnMoveLeft()
    {
        moveLeft = true;
    }

    public void StopMoveLeft()
    {
        moveLeft = false;
    }

    public void OnMoveRight()
    {
        moveRight = true;
    }

    public void StopMoveRight()
    {
        moveRight = false;
    }

    public void OnJump()
    {
        isJumping = true;
    }
}
