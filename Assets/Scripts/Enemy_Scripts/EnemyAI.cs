using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f; // Velocit� del nemico
    private Transform target; // Target pi� vicino (giocatore)
    private bool isFacingRight = true; // Controllo dello stato di direzione
    private bool isPlayerInRange = false; // Controlla se il giocatore � nella zona di attacco

    // Parametri di salto
    public float jumpForce = 5f; // Altezza del salto

    // Riferimento al GameObject che contiene la grafica del nemico
    public Transform graphics;
    private Transform pomodoro; // Riferimento al pomodoro
    private Animator animator; // Riferimento all'Animator
    private Rigidbody2D rb; // Riferimento al Rigidbody2D

    // Distanza per rilevare un "buco"
    public float holeDetectionDistance = 10f; // Distanza di ricerca del buco
    private GameObject hole; // Riferimento al buco pi� vicino

    public float heightDifferenceThreshold = 3f; // Differenza di altezza per attivare la ricerca del buco

    void Start()
    {
        // Cerca il pomodoro tra i figli
        pomodoro = transform.Find("pomodoro_spappolato");
        if (pomodoro == null)
        {
            Debug.LogWarning("Pomodoro non trovato tra i figli di " + gameObject.name);
        }

        // Ottieni il componente Animator e Rigidbody2D
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Trova il giocatore pi� vicino
        FindClosestPlayer();

        if (target != null)
        {
            // Se la differenza di altezza tra il nemico e il giocatore � maggiore di heightDifferenceThreshold
            if (transform.position.y > target.position.y + heightDifferenceThreshold)
            {
                // Cerca il buco pi� vicino e muoviti verso di esso
                MoveTowardsHole();
            }
            else
            {
                // Comportamento normale del nemico quando sulla stessa Y o sotto il giocatore
                MoveTowardsPlayer();
            }
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }

    // Metodo per trovare il giocatore pi� vicino
    void FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float closestDistance = Mathf.Infinity;
        GameObject closestPlayer = null;

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayer = player;
            }
        }

        if (closestPlayer != null)
        {
            target = closestPlayer.transform;
        }
    }

    // Metodo per muovere il nemico verso il giocatore
    void MoveTowardsPlayer()
    {
        if (!isPlayerInRange)
        {
            // Muovi il nemico verso il giocatore lungo l'asse X
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += new Vector3(direction.x, 0, 0) * speed * Time.deltaTime;

            // Aggiorna la velocit� nell'animatore
            animator.SetFloat("Speed", Mathf.Abs(direction.x) * speed);

            // Controlla la direzione e flippa il nemico se necessario
            if (direction.x > 0 && isFacingRight)
            {
                Flip();
            }
            else if (direction.x < 0 && !isFacingRight)
            {
                Flip();
            }
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }

    // Metodo per muoversi verso il buco pi� vicino
    void MoveTowardsHole()
    {
        // Cerca il buco pi� vicino
        GameObject[] holes = GameObject.FindGameObjectsWithTag("Hole");
        float closestDistance = Mathf.Infinity;

        // Trova il buco pi� vicino
        foreach (GameObject currentHole in holes)
        {
            float distance = Vector3.Distance(transform.position, currentHole.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                hole = currentHole; // Salva il buco pi� vicino
            }
        }

        // Se � stato trovato un buco, muovi verso di esso
        if (hole != null)
        {
            Vector3 direction = (hole.transform.position - transform.position).normalized;
            transform.position += new Vector3(direction.x, 0, 0) * speed * Time.deltaTime;

            // Aggiorna la velocit� nell'animatore
            animator.SetFloat("Speed", Mathf.Abs(direction.x) * speed);

            // Controlla la direzione e flippa il nemico se necessario
            if (direction.x > 0 && isFacingRight)
            {
                Flip();
            }
            else if (direction.x < 0 && !isFacingRight)
            {
                Flip();
            }
        }
    }

    // Metodo per flippare la grafica del nemico
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = graphics.localScale;
        theScale.x *= -1;
        graphics.localScale = theScale;

        if (pomodoro != null)
        {
            Vector3 pomodoroScale = pomodoro.localScale;
            pomodoroScale.x *= -1;
            pomodoro.localScale = pomodoroScale;

            Vector3 offset = new Vector3(8f, 0, 0);
            pomodoro.localPosition += offset;
        }
    }

    // Metodo per la gestione della collisione con ostacoli (Collisione Fisica)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Giocatore a contatto con AttackHitBox.");
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Quando il nemico collide con un ostacolo, salta
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Salto verticale
            Debug.Log("Il nemico ha saltato un ostacolo.");
        }
    }

    // Metodo chiamato quando l'AttackHitBox esce dal contatto con un oggetto (Collisione)
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("Giocatore � uscito dalla zona di attacco.");
        }
    }

    // Metodo per la gestione del trigger (zona di attacco)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Il giocatore entra nella zona di attacco
            isPlayerInRange = true;
            Debug.Log("Giocatore nella zona di attacco.");
        }
        else if (other.CompareTag("Obstacle"))
        {
            // Quando il nemico entra in un trigger di ostacolo, salta
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Salto verticale
            Debug.Log("Il nemico ha saltato un ostacolo nel trigger.");
        }
    }

    // Metodo chiamato quando il trigger esce dal contatto con un oggetto
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Il giocatore esce dalla zona di attacco
            isPlayerInRange = false;
            Debug.Log("Giocatore � uscito dalla zona di attacco nel trigger.");
        }
    }
}
