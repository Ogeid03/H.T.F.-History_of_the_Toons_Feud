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
            scoreFileManager.AddScore(score);
        }
        else
        {
            Debug.LogWarning("ScoreFileManager non trovato!");
        }
    }
}
