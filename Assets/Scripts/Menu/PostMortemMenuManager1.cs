using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PostMortemMenuManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Riferimento al componente Text che visualizzer� il punteggio

    void Start()
    {
        // Recupera il punteggio dell'ultima partita dai PlayerPrefs
        int lastScore = PlayerPrefs.GetInt("LastScore", 0); // 0 � il valore di default se non � presente un punteggio salvato
        scoreText.text = "LastScore: " + lastScore.ToString();
    }

    // Metodo per iniziare il gioco
    public void RePlayGame()
    {
        Debug.Log("PlayGame button pressed");
        // Carica la scena di gioco, sostituisci "GameScene" con il nome della tua scena di gioco
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    // Metodo per uscire dal gioco 
    /*public void QuitGame()
    {
        // Chiude l'applicazione
        Application.Quit();
        Debug.Log("Game Closed"); // Solo per debug, non si vedr� nell'app finale
    }*/
}
