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
            if (launcher != null && launcher.CompareTag("Enemy"))
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

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Proiettile colpisce il giocatore!");

            // Ottieni il componente PlayerHealth sul giocatore
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount); // Infliggi danno al giocatore
                Debug.Log("Danno inflitto al giocatore!");
            }
            else
            {
                Debug.LogWarning("PlayerHealth non trovato!");
            }

            // Riproduci il suono
            PlaySound();

            // Distruggi il proiettile dopo la collisione
            Destroy(gameObject);
        }

        // Per qualsiasi altro tipo di oggetto colpito, riproduci suono e distruggi
        PlaySound();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
{
    // Verifica se il launcher è stato inizializzato prima di usarlo
    if (launcher == null)
    {
        Debug.LogWarning("Launcher non è stato impostato!");
        return;
    }

    // Se il proiettile entra in contatto con il giocatore, infliggi danno
    if (other.CompareTag("Player"))
    {
        Debug.Log("Proiettile colpisce il giocatore!");

        // Ottieni il componente PlayerHealth sul giocatore
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount); // Infliggi danno al giocatore
            Debug.Log("Danno inflitto al giocatore!");
        }
        else
        {
            Debug.LogWarning("PlayerHealth non trovato!");
        }

        // Riproduci il suono
        PlaySound();

        // Distruggi il proiettile dopo la collisione
        Destroy(gameObject);
    }

    // Se il proiettile entra in contatto con un nemico, infliggi danno
    if (other.CompareTag("Enemy"))
    {
        // Evita che il proiettile danneggi un nemico se è stato lanciato da un altro nemico
        if (launcher.CompareTag("Enemy") || launcher == null)
        {
            return; // Ignora se è lo stesso team
        }

        // Infliggi danno al nemico
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damageAmount); // Infliggi danno
        }

        PlaySound(); // Riproduci suono prima di distruggere
        Destroy(gameObject); // Distruggi il proiettile
    }
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
        else
        {
            Debug.LogWarning("AudioSource non è stato trovato, suono non riprodotto.");
        }
    }
}
