using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float attackDuration = 1.5f;    // Durata dell'attacco (tempo di animazione)
    public float attackRange = 0.5f;       // Raggio dell'attacco
    public LayerMask enemyLayer;           // Layer per i nemici
    public int damageAmount = 10;          // Danno inflitto ai nemici

    // Aggiunte per il proiettile
    public GameObject projectilePrefab;    // Prefab del proiettile del giocatore
    public Transform launchPoint;          // Punto di lancio del proiettile
    public float projectileForce = 10f;    // Forza con cui il proiettile viene lanciato

    private Animator animator;             // Riferimento all'animator
    private bool isAttacking = false;      // Controlla se il giocatore sta attaccando
    private Queue<IEnumerator> attackQueue = new Queue<IEnumerator>(); // Coda per gestire gli attacchi

    void Start()
    {
        // Ottieni l'Animator dal giocatore
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Attacco corpo a corpo
        if (Input.GetButtonDown("Fire1")) // Il pulsante "Fire1" è predefinito per gli attacchi corpo a corpo
        {
            attackQueue.Enqueue(PerformAttack());
            if (!isAttacking)
            {
                StartCoroutine(ExecuteNextAttack());
            }
        }

        // Attacco con proiettili
        if (Input.GetButtonDown("Fire2")) // "Fire2" è predefinito per l'attacco a distanza (mouse destro)
        {
            LaunchProjectile();
        }
    }

    private IEnumerator ExecuteNextAttack()
    {
        while (attackQueue.Count > 0)
        {
            yield return StartCoroutine(attackQueue.Dequeue());
        }
    }

    // Coroutine per eseguire l'attacco corpo a corpo
    IEnumerator PerformAttack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack"); // Attiva l'animazione di attacco
        yield return new WaitForSeconds(attackDuration); // Aspetta la durata dell'attacco

        // Rilevamento dei nemici colpiti
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Colpito: " + enemy.name);

            // Infliggi danno al nemico
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
            }
        }

        isAttacking = false;
    }

    // Metodo per lanciare un proiettile
    void LaunchProjectile()
    {
        // Crea un'istanza del prefab nel punto di lancio
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, launchPoint.rotation);

        // Configura il proiettile come un proiettile del giocatore
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.SetLauncher(gameObject); // Imposta il lanciatore (giocatore)
        }

        // Aggiungi una forza al rigidbody del proiettile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(launchPoint.right * projectileForce, ForceMode2D.Impulse); // Lancia il proiettile
        }
    }


    private void OnDrawGizmosSelected()
    {
        // Visualizza il raggio di attacco nell'editor per il debug
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}