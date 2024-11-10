using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damage = 10f; // Danno inflitto al giocatore
    public float attackCooldown = 1f; // Tempo di attesa tra gli attacchi
    private Transform target; // Riferimento al giocatore
    private float lastAttackTime; // Tempo dell'ultimo attacco

    public PrefabSpawner prefabSpawner; // Riferimento allo script PrefabSpawner

    void Update()
    {
        // Controlla se il target è presente e se il cooldown è scaduto
        if (target != null && Time.time >= lastAttackTime + attackCooldown)
        {
            // Esegui l'attacco e attiva lo spawn del prefab
            Attack();
            prefabSpawner?.SpawnPrefab(); // Chiama il metodo SpawnPrefab() su PrefabSpawner

            lastAttackTime = Time.time; // Aggiorna il tempo dell'ultimo attacco
        }
    }

    // Metodo per infliggere danno al giocatore
    void Attack()
    {
        if (target != null)
        {
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage((int)damage);
                Debug.Log("Attacco effettuato! Danno inflitto: " + damage);
            }
        }
    }

    // Metodo chiamato quando l'AttackHitBox entra in contatto con il giocatore
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform; // Imposta il target sul giocatore
            Debug.Log("Giocatore entrato nell'area di attacco.");
        }
    }

    // Metodo chiamato quando l'AttackHitBox esce dal contatto con il giocatore
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            target = null; // Rimuovi il target quando il giocatore esce dall'area di attacco
            Debug.Log("Giocatore uscito dall'area di attacco.");
        }
    }
}
