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

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se l'oggetto che ha colpito il nemico ha il tag "Bullet"
        if (other.CompareTag("Bullet"))
        {
            // Ottieni il componente "Projectile" dall'oggetto colpito
            Projectile projectile = other.GetComponent<Projectile>();

            // Se esiste il componente "Projectile", ottieni il danno e applicalo
            if (projectile != null)
            {
                // Applica il danno predefinito del proiettile
                TakeDamage(projectile.GetDamageAmount());

                // Distruggi il proiettile dopo l'impatto
                Destroy(other.gameObject);
            }
        }
    }
}