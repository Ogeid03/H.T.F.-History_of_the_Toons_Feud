using UnityEngine;
using UnityEngine.UI;  // Importa il namespace per usare gli elementi UI come Text e Image

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;           // Salute massima del giocatore
    private int currentHealth;            // Salute attuale del giocatore

    public Text healthText;               // Riferimento all'elemento UI Text

    // Immagini da cambiare in base alla salute
    public Image healthHeadImage;         // Riferimento all'elemento Image nella scena chiamato Health_Head
    public Sprite healthAbove75;          // Sprite da mostrare quando la salute è sopra il 75%
    public Sprite healthAbove50;          // Sprite da mostrare quando la salute è sopra il 50%
    public Sprite healthAbove25;          // Sprite da mostrare quando la salute è sopra il 25%
    public Sprite healthAbove0;           // Sprite da mostrare quando la salute è sopra lo 0%

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
        // Aggiorna il testo con la salute attuale
        healthText.text = currentHealth.ToString();

        // Aggiorna l'immagine dell'elemento Health_Head in base alla percentuale di salute
        UpdateHealthHeadImage();
    }

    private void UpdateHealthHeadImage()
    {
        float healthPercentage = (float)currentHealth / maxHealth * 100;  // Calcola la percentuale di salute

        if (healthPercentage > 75)
        {
            healthHeadImage.sprite = healthAbove75;
        }
        else if (healthPercentage > 50)
        {
            healthHeadImage.sprite = healthAbove50;
        }
        else if (healthPercentage > 25)
        {
            healthHeadImage.sprite = healthAbove25;
        }
        else if (healthPercentage > 0)
        {
            healthHeadImage.sprite = healthAbove0;
        }
    }

    public void Heal(int amount)          // Nuova funzione per guarire il giocatore
    {
        currentHealth += amount;          // Aumenta la salute
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // Limita la salute al massimo
        Debug.Log("Giocatore ha raccolto un medikit! Salute attuale: " + currentHealth);

        UpdateHealthUI();                 // Aggiorna il testo UI dopo la guarigione
    }

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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Medikit"))  // Controlla se l'oggetto ha il tag Medikit
        {
            Medikit medikit = other.GetComponent<Medikit>();
            if (medikit != null)
            {
                Heal(medikit.healAmount);  // Guarisci il giocatore con la quantità del medikit
                Destroy(other.gameObject); // Distruggi il medikit dopo averlo raccolto
            }
        }
    }
}
