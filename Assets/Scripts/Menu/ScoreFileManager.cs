using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class ScoreEntry
{
    public int score; // Il punteggio
    public string date; // La data
}

[System.Serializable]
public class ScoreData
{
    public List<ScoreEntry> scores = new List<ScoreEntry>(); // Lista dei punteggi
}

public class ScoreFileManager : MonoBehaviour
{
    private string filePath; // Percorso del file JSON
    private ScoreData scoreData = new ScoreData(); // Dati dei punteggi

    void Awake()
    {
        // Percorso del file JSON
        filePath = Path.Combine(Application.persistentDataPath, "scores.json");

        // Carica i punteggi esistenti
        LoadScores();
    }

    // Aggiungi un nuovo punteggio e salva
    public void AddScore(int newScore)
    {
        ScoreEntry newEntry = new ScoreEntry
        {
            score = newScore,
            date = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm")
        };

        scoreData.scores.Add(newEntry);
        SaveScores();
    }

    // Restituisci la lista dei punteggi
    public List<ScoreEntry> GetScores()
    {
        return scoreData.scores;
    }

    // Salva i dati nel file JSON
    private void SaveScores()
    {
        string json = JsonUtility.ToJson(scoreData, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Punteggi salvati in: " + filePath);
    }

    // Carica i dati dal file JSON
    private void LoadScores()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            scoreData = JsonUtility.FromJson<ScoreData>(json);
            Debug.Log("Punteggi caricati dal file JSON.");
        }
        else
        {
            Debug.Log("Nessun file trovato, inizializzo un nuovo file.");
        }
    }
}
