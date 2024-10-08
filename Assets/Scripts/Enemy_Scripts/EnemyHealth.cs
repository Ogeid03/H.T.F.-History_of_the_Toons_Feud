using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;              // Salute massima del nemico
    private int currentHealth;              // Salute attuale del nemico

    public int scoreValue = 10;
    private EnemyScoreManager scoreManager; // Riferimento al gestore del punteggio


    void Start()
    {
        currentHealth = maxHealth;          // Inizializza la salute corrente a quella massima
        scoreManager = FindObjectOfType<EnemyScoreManager>(); // Trova il gestore del punteggio

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();  // Chiama la funzione per gestire la morte del nemico
        }
    }

    public string GetPrefabName()
    {
        return gameObject.name; // Restituisce il nome dell'oggetto
    }

    private void Die()
    {
        // Logica per la morte del nemico (es. distruzione, animazione, ecc.)
        Debug.Log("Enemy: " + GetPrefabName() + " died!");

        if (scoreManager != null)
        {
            scoreManager.AddScore(scoreValue);
        }

        Destroy(gameObject);  // Distruggi l'oggetto nemico
    }

   
}