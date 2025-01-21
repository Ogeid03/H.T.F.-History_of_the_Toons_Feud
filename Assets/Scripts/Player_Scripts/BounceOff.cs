using UnityEngine;
using System.Collections;  // Assicurati di includere questa direttiva per la coroutine

public class BounceOff : MonoBehaviour
{
    public float bounceForce = 10f; // Forza verso l'alto dopo il rimbalzo
    public float lateralForce = 5f; // Forza laterale

    public float stretchFactor = 0.8f; // Fattore di stretching (0.8 = 80% della scala originale)
    public float stretchDuration = 0.2f; // Durata dell'effetto di stretching

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                Vector2 normal = contact.normal;

                // Controlla se il personaggio ha colpito il nemico dall'alto
                if (normal.y > 0.5f)
                {
                    float lateralDirection = transform.position.x < collision.transform.position.x ? -1f : 1f;
                    // Applica la forza di rimbalzo verso l'alto
                    rb.velocity = new Vector2(lateralDirection * lateralForce, bounceForce);

                    // Riduci la salute del nemico
                    EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(50); // Riduce la vita del nemico di 50
                        Debug.Log("Nemico colpito dall'alto. Salute restante: " + (enemyHealth != null ? enemyHealth.GetPrefabName() : "Non trovato"));
                    }

                    StartCoroutine(StretchEnemy(collision.gameObject));

                    break; // Esci dal loop dopo aver gestito il rimbalzo e il danno
                }
            }
        }
    }

    // Coroutine per gestire l'effetto di stretching
    private IEnumerator StretchEnemy(GameObject enemy)
    {
        // Memorizza la scala originale del nemico
        Vector3 originalScale = enemy.transform.localScale;

        // Modifica la scala del nemico (riducendo l'asse Y)
        enemy.transform.localScale = new Vector3(originalScale.x, originalScale.y * stretchFactor, originalScale.z);

        // Aspetta per la durata dello stretching
        yield return new WaitForSeconds(stretchDuration);

        // Ripristina la scala originale del nemico
        enemy.transform.localScale = originalScale;
    }
}