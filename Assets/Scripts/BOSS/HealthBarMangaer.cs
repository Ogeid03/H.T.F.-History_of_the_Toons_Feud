using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    public BossHealth bossHealth;    // Riferimento alla salute del boss
    public Transform healthBar;      // Transform dell'oggetto della barra della salute (un oggetto 2D)

    void Start()
    {
        // Verifica che il riferimento alla salute del boss sia assegnato
        if (bossHealth == null)
        {
            Debug.LogError("BossHealth non è stato assegnato!");
            return;
        }

        // Inizializza la barra della salute con una scala iniziale di 3.2 sull'asse X
        healthBar.localScale = new Vector3(3.2f, healthBar.localScale.y, healthBar.localScale.z);

        // Inizializza la barra della salute
        UpdateHealthBar();
    }

    void Update()
    {
        // Aggiorna la barra della salute ogni frame in base alla salute del boss
        UpdateHealthBar();
    }

    // Metodo per aggiornare la barra della salute
    private void UpdateHealthBar()
    {
        // Ottieni la salute attuale del boss
        float currentHealth = bossHealth.GetCurrentHealth();

        // Calcola la percentuale di vita
        float healthPercentage = currentHealth / bossHealth.maxHealth;

        // Aggiorna la larghezza della barra della salute in base alla percentuale
        healthBar.localScale = new Vector3(healthPercentage * 3.2f, healthBar.localScale.y, healthBar.localScale.z);

        // Nascondi la barra se la vita è 0
        if (healthPercentage <= 0)
        {
            healthBar.gameObject.SetActive(false);  // Nasconde la barra della salute (opzionale)
        }
        else
        {
            healthBar.gameObject.SetActive(true);   // Mostra la barra della salute
        }
    }
}
