using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;              // Salute massima del nemico
    private int currentHealth;              // Salute attuale del nemico

    public int scoreValue = 10;
    private EnemyScoreManager scoreManager; // Riferimento al gestore del punteggio

    private Transform pomodoroSpiaccicato;  // Riferimento al figlio "pomodoro_spappolato"
    private bool isPomodoroVisible = false; // Verifica se il pomodoro è già visibile

    private Respawn respawnScript;          // Riferimento allo script Respawn

    // Variabili per il suono
    public AudioClip damageSound;           // Suono da riprodurre quando il nemico subisce danno
    private AudioSource customAudioSource;  // AudioSource che verrà recuperato dal GameObject

    void Start()
    {
        currentHealth = maxHealth; // Inizializza la salute corrente a quella massima
        scoreManager = FindObjectOfType<EnemyScoreManager>(); // Trova il gestore del punteggio

        // Trova "pomodoro_spappolato" direttamente come figlio del GameObject corrente
        pomodoroSpiaccicato = transform.Find("pomodoro_spappolato");

        // Se esiste, lo nascondi all'inizio
        if (pomodoroSpiaccicato != null)
        {
            pomodoroSpiaccicato.gameObject.SetActive(false); // Nascondi il pomodoro all'inizio
        }
        else
        {
            Debug.LogWarning("pomodoro_spappolato non trovato come figlio del GameObject con EnemyHealth!");
        }

        // Cerca automaticamente lo script Respawn sul GameObject "G:O:D"
        GameObject respawnObject = GameObject.Find("G:O:D");
        if (respawnObject != null)
        {
            respawnScript = respawnObject.GetComponent<Respawn>();
            if (respawnScript == null)
            {
                Debug.LogError("Lo script Respawn non è stato trovato su G:O:D.");
            }
        }
        else
        {
            Debug.LogError("Non è stato trovato un oggetto con il nome G:O:D nella scena.");
        }

        // Cerca il GameObject con il nome "DamageSound" e recupera il componente AudioSource
        GameObject audioSourceObject = GameObject.Find("DamageSound");
        if (audioSourceObject != null)
        {
            customAudioSource = audioSourceObject.GetComponent<AudioSource>();
            if (customAudioSource == null)
            {
                Debug.LogError("Il GameObject 'DamageSound' non contiene un componente AudioSource!");
            }
        }
        else
        {
            Debug.LogError("Non è stato trovato un oggetto chiamato 'DamageSound' nella scena.");
        }
    }

    public void TakeDamage(int damage, bool isProjectile)
    {
        currentHealth -= damage;
        Debug.Log("Enemy health: " + currentHealth);

        // Riproduci il suono del danno usando l'AudioSource assegnato
        if (customAudioSource != null && damageSound != null)
        {
            customAudioSource.PlayOneShot(damageSound);
        }

        // Se è presente il pomodoro e il danno è causato da un proiettile, lo rendi visibile
        if (isProjectile && pomodoroSpiaccicato != null && !isPomodoroVisible)
        {
            pomodoroSpiaccicato.gameObject.SetActive(true);  // Rendi visibile il pomodoro
            isPomodoroVisible = true;  // Segna il pomodoro come visibile
        }

        if (currentHealth <= 0)
        {
            // Verifica se il componente "EnemyLauncherAI" è presente
            EnemyLauncherAI launcherAI = GetComponent<EnemyLauncherAI>();
            if (launcherAI != null && respawnScript != null)
            {
                // Passa la posizione e la rotazione dell'oggetto che sta per morire (il nemico) come parametro
                respawnScript.RespawnPrefab(transform.position, transform.rotation);
            }

            Die();  // Chiama la funzione per gestire la morte del nemico
        }
    }

    // Metodo originale per compatibilità
    public void TakeDamage(int damage)
    {
        TakeDamage(damage, false); // Assume che il danno non sia da proiettile
    }

    public string GetPrefabName()
    {
        return gameObject.name; // Restituisce il nome dell'oggetto
    }

    private void Die()
    {
        // Logica per la morte del nemico (es. distruzione, animazione, ecc.)
        Debug.Log("Enemy: " + GetPrefabName() + " died!");

        if (scoreManager != null)
        {
            scoreManager.AddScore(scoreValue);
        }

        // Riproduci il suono della morte usando l'AudioSource assegnato
        if (customAudioSource != null && damageSound != null)
        {
            customAudioSource.PlayOneShot(damageSound);
        }

        // Inizia la coroutine per ritardare la distruzione di 0.5 secondi
        StartCoroutine(DelayedDeath());
    }

    private System.Collections.IEnumerator DelayedDeath()
    {
        // Attendi 0.5 secondi prima di distruggere il nemico
        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);  // Distruggi l'oggetto nemico
    }
}
