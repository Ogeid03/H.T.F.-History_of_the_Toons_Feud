using UnityEngine;

public class EnemyKiller : MonoBehaviour
{
    // Questo metodo viene chiamato automaticamente quando un altro collider entra in contatto
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Controlla se l'oggetto in contatto ha il tag "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Nemico colpito! Distruzione dell'oggetto: " + collision.gameObject.name);
            Destroy(collision.gameObject); // Distruggi l'oggetto nemico
        }
    }
}
