using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform target;  // Riferimento al giocatore
    public int health = 100;  // Punti vita del nemico
    public float hitPauseDuration = 1f;
    public float deathAnimationDuration = 1f; // Durata dell'animazione di morte
    public float attackRadius = 3f; // Raggio d'azione per infliggere danno al giocatore
    public int damageToPlayer = 10; // Danno inflitto al giocatore

    private bool isDying = false;
    private bool isHit = false;
    private NavMeshAgent agent;
    private Animator animator; // Nuova variabile per l'animatore
    private float attackCooldown = 1.5f;
    private float lastAttackTime;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // Ottieni il componente Animator

        if (target != null)
        {
            agent.SetDestination(target.position);
        }

        lastAttackTime = -attackCooldown; // Inizializza il tempo dell'ultimo attacco
    }

    void Update()
    {
        if (target != null && !isDying && !isHit)
        {
            agent.SetDestination(target.position);

            // Controlla se il giocatore � entro il raggio d'azione per infliggere danno
            if (Vector3.Distance(transform.position, target.position) <= attackRadius)
            {
                // Controlla se il tempo passato dall'ultimo attacco � maggiore del cooldown
                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    // Infliggi danno al giocatore
                    PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(damageToPlayer); // Chiama il metodo per infliggere danno
                        Debug.Log("Giocatore colpito dal nemico. Danno inflitto: " + damageToPlayer);
                    }
                    lastAttackTime = Time.time; // Aggiorna il tempo dell'ultimo attacco
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Nemico colpito! Vita rimanente: " + health);

        if (health <= 0 && !isDying)
        {
            Debug.Log("Nemico � morto.");
            StartCoroutine(Die());
        }
        else if (!isDying)
        {
            Debug.Log("Nemico colpito ma ancora vivo.");
            StartCoroutine(HitPause());
        }
    }

    IEnumerator HitPause()
    {
        isHit = true;
        agent.isStopped = true;
        Debug.Log("Nemico fermo per un istante dopo il colpo.");
        yield return new WaitForSeconds(hitPauseDuration);
        agent.isStopped = false;
        isHit = false;
        Debug.Log("Nemico riprende a muoversi.");
    }

    IEnumerator Die()
    {
        isDying = true;
        Debug.Log("Nemico in fase di morte, disabilitando il movimento.");
        agent.isStopped = true;
        agent.enabled = false;

        // Riproduci l'animazione di morte
        animator.SetTrigger("Die"); // Assumiamo che ci sia un trigger chiamato "Die" nell'Animator

        // Attendi la fine dell'animazione di morte
        yield return new WaitForSeconds(deathAnimationDuration);

        // Distruggi il corpo del nemico dopo 1 secondo
        Debug.Log("Distruzione del nemico dopo l'animazione di morte.");
        Destroy(gameObject, 1f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
                Debug.Log("Proiettile ha colpito il nemico. Danno inflitto: " + bullet.damage);
            }

            Destroy(other.gameObject);
        }
    }
}
