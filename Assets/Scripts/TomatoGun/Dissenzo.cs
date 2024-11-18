using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Aggiungi questo per usare l'interfaccia utente

public class EnemyAttack : MonoBehaviour
{
    public GameObject projectilePrefab;     // Prefab da lanciare
    public float launchForce = 10f;         // Forza con cui l'oggetto verrà lanciato
    public Transform launchPoint;           // Punto da cui l'oggetto verrà lanciato (posizionato davanti al nemico)

    public float attackCooldown = 2f;       // Tempo di attesa tra un attacco e l'altro
    private bool canAttack = true;          // Flag per controllare se l'attacco è possibile
    private bool jumped = false;
    public Button attackButton;             // Riferimento al pulsante UI
    public Button jumpButton;

    void Start()
    {
        // Associa l'evento del pulsante al metodo LaunchProjectile
        if (attackButton != null)
        {
            attackButton.onClick.AddListener(OnAttackButtonClicked);
        }

        if (jumpButton != null)
        {
            jumpButton.onClick.AddListener(OnjumpButtonClicked);
            
        }
    }

    void Update()
    {
        // Puoi ancora controllare il lancio con il mouse, se lo desideri
        if (Input.GetMouseButtonDown(1) && canAttack && !jumped)
        {
            LaunchProjectile();
            StartCoroutine(AttackCooldown());
        }
    }

    public void LaunchProjectile()
    {
        if (!canAttack) return; // Previene il lancio durante il cooldown

        // Crea un'istanza del prefab nel punto di lancio
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, launchPoint.rotation);

        // Imposta il proiettile come un proiettile nemico
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.SetLauncher(gameObject); // Imposta il lanciatore (nemico)
        }

        // Aggiungi una forza al rigidbody del proiettile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Verifica l'orientamento del nemico e lancia il proiettile nella direzione corretta
            if (transform.localScale.x > 0)
            {
                // Se il nemico è rivolto a sinistra
                rb.AddForce(-launchPoint.right * launchForce, ForceMode2D.Impulse);
            }
            else
            {
                // Se il nemico è rivolto a destra
                rb.AddForce(launchPoint.right * launchForce, ForceMode2D.Impulse);
            }
        }

        StartCoroutine(AttackCooldown()); // Inizia il cooldown dopo il lancio
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false; // Disabilita la possibilità di attaccare
        yield return new WaitForSeconds(attackCooldown); // Aspetta il tempo di cooldown
        canAttack = true; // Ritorna alla possibilità di attaccare
    }

    // Metodo chiamato quando il pulsante è premuto
    private void OnAttackButtonClicked()
    {
        LaunchProjectile(); // Chiama il metodo per lanciare il proiettile
    }

    private void OnjumpButtonClicked()
    {
        jumped = true;
    }
}