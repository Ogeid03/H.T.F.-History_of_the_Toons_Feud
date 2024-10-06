using UnityEngine;
using UnityEngine.UI;  // Importa il namespace per usare gli elementi UI come Text

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;           // Salute massima del giocatore
    private int currentHealth;            // Salute attuale del giocatore

    public Text healthText;               // Riferimento all'elemento UI Text

    void Start()
    {
        currentHealth = maxHealth;        // Imposta la salute attuale al valore massimo
        UpdateHealthUI();                 // Aggiorna il testo UI alla partenza
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;          // Riduci la salute
        Debug.Log("Giocatore ha subito danno! Salute attuale: " + currentHealth);

        UpdateHealthUI();                 // Aggiorna il testo UI quando si subisce danno

        if (currentHealth <= 0)
        {
            Die();                        // Gestisci la morte del giocatore.
        }
    }

    private void Die()
    {
        Debug.Log("Il giocatore è morto!");
        gameObject.SetActive(false);      // Disattiva il giocatore per il test
    }

    private void UpdateHealthUI()
    {
        healthText.text = currentHealth.ToString(); // Aggiorna il testo con la salute attuale
    }
}
