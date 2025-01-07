using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Metodo per iniziare il gioco
    public void PlayGame()
    {
        Debug.Log("PlayGame button pressed");
        // Carica la scena di gioco, sostituisci "GameScene" con il nome della tua scena di gioco
        SceneManager.LoadScene("IntroScene", LoadSceneMode.Single);
    }

    // Metodo per uscire dal gioco 
    /*public void QuitGame()
    {
        // Chiude l'applicazione
        Application.Quit();
        Debug.Log("Game Closed"); // Solo per debug, non si vedrà nell'app finale
    }*/
}
