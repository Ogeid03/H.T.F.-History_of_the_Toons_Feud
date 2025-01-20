using UnityEngine;
using UnityEngine.UI; // Usa questo se stai usando il sistema di UI di Unity
// using TMPro; // Usa questo se stai usando TextMeshPro

public class EnemyScoreManager : MonoBehaviour
{
    public Text scoreText; // Riferimento al componente Text (o TextMeshPro)
    // public TextMeshProUGUI scoreText; // Se stai usando TextMeshPro
    private int score = 0; // Valore del punteggio iniziale

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
        // Se usi TextMeshPro, usa:
        // scoreText.text = "Punteggio: " + score.ToString();
    }

    public void SaveScore()
    {
        // Salva il punteggio dell'ultima partita nei PlayerPrefs
        PlayerPrefs.SetInt("LastScore", score);
        PlayerPrefs.Save();
    }
}

