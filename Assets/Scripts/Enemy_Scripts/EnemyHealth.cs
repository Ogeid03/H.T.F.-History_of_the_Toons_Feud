using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;              // Salute massima del nemico
    private int currentHealth;              // Salute attuale del nemico

    public int scoreValue = 10;
    private EnemyScoreManager scoreManager; // Riferimento al gestore del punteggio

    private Transform pomodoroSpiaccicato;  // Riferimento al figlio "pomodoro_spappolato"
    private bool isPomodoroVisible = false; // Verifica se il pomodoro è già visibile

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
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy health: " + currentHealth);

        // Se è presente il pomodoro e non è già stato reso visibile
        if (pomodoroSpiaccicato != null && !isPomodoroVisible)
        {
            pomodoroSpiaccicato.gameObject.SetActive(true);  // Rendi visibile il pomodoro quando il nemico subisce danno
            isPomodoroVisible = true;  // Segna il pomodoro come visibile
        }

        if (currentHealth <= 0)
        {
            Die();  // Chiama la funzione per gestire la morte del nemico
        }
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
