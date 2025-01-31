using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float maxHealth = 250f;       // Vita massima del boss
    private float currentHealth;         // Vita corrente del boss

    public Animator animator;            // Riferimento all'animatore del boss
    public AudioSource hurtSound;        // Suono di danno
    public AudioClip damageClip;         // Clip audio da riprodurre quando il boss subisce danno
    public GameObject deathEffect;       // Effetto visivo alla morte del boss
    private GameOverManager gameOverManager;

    public SpriteRenderer spriteRenderer; // Riferimento allo SpriteRenderer
    public Sprite hurtSprite;             // Nuovo sprite da usare quando la vita � sotto il 50%
    //public Sprite criticalSprite;         // Nuovo sprite da usare quando la vita � sotto il 10%

    void Start()
    {
        currentHealth = maxHealth;       // Imposta la vita iniziale del boss

        // Trova GameOverManager automaticamente se non � stato assegnato
        gameOverManager = FindObjectOfType<GameOverManager>();
        if (gameOverManager == null)
        {
            Debug.LogError("GameOverManager non trovato nella scena!");
        }
    }

    // Funzione per danneggiare il boss
    // Funzione per danneggiare il boss
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;    // Riduce la salute
        Debug.Log("BossHealth health:" + currentHealth);

        // Se la salute del boss � ancora maggiore di zero
        if (currentHealth > 0)
        {
            if (animator != null)
            {
                animator.SetTrigger("Hurt"); // Imposta l'animazione di danno
            }

            if (hurtSound != null && damageClip != null)
            {
                hurtSound.clip = damageClip;  // Imposta il clip audio
                hurtSound.Play(); // Esegui il suono di danno
            }

            // Cambia lo sprite se la vita � sotto il 50%
            if (currentHealth <= maxHealth * 0.5f && spriteRenderer != null && hurtSprite != null)
            {
                spriteRenderer.sprite = hurtSprite; // Cambia lo sprite
                spriteRenderer.transform.localScale = Vector3.one; // Ripristina la scala originale
            }

            /*// Cambia lo sprite se la vita � sotto il 10%
            if (currentHealth <= maxHealth * 0.1f && spriteRenderer != null && criticalSprite != null)
            {
                spriteRenderer.sprite = criticalSprite; // Cambia lo sprite critico

                // Scala lo sprite critico di 3 volte
                spriteRenderer.transform.localScale = new Vector3(3f, 3f, 1f);
                Debug.Log("Scala del Critical Sprite impostata a 3x.");
            }*/
        }
        else
        {
            Die();  // Se la vita � 0 o meno, il boss muore
        }
    }


    // Funzione per la morte del boss
    private void Die()
    {
        currentHealth = 0;   // La vita del boss � ora zero

        if (animator != null)
        {
            animator.SetTrigger("Die");  // Imposta l'animazione di morte
        }

        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity); // Crea l'effetto visivo alla morte
        }

        Debug.Log("Il boss � morto!");

        // Avvia la funzione per mostrare la schermata di Game Over
        if (gameOverManager != null)
        {
            gameOverManager.TriggerYouWonScreen(); // Chiama la funzione pubblica nel GameOverManager
        }
    }

    // Funzione per recuperare la vita del boss
    public void Heal(float healAmount)
    {
        currentHealth += healAmount;  // Aumenta la salute
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // Limita la salute tra 0 e max

        // Se la salute torna sopra il 50%, ripristina lo sprite originale
        if (currentHealth > maxHealth * 0.5f && spriteRenderer != null)
        {
            spriteRenderer.sprite = null; // Imposta lo sprite originale (se necessario)
        }

        // Se la salute torna sopra il 10%, ripristina lo sprite originale o lo sprite di danno
        if (currentHealth > maxHealth * 0.1f && spriteRenderer != null)
        {
            spriteRenderer.sprite = hurtSprite; // Puoi scegliere di ripristinare l'altro sprite se necessario
        }
    }

    // Funzione per ottenere la vita corrente del boss
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
