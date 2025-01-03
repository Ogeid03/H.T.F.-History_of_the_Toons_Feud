using UnityEngine;
using UnityEngine.SceneManagement;  // Importa il pacchetto per la gestione delle scene
using UnityEngine.UI;  // Per interagire con i componenti UI come il Button

public class SceneLoader : MonoBehaviour
{
    // Associa il bottone attraverso l'Inspector
    public Button loadSceneButton;

    // Nome della scena che desideri caricare
    public string sceneName;

    void Start()
    {
        // Verifica se il bottone è stato assegnato correttamente
        if (loadSceneButton != null)
        {
            // Aggiungi un listener al bottone che carica la scena quando viene cliccato
            loadSceneButton.onClick.AddListener(LoadScene);
        }
        else
        {
            Debug.LogError("LoadSceneButton non è stato assegnato!");
        }
    }

    // Metodo per caricare la scena
    public void LoadScene()
    {
        // Carica la scena con il nome specificato
        SceneManager.LoadScene(sceneName);
    }
}
