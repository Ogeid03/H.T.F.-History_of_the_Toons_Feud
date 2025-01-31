using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;  // Necessario per le coroutine

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;            // Salute massima del giocatore
    private int currentHealth;             // Salute attuale del giocatore

    public Text healthText;                // Riferimento all'elemento UI Text
    public Image healthHeadImage;          // Riferimento all'elemento Image per la salute del giocatore
    public GameObject damageEffect;        // Riferimento al GameObject "Damage"

    // Sprite della salute in base alla percentuale di vita
    public Sprite healthAbove75;
    public Sprite healthAbove50;
    public Sprite healthAbove25;
    public Sprite healthAbove0;

    public Camera mainCamera;              // Riferimento alla camera principale
    private Animator playerAnimator;       // Riferimento all'animator del giocatore
    private bool isDead = false;           // Flag per verificare se il giocatore � morto
    private GameOverManager gameOverManager;

    // Variabili per il suono
    public AudioClip damageSound;          // Suono da riprodurre quando il giocatore subisce danno
    private AudioSource customAudioSource; // AudioSource che verr� recuperato dal GameObject

    void Start()
    {
        currentHealth = maxHealth;         // Inizializza la salute al massimo
        UpdateHealthUI();                  // Aggiorna l'interfaccia della salute


        if (damageEffect != null)
        {
            damageEffect.SetActive(false);
        }
        else
        {
            Debug.LogError("GameObject 'Damage' non assegnato!");
        }

        // Trova GameOverManager automaticamente se non � stato assegnato
        gameOverManager = FindObjectOfType<GameOverManager>();
        if (gameOverManager == null)
        {
            Debug.LogError("GameOverManager non trovato nella scena!");
        }

        // Assicurati che la velocit� di gioco sia normale all'inizio
        Time.timeScale = 1;

        // Trova l'animator del giocatore
        playerAnimator = GetComponent<Animator>();
        if (playerAnimator == null)
        {
            Debug.LogError("Animator non trovato sul giocatore!");
        }

        // Cerca il GameObject con il nome "HittedSound" e recupera il componente AudioSource
        GameObject audioSourceObject = GameObject.Find("HittedSound");
        if (audioSourceObject != null)
        {
            customAudioSource = audioSourceObject.GetComponent<AudioSource>();
            if (customAudioSource == null)
            {
                Debug.LogError("Il GameObject 'HittedSound' non contiene un componente AudioSource!");
            }
        }
        else
        {
            Debug.LogError("Non � stato trovato un oggetto chiamato 'HittedSound' nella scena.");
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // Se il giocatore � gi� morto, non fa nulla

        currentHealth -= damage;           // Riduce la salute in base al danno
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // Limita la salute tra 0 e il massimo
        Debug.Log("Giocatore ha subito danno! Salute attuale: " + currentHealth);

        // Mostra l'effetto "Damage"
        if (damageEffect != null)
        {
            StartCoroutine(ShowDamageEffect());
        }

        // Riproduci il suono del danno usando l'AudioSource assegnato
        if (customAudioSource != null && damageSound != null)
        {
            customAudioSource.PlayOneShot(damageSound);
        }

        UpdateHealthUI();                  // Aggiorna la UI

        if (currentHealth <= 0 && !isDead)
        {
            Die();                         // Chiama la funzione di morte se la salute � zero
        }
    }

    private IEnumerator ShowDamageEffect()
    {
        damageEffect.SetActive(true);  // Attiva l'effetto
        yield return new WaitForSeconds(0.25f);  // Aspetta per 0,5 secondi
        damageEffect.SetActive(false); // Disattiva l'effetto
    }

    private void Die()
    {
        Debug.Log("Il giocatore � morto!");

        // Imposta il trigger "Die" nell'Animator per avviare l'animazione di morte
        if (playerAnimator != null)
        {
            playerAnimator.SetBool("die", true);  // Imposta il parametro booleano "die" su true
        }

        // Segna il giocatore come morto
        isDead = true;

        // Scollega la camera dal giocatore
        if (mainCamera != null)
        {
            mainCamera.transform.parent = null;
        }

        // Attiva la schermata di Game Over attraverso GameOverManager
        if (gameOverManager != null)
        {
            gameOverManager.TriggerGameOverScreen();
        }
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
