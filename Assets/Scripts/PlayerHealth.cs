using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;           // Salute massima del giocatore
    private int currentHealth;            // Salute attuale del giocatore

    void Start()
    {
        currentHealth = maxHealth;        // Imposta la salute attuale al valore massimo
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Riduci la salute
        Debug.Log("Giocatore ha subito danno! Salute attuale: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die(); // Gestisci la morte del giocatore
        }
    }

    private void Die()
    {
        Debug.Log("Il giocatore è morto!");
        gameObject.SetActive(false); // Disattiva il giocatore per il test
    }
}
