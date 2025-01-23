using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverScreen; // Riferimento alla schermata di Game Over
    public GameObject youWonScreen;  // Riferimento alla schermata "You Won"
    public EnemyScoreManager scoreManager; // Riferimento al gestore del punteggio

    void Start()
    {
        // Nasconde entrambe le schermate all'inizio
        if (gameOverScreen != null)
            gameOverScreen.SetActive(false);

        if (youWonScreen != null)
            youWonScreen.SetActive(false);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("PostMortemMenu");
    }

    // Metodo pubblico per avviare la coroutine di Game Over
    public void TriggerGameOverScreen()
    {
        if (scoreManager != null)
            scoreManager.SaveScore(); // Salva il punteggio prima di visualizzare la schermata

        StartCoroutine(ShowGameOverScreen());
    }

    private IEnumerator ShowGameOverScreen()
    {
        yield return new WaitForSeconds(1f);
        if (gameOverScreen != null)
            gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }

    // Metodo pubblico per avviare la coroutine "You Won"
    public void TriggerYouWonScreen()
    {
        if (scoreManager != null)
            scoreManager.SaveScore(); // Salva il punteggio anche quando il giocatore vince

        StartCoroutine(ShowYouWonScreen());
    }

    private IEnumerator ShowYouWonScreen()
    {
        yield return new WaitForSeconds(1f);
        if (youWonScreen != null)
            youWonScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
