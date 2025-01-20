using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Lista dei punteggi salvati
    private List<int> scores = new List<int>();

    // Riferimenti alla UI
    public Text scoreText; // Per mostrare il punteggio corrente
    public GameObject scoreListItemPrefab; // Prefab per ogni elemento della lista di punteggi
    public Transform scoreListParent; // Genitore degli oggetti della lista

    // Numero massimo di punteggi da salvare
    public int maxScores = 10;

    void Start()
    {
        // Carica i punteggi salvati quando il gioco inizia
        LoadScores();
        DisplayScores();
    }

    // Funzione per aggiungere un nuovo punteggio
    public void AddScore(int score)
    {
        // Aggiungi il punteggio alla lista
        scores.Add(score);

        // Se il numero di punteggi supera il limite, rimuovi il punteggio più basso
        if (scores.Count > maxScores)
        {
            scores.Sort(); // Ordina in ordine crescente
            scores.RemoveAt(0); // Rimuovi il punteggio più basso
        }

        // Salva i punteggi
        SaveScores();

        // Mostra i punteggi aggiornati
        DisplayScores();
    }

    // Funzione per salvare i punteggi
    void SaveScores()
    {
        for (int i = 0; i < scores.Count; i++)
        {
            PlayerPrefs.SetInt("Score" + i, scores[i]);
        }
        PlayerPrefs.SetInt("ScoreCount", scores.Count); // Salva quanti punteggi sono stati salvati
        PlayerPrefs.Save(); // Salva tutto
    }

    // Funzione per caricare i punteggi
    void LoadScores()
    {
        int count = PlayerPrefs.GetInt("ScoreCount", 0); // Carica il numero di punteggi salvati
        scores.Clear();

        for (int i = 0; i < count; i++)
        {
            scores.Add(PlayerPrefs.GetInt("Score" + i)); // Carica ogni punteggio
        }
    }

    // Funzione per mostrare i punteggi nella UI
    void DisplayScores()
    {
        // Rimuovi tutti gli elementi dalla lista
        foreach (Transform child in scoreListParent)
        {
            Destroy(child.gameObject);
        }

        // Crea un elemento per ogni punteggio
        foreach (int score in scores)
        {
            GameObject scoreItem = Instantiate(scoreListItemPrefab, scoreListParent);
            Text scoreText = scoreItem.GetComponentInChildren<Text>();
            scoreText.text = "Score: " + score;
        }
    }
}
