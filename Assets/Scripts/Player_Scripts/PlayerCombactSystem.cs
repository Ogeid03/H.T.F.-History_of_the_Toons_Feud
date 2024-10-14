using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float attackDuration = 1.5f;    // Durata dell'attacco (tempo di animazione)
    public float attackRange = 3f;          // Raggio dell'attacco
    public LayerMask enemyLayer;            // Layer per i nemici
    public int damageAmount = 10;           // Danno inflitto ai nemici

    private Animator animator;               // Riferimento all'animator
    private bool isAttacking = false;       // Controlla se il giocatore sta attaccando

    void Start()
    {
        // Ottieni l'Animator dal giocatore
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Disabilitiamo l'input da tastiera per l'attacco, poiché useremo i pulsanti UI
        // Se vuoi mantenere anche l'input da tastiera puoi tenerlo:
        /*
        if (Input.GetButtonDown("Fire1") && !isAttacking) // Il pulsante "Fire1" è predefinito per gli attacchi corpo a corpo
        {
            StartCoroutine(PerformAttack());
        }
        */
    }

    // Metodo da collegare al bottone di attacco
    public void OnAttackButtonPressed()
    {
        if (!isAttacking)
        {
            StartCoroutine(PerformAttack());
        }
    }

    // Se hai un'azione di lancio (esempio per un attacco a distanza o una magia)
    public void OnLaunchButtonPressed()
    {
        // Logica per il lancio di un attacco a distanza o una magia
        Debug.Log("Azione di lancio eseguita!");
    }

    private IEnumerator PerformAttack()
    {
        isAttacking = true;                      // Imposta lo stato di attacco a true
        animator.SetTrigger("Attack");           // Attiva l'animazione di attacco

        // Rilevamento dei nemici colpiti
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        // Controllo dei nemici colpiti
        if (hitEnemies.Length > 0)
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("Colpito: " + enemy.name);

                // Infliggi danno al nemico
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damageAmount);
                }
                else
                {
                    Debug.LogWarning("Il nemico non ha il componente EnemyHealth!");
                }
            }
        }
        else
        {
            Debug.Log("Nessun nemico colpito.");
        }

        yield return new WaitForSeconds(attackDuration); // Aspetta la durata dell'animazione

        isAttacking = false;                     // Ripristina lo stato di attacco
    }

    private void OnDrawGizmosSelected()
    {
        // Visualizza il raggio di attacco nell'editor per il debug
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange); // Visualizza una sfera rossa attorno al giocatore
    }
}
