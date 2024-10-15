using UnityEngine;
using UnityEngine.UI;

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
        currentHealth = maxHealth;        // Inizializza la salute attuale al valore massimo all'inizio
        UpdateHealthUI();                 // Aggiorna il testo UI e l'immagine all'inizio
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;          // Riduci la salute del danno subito
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // Assicurati che la salute non scenda sotto 0

        Debug.Log("Giocatore ha subito danno! Salute attuale: " + currentHealth);

        UpdateHealthUI();                 // Aggiorna la UI quando si subisce danno

        if (currentHealth <= 0)
        {
            Die();                        // Gestisci la morte del giocatore
        }
    }

    private void Die()
    {
        Debug.Log("Il giocatore è morto!");
        gameObject.SetActive(false);      // Disattiva il giocatore
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

    public void Heal(int amount)
    {
        // Controlla se la salute è inferiore a quella massima
        if (currentHealth < maxHealth)
        {
            // Incrementa la salute, ma assicurati che non superi il massimo
            currentHealth += amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // Impedisci che superi il massimo

            Debug.Log("Giocatore ha raccolto un medikit! Salute attuale: " + currentHealth);

            UpdateHealthUI();  // Aggiorna la UI dopo la guarigione
        }
        else
        {
            Debug.Log("Giocatore ha già tutta la vita, non può raccogliere il medikit.");
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
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
        else if (other.CompareTag("Medikit"))  // Controlla se l'oggetto ha il tag Medikit
        {
            Medikit medikit = other.GetComponent<Medikit>();
            if (medikit != null && currentHealth!=100)
            {
                Heal(medikit.healAmount);  // Guarisci il giocatore con la quantità del medikit
                Destroy(other.gameObject); // Distruggi il medikit dopo averlo raccolto
            }
        }
    }
}
