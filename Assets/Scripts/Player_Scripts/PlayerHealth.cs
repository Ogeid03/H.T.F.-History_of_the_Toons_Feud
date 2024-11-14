using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;            // Salute massima del giocatore
    private int currentHealth;             // Salute attuale del giocatore

    public Text healthText;                // Riferimento all'elemento UI Text
    public Image healthHeadImage;          // Riferimento all'elemento Image per la salute del giocatore

    // Sprite della salute in base alla percentuale di vita
    public Sprite healthAbove75;
    public Sprite healthAbove50;
    public Sprite healthAbove25;
    public Sprite healthAbove0;

    public GameObject gameOverScreen;      // Riferimento alla schermata di Game Over

    public Camera mainCamera;              // Riferimento alla camera principale

    void Start()
    {
        currentHealth = maxHealth;         // Inizializza la salute al massimo
        UpdateHealthUI();                  // Aggiorna l'interfaccia della salute
        gameOverScreen.SetActive(false);   // Nasconde la schermata di Game Over all'inizio

        // Assicurati che la velocità di gioco sia normale all'inizio
        Time.timeScale = 1;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;           // Riduce la salute in base al danno
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // Limita la salute tra 0 e il massimo
        Debug.Log("Giocatore ha subito danno! Salute attuale: " + currentHealth);
        UpdateHealthUI();                  // Aggiorna la UI

        if (currentHealth <= 0)
        {
            Die();                         // Chiama la funzione di morte se la salute è zero
        }
    }

    private void Die()
    {
        Debug.Log("Il giocatore è morto!");

        // Scollega la camera dal giocatore
        if (mainCamera != null)
        {
            mainCamera.transform.parent = null;
        }

        // Mostra la schermata di Game Over
        gameOverScreen.SetActive(true);

        // Ferma completamente il gioco
        Time.timeScale = 0;
    }

    private void UpdateHealthUI()
    {
        healthText.text = currentHealth.ToString();  // Aggiorna il testo con la salute attuale
        UpdateHealthHeadImage();                     // Aggiorna l'immagine della salute
    }

    private void UpdateHealthHeadImage()
    {
        float healthPercentage = (float)currentHealth / maxHealth * 100;

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
        if (currentHealth < maxHealth)
        {
            currentHealth += amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            Debug.Log("Giocatore ha raccolto un medikit! Salute attuale: " + currentHealth);
            UpdateHealthUI();
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            Projectile projectile = other.GetComponent<Projectile>();
            if (projectile != null)
            {
                TakeDamage(projectile.GetDamageAmount());
                Destroy(other.gameObject);
            }
        }
        else if (other.CompareTag("Medikit"))
        {
            Medikit medikit = other.GetComponent<Medikit>();
            if (medikit != null && currentHealth != 100)
            {
                Heal(medikit.healAmount);
                Destroy(other.gameObject);
            }
        }
    }
}
