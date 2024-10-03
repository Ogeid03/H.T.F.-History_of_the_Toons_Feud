using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;        // Velocità di movimento laterale
    public float jumpForce = 10f;       // Forza del salto
    public LayerMask groundLayer;       // Layer del terreno per verificare se è a terra
    public Transform groundCheck;       // Oggetto per verificare se il giocatore è a terra
    public float groundCheckRadius = 0.2f; // Raggio per il controllo del terreno
    public float attackDuration = 0.5f; // Durata dell'attacco (tempo di animazione)
    public int maxJumps = 2;            // Numero massimo di salti (doppio salto)

    private Rigidbody2D rb;             // Riferimento al rigidbody del giocatore
    private Animator animator;          // Riferimento all'animator
    private bool isGrounded;            // Controlla se il giocatore è a terra
    private bool isAttacking = false;   // Controlla se il giocatore sta attaccando
    private int jumpCount;              // Contatore dei salti

    void Start()
    {
        // Ottieni il Rigidbody2D e l'Animator dal giocatore
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Movimento laterale solo se non stiamo attaccando
        if (!isAttacking)
        {
            float moveInput = Input.GetAxis("Horizontal");
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
        }

        // Controlla se il giocatore è a terra
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        animator.SetBool("IsGrounded", isGrounded);

        if (isGrounded)
        {
            jumpCount = 0; // Resetta il numero di salti quando il giocatore è a terra
        }

        // Salto o doppio salto
        if ((isGrounded || jumpCount < maxJumps) && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++; // Incrementa il contatore di salti
            animator.SetTrigger("Jump");
        }

        // Attacco
        if (Input.GetButtonDown("Fire1") && !isAttacking) // Il pulsante "Fire1" è predefinito per gli attacchi
        {
            StartCoroutine(PerformAttack());
        }
    }

    // Coroutine per eseguire l'attacco
    IEnumerator PerformAttack()
    {
        isAttacking = true;
        rb.velocity = Vector2.zero; // Ferma il movimento durante l'attacco
        animator.SetTrigger("Attack"); // Attiva l'animazione di attacco
        yield return new WaitForSeconds(attackDuration); // Aspetta la durata dell'attacco
        isAttacking = false;
    }

    // Visualizza il raggio di controllo a terra (opzionale, utile per debug)
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
