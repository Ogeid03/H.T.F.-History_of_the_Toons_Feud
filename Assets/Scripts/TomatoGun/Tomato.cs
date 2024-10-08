using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damageAmount = 10;         // Danno inflitto dal proiettile
    public float knockbackForce = 10f;    // Forza del knockback (puoi personalizzarlo se necessario)

    // Riferimento al lanciatore (Player o Enemy)
    private GameObject launcher;

    // Funzione per impostare il lanciatore
    public void SetLauncher(GameObject launcher)
    {
        this.launcher = launcher;
    }

    // Funzione che restituisce il valore del danno inflitto
    public int GetDamageAmount()
    {
        return damageAmount; // Restituisce la quantità di danno
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Se il proiettile colpisce il suo lanciatore, non infligge danno
        if (collision.gameObject == launcher)
        {
            return; // Ignora la collisione
        }

        // Se il proiettile colpisce un nemico, infliggi danno
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Se il proiettile è stato lanciato da un nemico, attraversa l'altro nemico
            if (launcher.CompareTag("Enemy"))
            {
                return; // Ignora la collisione con un altro nemico
            }

            // Se non è il lanciatore, infliggi danno
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount); // Infliggi danno al nemico
            }
            Destroy(gameObject); // Distruggi il proiettile dopo l'impatto
        }

        // Se il proiettile colpisce il giocatore, infliggi danno
        else if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount); // Infliggi danno al giocatore
            }
            Destroy(gameObject); // Distruggi il proiettile dopo l'impatto
        }

        // Distruggi il proiettile se colpisce altro
        Destroy(gameObject);
    }
}