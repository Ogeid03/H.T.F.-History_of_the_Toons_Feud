using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public int damageAmount = 15;       // Quantità di danno inflitto al player
    public float lifetime = 100f;       // Tempo di vita del proiettile in secondi
    public LayerMask groundLayer;       // Layer del terreno con cui il proiettile si autodistrugge in caso di collisione

    private float timeAlive = 0f;       // Timer per tenere traccia della durata del proiettile

    // Questo metodo viene chiamato quando il proiettile entra in collisione con un altro oggetto
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se l'oggetto con cui si è entrato in collisione ha il tag "Player"
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);  // Infligge danno al player
                Debug.Log("Il proiettile ha inflitto 15 danni al giocatore!");
            }

            Destroy(gameObject);  // Distrugge il proiettile dopo aver inflitto danno
        }
        // Verifica se l'oggetto con cui si è entrato in collisione è nel layer "Ground"
        /*else if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            Debug.Log("Il proiettile ha colpito il terreno e si autodistrugge.");
            Destroy(gameObject);  // Distrugge il proiettile
        }*/
    }

    private void Update()
    {
        // Incrementa il timer del proiettile
        timeAlive += Time.deltaTime;

        // Verifica se il proiettile ha superato il tempo di vita e, in caso, autodistrugge il proiettile
        if (timeAlive > lifetime)
        {
            Debug.Log("Il proiettile ha superato il tempo di vita e si autodistrugge.");
            Destroy(gameObject);
        }
    }
}
