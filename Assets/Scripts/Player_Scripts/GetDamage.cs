using UnityEngine;

public class DamageObject : MonoBehaviour
{
    public int damageAmount = 30;  // La quantit� di danno che l'oggetto infligge

    // Questo metodo viene chiamato quando qualcosa entra nel collider dell'oggetto
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Controlla se l'oggetto con cui si � entrato in collisione ha il tag "Player"
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);  // Infligge danno al giocatore
                Debug.Log("Giocatore ha subito 30 danni!");
            }
        }
    }
}