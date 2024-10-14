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
        Debug.Log("Il giocatore � morto!");
        gameObject.SetActive(false);      // Disattiva il giocatore per il test
        // Qui potresti aggiungere altre logiche come la gestione della scena o la visualizzazione di un menu di morte
    }

    private void UpdateHealthUI()
    {
        healthText.text = currentHealth.ToString(); // Aggiorna il testo con la salute attuale
    }

    public void Heal(int amount)          // Nuova funzione per guarire il giocatore
    {
        currentHealth += amount;          // Aumenta la salute
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // Limita la salute al massimo
        Debug.Log("Giocatore ha raccolto un medikit! Salute attuale: " + currentHealth);

        UpdateHealthUI();                 // Aggiorna il testo UI dopo la guarigione
    }

    // Questa funzione gestisce la collisione con i proiettili nemici
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet")) // Controlla se il proiettile � di un nemico
        {
            Projectile projectile = other.GetComponent<Projectile>();
            if (projectile != null)
            {
                TakeDamage(projectile.GetDamageAmount()); // Infliggi danno al giocatore
                Destroy(other.gameObject); // Distruggi il proiettile dopo l'impatto
            }
        }
    }

    // Gestione del medikit
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Medikit"))  // Controlla se l'oggetto ha il tag Medikit
        {
            Medikit medikit = other.GetComponent<Medikit>();
            if (medikit != null)
            {
                Heal(medikit.healAmount);  // Guarisci il giocatore con la quantit� del medikit
                Destroy(other.gameObject); // Distruggi il medikit dopo averlo raccolto
            }
        }
    }
}