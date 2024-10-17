using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damage = 10f; // Danno inflitto al giocatore
    public float attackCooldown = 1f; // Tempo di attesa tra gli attacchi
    private Transform target; // Riferimento al giocatore
    private float lastAttackTime; // Tempo dell'ultimo attacco

    void Update()
    {
        // Se il target è stato impostato e il cooldown è scaduto, infliggi danno
        if (target != null && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time; // Resetta il tempo dell'ultimo attacco
        }
    }

    // Metodo per infliggere danno al giocatore
    void Attack()
    {
        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>(); // Ottieni il componente salute del giocatore
        if (playerHealth != null)
        {
            playerHealth.TakeDamage((int)damage); // Infliggi danno al giocatore
            Debug.Log("Attaccato il giocatore! Danno inflitto: " + damage);
        }
    }

    // Metodo chiamato quando l'AttackHitBox entra in contatto con un oggetto
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform; // Imposta il target sul giocatore
            Debug.Log("Giocatore entrato nell'area di attacco.");
        }
    }

    // Metodo chiamato quando l'AttackHitBox esce dal contatto con un oggetto
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            target = null; // Rimuovi il target quando il giocatore esce dall'area di attacco
            Debug.Log("Giocatore uscito dall'area di attacco.");
        }
    }
}
