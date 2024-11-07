using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damageAmount = 10;         // Danno inflitto dal proiettile
    public float knockbackForce = 10f;    // Forza del knockback (puoi personalizzarlo se necessario)

    // Riferimento al lanciatore (Player o Enemy)
    private GameObject launcher;

    // Riferimento all'AudioSource
    private AudioSource audioSource;

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

    private void Start()
    {
        // Ottieni il componente AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource non trovato sul proiettile!");
        }
        // Autodistruggi il proiettile dopo 3 secondi
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Se il proiettile colpisce il suo lanciatore, non infligge danno
        if (collision.gameObject == launcher)
        {
            return; // Ignora la collisione
        }

        // Se colpisce il layer Ground, distruggi il proiettile
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            PlaySound(); // Riproduci suono prima di distruggere
            Destroy(gameObject); // Distruggi il proiettile
            return; // Esci per evitare ulteriori controlli
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
            PlaySound(); // Riproduci suono prima di distruggere
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
            PlaySound(); // Riproduci suono prima di distruggere
            Destroy(gameObject); // Distruggi il proiettile dopo l'impatto
        }

        // Se colpisce altro, riproduci suono e distruggi
        PlaySound(); // Riproduci suono prima di distruggere
        Destroy(gameObject);
    }

    // Funzione per riprodurre il suono
    private void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.enabled = true; // Assicurati che l'AudioSource sia abilitato
            audioSource.Play(); // Riproduci il suono
            Destroy(gameObject, audioSource.clip.length); // Distruggi dopo la durata del clip
        }
    }
}
