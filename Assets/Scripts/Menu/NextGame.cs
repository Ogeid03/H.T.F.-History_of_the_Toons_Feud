using UnityEngine;
using UnityEngine.SceneManagement; // Importa il namespace per gestire le scene

public class Button3D : MonoBehaviour
{
    // Funzione chiamata quando l'oggetto viene cliccato con il mouse
    private void OnMouseDown()
    {
        LoadNewScene();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Converte la posizione del tocco in un raggio
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                // Controlla se il raggio colpisce questo oggetto
                if (Physics.Raycast(ray, out hit) && hit.transform == transform)
                {
                    LoadNewScene(); // Avvia la nuova scena
                }
            }
        }
    }

    // Funzione per caricare la nuova scena
    private void LoadNewScene()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
