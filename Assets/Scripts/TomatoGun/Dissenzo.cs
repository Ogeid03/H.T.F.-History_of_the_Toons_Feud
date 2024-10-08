using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject projectilePrefab;     // Prefab da lanciare
    public float launchForce = 10f;         // Forza con cui l'oggetto verrà lanciato
    public Transform launchPoint;            // Punto da cui l'oggetto verrà lanciato (posizionato davanti al nemico)

    public float attackCooldown = 2f;       // Tempo di attesa tra un attacco e l'altro
    private bool canAttack = true;           // Flag per controllare se l'attacco è possibile

    void Update()
    {
        // Controlla se è stato premuto il pulsante del mouse destro e se l'attacco è disponibile
        if (Input.GetMouseButtonDown(1) && canAttack) // 1 corrisponde al pulsante destro del mouse
        {
            LaunchProjectile();
            StartCoroutine(AttackCooldown());
        }
    }

    void LaunchProjectile()
    {
        // Crea un'istanza del prefab nel punto di lancio
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, launchPoint.rotation);

        // Aggiungi una forza al rigidbody del proiettile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Verifica l'orientamento del nemico e lancia il proiettile nella direzione corretta
            if (transform.localScale.x < 0)
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
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false; // Disabilita la possibilità di attaccare
        yield return new WaitForSeconds(attackCooldown); // Aspetta il tempo di cooldown
        canAttack = true; // Ritorna alla possibilità di attaccare
    }
}
