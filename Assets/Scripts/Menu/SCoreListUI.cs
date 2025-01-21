using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreListUI : MonoBehaviour
{
    public GameObject scorePrefab; // Prefab per ogni elemento della lista
    public Transform contentPanel; // Pannello contenitore dello Scroll View
    public ScoreFileManager scoreFileManager; // Riferimento al file manager

    void Start()
    {
        PopulateScoreList();
    }

    // Popola la lista dei punteggi
    public void PopulateScoreList()
    {
        // Ottieni i punteggi salvati
        List<ScoreEntry> scores = scoreFileManager.GetScores();

        // Rimuovi vecchi elementi nella lista
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        // Crea nuovi elementi per ogni punteggio
        foreach (ScoreEntry entry in scores)
        {
            GameObject newScoreItem = Instantiate(scorePrefab, contentPanel);
            Text scoreText = newScoreItem.GetComponent<Text>();
            scoreText.text = $"Punteggio: {entry.score} - Data: {entry.date}";
        }
    }
}
