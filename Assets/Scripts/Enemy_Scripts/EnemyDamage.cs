using System.Collections;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damage = 10f; // Danno inflitto al giocatore
    public float attackCooldown = 1f; // Tempo di attesa tra gli attacchi
    public bool shouldSpawn = false; // Variabile booleana che controlla lo spawn del prefab animato
    private Transform target; // Riferimento al giocatore
    private float lastAttackTime; // Tempo dell'ultimo attacco

    public bool resetShouldSpawn = false; // Variabile che permette di resettare shouldSpawn da uno script esterno

    void Update()
    {
        // Se il target è stato impostato e il cooldown è scaduto, infliggi danno
        if (target != null && Time.time >= lastAttackTime + attackCooldown)
        {
            // Imposta shouldSpawn su true prima di infliggere danno
            shouldSpawn = true;
            Debug.Log("shouldSpawn impostato a TRUE.");  // Log per tracciare quando viene impostato su true

            // Avvia l'attacco (infliggi danno)
            Attack();
            lastAttackTime = Time.time; // Resetta il tempo dell'ultimo attacco
        }

        // Se la variabile resetShouldSpawn è vera, resetta shouldSpawn
        if (resetShouldSpawn)
        {
            shouldSpawn = false;
            resetShouldSpawn = false; // Reset della variabile per evitare che venga eseguito continuamente
            Debug.Log("shouldSpawn è stato resettato a FALSE.");
        }
    }

    // Metodo per infliggere danno al giocatore
    void Attack()
    {
        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>(); // Ottieni il componente salute del giocatore
        if (playerHealth != null)
        {
            // Infliggi danno al giocatore
            playerHealth.TakeDamage((int)damage);
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
