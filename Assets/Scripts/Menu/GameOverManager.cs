using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverManager : MonoBehaviour
{ 
    public GameObject gameOverScreen;
    public EnemyScoreManager scoreManager; // Riferimento al gestore del punteggio

    void Start()
    {
        gameOverScreen.SetActive(false);   // Nasconde la schermata di Game Over all'inizio
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;               
        SceneManager.LoadScene("PostMortemMenu");
    }

    // Metodo pubblico per avviare la coroutine di Game Over
    public void TriggerGameOverScreen()
    {
        scoreManager.SaveScore(); // Salva il punteggio prima di visualizzare la schermata
        scoreManager.AddScore(100);
        StartCoroutine(ShowGameOverScreen());
    }

    private IEnumerator ShowGameOverScreen()
    {
        yield return new WaitForSeconds(1f);
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
