using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damageAmount = 10; // Danno inflitto dal proiettile

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Controlla se il proiettile colpisce un nemico
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Infliggi danno al nemico
            collision.gameObject.GetComponent<EnemyAI>().TakeDamage(damageAmount);
            Destroy(gameObject); // Distruggi il proiettile dopo l'impatto
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // Infliggi danno al giocatore
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
            Destroy(gameObject); // Distruggi il proiettile dopo l'impatto
        }
        else
        {
            // Distruggi il proiettile se colpisce un altro oggetto
            Destroy(gameObject);
        }
    }
}
