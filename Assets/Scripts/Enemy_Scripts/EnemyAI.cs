using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f; // Velocità del nemico
    private Transform target; // Target più vicino (giocatore)
    private bool isFacingRight = true; // Controllo dello stato di direzione
    private bool isPlayerInRange = false; // Controlla se il giocatore è nella zona di attacco

    // Riferimento al GameObject che contiene la grafica del nemico
    public Transform graphics;
    private Transform pomodoro; // Riferimento al pomodoro
    private Animator animator; // Riferimento all'Animator

    void Start()
    {
        // Cerca il pomodoro tra i figli
        pomodoro = transform.Find("pomodoro_spappolato");
        if (pomodoro == null)
        {
            Debug.LogWarning("Pomodoro non trovato tra i figli di " + gameObject.name);
        }
        // Ottieni il componente Animator
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Trova il giocatore più vicino
        FindClosestPlayer();

        if (target != null && !isPlayerInRange)
        {
            // Muovi il nemico verso il giocatore lungo l'asse X
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += new Vector3(direction.x, 0, 0) * speed * Time.deltaTime;

            // Aggiorna la velocità nell'animatore
            animator.SetFloat("Speed", Mathf.Abs(direction.x) * speed); // Aggiorna il parametro Speed


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
            // Se non ci sono movimenti, imposta Speed a 0
            animator.SetFloat("Speed", 0);
        }
    }

    // Metodo per trovare il giocatore più vicino
    void FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player"); // Trova tutti i giocatori con il tag "Player"
        float closestDistance = Mathf.Infinity; // Distanza iniziale molto grande
        GameObject closestPlayer = null;

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position); // Calcola la distanza
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayer = player; // Salva il giocatore più vicino
            }
        }

        if (closestPlayer != null)
        {
            target = closestPlayer.transform; // Imposta il target sul giocatore più vicino
        }
    }

    // Metodo per flippare la grafica del nemico
    void Flip()
    {
        isFacingRight = !isFacingRight; // Inverti lo stato di direzione
        Vector3 theScale = graphics.localScale;
        theScale.x *= -1; // Inverti l'asse X della scala della grafica del nemico
        graphics.localScale = theScale;

        // Flippa anche il pomodoro
        if (pomodoro != null)
        {
            Vector3 pomodoroScale = pomodoro.localScale;
            pomodoroScale.x *= -1; // Inverti l'asse X della scala del pomodoro
            pomodoro.localScale = pomodoroScale;

            Vector3 offset = new Vector3(8f, 0, 0);
            pomodoro.localPosition += offset;
        }
    }

    //Vector3 offset = new Vector3(0.5f, 0, 0);
    //pomodoro.localPosition += offset;
    


    // Metodo chiamato quando l'AttackHitBox entra in contatto con un oggetto
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // Il giocatore è nella zona di attacco
            // Qui puoi anche gestire l'attacco se necessario
            Debug.Log("Giocatore a contatto con AttackHitBox.");
        }
    }

    // Metodo chiamato quando l'AttackHitBox esce dal contatto con un oggetto
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // Il giocatore non è più nella zona di attacco
            Debug.Log("Giocatore è uscito dalla zona di attacco.");
        }
    }
}
