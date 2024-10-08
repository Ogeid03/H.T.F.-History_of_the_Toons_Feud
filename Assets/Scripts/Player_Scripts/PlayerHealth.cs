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
        // Qui potresti aggiungere altre logiche come la gestione della scena o la visualizzazione di un menu di morte
    }

    private void UpdateHealthUI()
    {
        healthText.text = currentHealth.ToString(); // Aggiorna il testo con la salute attuale
    }

    // Questa funzione gestisce la collisione con i proiettili nemici
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet")) // Controlla se il proiettile è di un nemico
        {
            Projectile projectile = other.GetComponent<Projectile>();
            if (projectile != null)
            {
                TakeDamage(projectile.GetDamageAmount()); // Infliggi danno al giocatore
                Destroy(other.gameObject); // Distruggi il proiettile dopo l'impatto
            }
        }
    }
}