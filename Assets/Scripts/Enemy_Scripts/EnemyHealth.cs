using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;              // Salute massima del nemico
    private int currentHealth;              // Salute attuale del nemico

    void Start()
    {
        currentHealth = maxHealth;          // Inizializza la salute corrente a quella massima
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

    private void Die()
    {
        // Logica per la morte del nemico (es. distruzione, animazione, ecc.)
        Debug.Log("Enemy died!");
        Destroy(gameObject);  // Distruggi l'oggetto nemico
    }

   
}