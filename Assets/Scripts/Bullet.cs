using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20f;    // Velocità del proiettile
    public Rigidbody rb;               // Il Rigidbody del proiettile
    public float destroyTime = 3f;     // Tempo prima che il proiettile venga distrutto
    public int damage = 100; // Danno che infligge il proiettile
    public float voidThreshold = -10f; // Y sotto la quale il proiettile viene considerato nel vuoto

    void Start()
    {
        // Applica velocità al proiettile
        rb.velocity = transform.forward * bulletSpeed;

        // Distruggi il proiettile dopo 3 secondi
        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        // Controlla se il proiettile è sotto una certa altezza (nel vuoto)
        if (transform.position.y < voidThreshold)
        {
            Destroy(gameObject);  // Distrugge il proiettile se è finito nel vuoto
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            // Il nemico viene colpito, applica danno
            enemy.TakeDamage(damage);
        }

        // Distruggi il proiettile al contatto con qualsiasi oggetto
        Destroy(gameObject);
    }
}
