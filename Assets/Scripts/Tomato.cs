using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Projectile : MonoBehaviour
{
    public int damageAmount = 10; // Danno inflitto dal proiettile
    public float knockbackForce = 10f; // Forza del knockback (puoi personalizzarlo se necessario)

    // Funzione che restituisce il valore del danno inflitto
    public int GetDamageAmount()
    {
        return damageAmount;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Controlla se il proiettile colpisce un nemico
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Ottieni il componente EnemyHealth per infliggere danno
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount); // Infliggi danno al nemico
            }
            Destroy(gameObject); // Distruggi il proiettile dopo l'impatto
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // Infliggi danno al giocatore
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount); // Infliggi solo danno, senza knockback
            }
            Destroy(gameObject); // Distruggi il proiettile dopo l'impatto
        }
        else
        {
            // Distruggi il proiettile se colpisce un altro oggetto
            Destroy(gameObject);
        }
    }
}
