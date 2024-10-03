using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;        // Velocità di movimento del nemico
    public float attackRange = 1.5f;    // Distanza alla quale il nemico può attaccare
    public float attackInterval = 1f;   // Intervallo tra un attacco e l'altro
    private Transform player;           // Riferimento al giocatore
    private bool isAttacking = false;   // Controlla se il nemico sta già attaccando

    void Start()
    {
        // Trova l'oggetto con il tag "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
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

        // Aggiungi qui il codice per infliggere danni al giocatore, ad esempio chiamare un metodo del giocatore che riduce la sua salute.

        // Attende l'intervallo di attacco prima di poter attaccare di nuovo
        yield return new WaitForSeconds(attackInterval);

        isAttacking = false;
    }
}
