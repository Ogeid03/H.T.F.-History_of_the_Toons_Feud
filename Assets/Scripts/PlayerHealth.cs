using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;           // Salute massima del giocatore
    private int currentHealth;            // Salute attuale del giocatore
    public float knockbackForce = 10f;    // Forza del knockback
    private Rigidbody2D rb;               // Riferimento al Rigidbody2D del giocatore

    void Start()
    {
        currentHealth = maxHealth;        // Imposta la salute attuale al valore massimo
        rb = GetComponent<Rigidbody2D>(); // Ottieni il Rigidbody2D per applicare il knockback
    }

    public void TakeDamage(int damage, Vector2 knockbackDirection)
    {
        currentHealth -= damage; // Riduci la salute
        Debug.Log("Giocatore ha subito danno! Salute attuale: " + currentHealth);

        // Applica il knockback
        Knockback(knockbackDirection);

        if (currentHealth <= 0)
        {
            Die(); // Gestisci la morte del giocatore
        }
    }

    private void Knockback(Vector2 direction)
    {
        // Applica una forza nella direzione opposta
        rb.velocity = Vector2.zero; // Resetta la velocità attuale
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse); // Applica la forza di knockback
    }

    private void Die()
    {
        Debug.Log("Il giocatore è morto!");
        gameObject.SetActive(false); // Disattiva il giocatore per il test
    }
}
