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

    private IEnumerator PerformAttack()
    {
        isAttacking = true;                      // Imposta lo stato di attacco a true
        animator.SetTrigger("Attack");           // Attiva l'animazione di attacco

        // Rilevamento degli oggetti colpiti nel raggio d'attacco
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, attackRange);

        // Controllo degli oggetti colpiti
        if (hitObjects.Length > 0)
        {
            foreach (Collider2D hitObject in hitObjects)
            {
                // Verifica se l'oggetto colpito ha il tag "Enemy"
                if (hitObject.CompareTag("Enemy"))
                {
                    Debug.Log("Colpito nemico: " + hitObject.name);

                    // Infliggi danno al nemico
                    EnemyHealth enemyHealth = hitObject.GetComponent<EnemyHealth>();
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
