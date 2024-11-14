using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverManager : MonoBehaviour
{ 
    public GameObject gameOverScreen;

    void Start()
    {
        gameOverScreen.SetActive(false);   // Nasconde la schermata di Game Over all'inizio
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;               
        SceneManager.LoadScene("MainMenu");
    }

    // Metodo pubblico per avviare la coroutine di Game Over
    public void TriggerGameOverScreen()
    {
        StartCoroutine(ShowGameOverScreen());
    }

    private IEnumerator ShowGameOverScreen()
    {
        yield return new WaitForSeconds(1f);
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
