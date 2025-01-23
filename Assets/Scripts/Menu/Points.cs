using UnityEngine;
using UnityEngine.UI;

public class EnemyScoreManager : MonoBehaviour
{
    public Text scoreText; // Riferimento al componente Text
    private int score = 0; // Valore del punteggio iniziale
    public ScoreFileManager scoreFileManager; // Riferimento al file manager

    void Start()
    {
        // Inizializza il punteggio e aggiorna il testo
        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        score += amount; // Aggiungi il punteggio specificato
        UpdateScoreText(); // Aggiorna il punteggio visualizzato
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    public void SaveScore()
    {
        if (scoreFileManager != null)
        {
            Debug.Log("Salvataggio del punteggio: " + score);
            scoreFileManager.AddScore(score); // Salva nella classifica
        }
        else
        {
            Debug.LogWarning("ScoreFileManager non trovato! Salvataggio saltato.");
        }

        // Salva il punteggio nei PlayerPrefs per il recupero nell'ultima partita
        PlayerPrefs.SetInt("LastScore", score);
        PlayerPrefs.Save(); // Salva i dati su disco
        Debug.Log("Ultimo punteggio salvato nei PlayerPrefs: " + score);
    }
}
