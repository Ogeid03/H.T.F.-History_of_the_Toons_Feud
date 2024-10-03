using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float attackDuration = 1.5f; // Durata dell'attacco (tempo di animazione)
    public float attackRange = 0.5f;     // Raggio dell'attacco
    public LayerMask enemyLayer;         // Layer per i nemici
    public int damageAmount = 10;        // Danno inflitto ai nemici

    private Animator animator;            // Riferimento all'animator
    private bool isAttacking = false;     // Controlla se il giocatore sta attaccando
    private Queue<IEnumerator> attackQueue = new Queue<IEnumerator>(); // Coda per gestire gli attacchi

    void Start()
    {
        // Ottieni l'Animator dal giocatore
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Attacco
        if (Input.GetButtonDown("Fire1")) // Il pulsante "Fire1" è predefinito per gli attacchi
        {
            // Aggiungi un attacco alla coda
            attackQueue.Enqueue(PerformAttack());
            // Se non stiamo già attaccando, inizia ad attaccare
            if (!isAttacking)
            {
                StartCoroutine(ExecuteNextAttack());
            }
        }
    }

    private IEnumerator ExecuteNextAttack()
    {
        while (attackQueue.Count > 0)
        {
            // Prendi l'attacco dalla coda
            yield return StartCoroutine(attackQueue.Dequeue());
        }
    }

    // Coroutine per eseguire l'attacco
    IEnumerator PerformAttack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack"); // Attiva l'animazione di attacco
        yield return new WaitForSeconds(attackDuration); // Aspetta la durata dell'attacco

        // Rilevamento dei nemici colpiti
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            // Gestisci il danno o altre interazioni
            Debug.Log("Colpito: " + enemy.name);
            // Supponiamo che i nemici abbiano un metodo TakeDamage(int damage)
            enemy.GetComponent<EnemyAI>().TakeDamage(damageAmount);
        }

        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        // Visualizza il raggio di attacco nell'editor per il debug
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
