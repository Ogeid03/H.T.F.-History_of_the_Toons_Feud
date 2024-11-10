using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public int damageAmount = 15;  // Quantità di danno inflitto al player

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

            // Distrugge il proiettile dopo aver inflitto danno
            Destroy(gameObject);
        }
    }
}
