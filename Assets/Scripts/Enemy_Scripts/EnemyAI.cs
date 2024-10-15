using UnityEngine;

public class SimpleEnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f; // Velocità di movimento del nemico

    private Transform player; // Riferimento al giocatore

    void Start()
    {
        // Trova l'oggetto con il tag "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
        {
            Debug.LogError("Giocatore non trovato! Assicurati che l'oggetto Player abbia il tag 'Player'.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        // Calcola la direzione verso il giocatore
        Vector3 direction = (player.position - transform.position).normalized;

        // Muovi il nemico verso il giocatore
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Flippa l'oggetto in base alla direzione
        if (direction.x < 0 && transform.localScale.x > 0)
        {
            Flip();
        }
        else if (direction.x > 0 && transform.localScale.x < 0)
        {
            Flip();
        }
    }

    void Flip()
    {
        // Cambia la direzione del nemico
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Capovolgi l'asse X
        transform.localScale = scale;
    }
}
