using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void ReturnToMenu()
    {
        Time.timeScale = 1;               // Ripristina il tempo di gioco
        SceneManager.LoadScene("MainMenu");  // Carica la scena del menu principale
    }
}
