using UnityEngine;

public class Medikit : MonoBehaviour
{
    public int healAmount = 20; // Quantità di vita che il medikit recupera

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Controlla se l'oggetto che ha toccato il medikit è il giocatore
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount); // Guarisce il giocatore
                Destroy(gameObject); // Distrugge il medikit dopo l'uso
            }
        }
    }
}