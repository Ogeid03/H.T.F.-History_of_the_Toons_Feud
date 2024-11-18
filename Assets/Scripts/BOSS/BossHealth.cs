using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float maxHealth = 250f;       // Vita massima del boss
    private float currentHealth;         // Vita corrente del boss

    public Animator animator;            // Riferimento all'animatore del boss
    public AudioSource hurtSound;        // Suono di danno
    public GameObject deathEffect;       // Effetto visivo alla morte del boss
    private GameOverManager gameOverManager;

    void Start()
    {
        currentHealth = maxHealth;       // Imposta la vita iniziale del boss

        // Trova GameOverManager automaticamente se non è stato assegnato
        gameOverManager = FindObjectOfType<GameOverManager>();
        if (gameOverManager == null)
        {
            Debug.LogError("GameOverManager non trovato nella scena!");
        }
    }

    // Funzione per danneggiare il boss
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;    // Riduce la salute
        Debug.Log("BossHealth health:" + currentHealth);

        // Se la salute del boss è ancora maggiore di zero
        if (currentHealth > 0)
        {
            if (animator != null)
            {
                animator.SetTrigger("Hurt"); // Imposta l'animazione di danno
            }

            if (hurtSound != null)
            {
                hurtSound.Play(); // Esegui il suono di danno
            }
        }
        else
        {
            Die();  // Se la vita è 0 o meno, il boss muore
        }
    }

    // Funzione per la morte del boss
    private void Die()
    {
        currentHealth = 0;   // La vita del boss è ora zero

        if (animator != null)
        {
            animator.SetTrigger("Die");  // Imposta l'animazione di morte
        }

        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity); // Crea l'effetto visivo alla morte
        }

        Debug.Log("Il boss è morto!");

        // Avvia la funzione per mostrare la schermata di Game Over
        if (gameOverManager != null)
        {
            gameOverManager.TriggerGameOverScreen(); // Chiama la funzione pubblica nel GameOverManager
        }
    }

    // Funzione per recuperare la vita del boss
    public void Heal(float healAmount)
    {
        currentHealth += healAmount;  // Aumenta la salute
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // Limita la salute tra 0 e max
    }

    // Funzione per ottenere la vita corrente del boss
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
