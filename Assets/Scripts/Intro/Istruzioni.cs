using UnityEngine;
using UnityEngine.UI;  // Per lavorare con i componenti UI come il Button

public class ObjectSwitcher : MonoBehaviour
{
    // Prefab dell'oggetto da attivare
    public GameObject newPrefab;

    // Il Button che attiverà la funzione
    public Button switchButton;

    // Canvas scelto dall'Inspector
    public Canvas targetCanvas;

    // Posizione in cui generare il nuovo prefab (da settare tramite l'Inspector)
    public Vector3 spawnPosition;

    void Start()
    {
        // Verifica se il bottone è stato assegnato
        if (switchButton != null)
        {
            // Aggiungi un listener al bottone che chiama il metodo SwitchObject() quando viene cliccato
            switchButton.onClick.AddListener(SwitchObject);
        }
        else
        {
            Debug.LogError("Il bottone non è stato assegnato!");
        }

        // Se il Canvas non è stato assegnato, cerca uno con il nome "GUI"
        if (targetCanvas == null)
        {
            targetCanvas = GameObject.Find("GUI")?.GetComponent<Canvas>();
        }

        // Se ancora non è stato trovato, mostra un errore
        if (targetCanvas == null)
        {
            Debug.LogError("Il Canvas non è stato assegnato e non è stato trovato un Canvas chiamato 'GUI'.");
        }
        else
        {
            Debug.Log($"Canvas utilizzato: {targetCanvas.name}");
        }
    }

    void SwitchObject()
    {
        // Disabilita l'oggetto a cui è associato questo script
        gameObject.SetActive(false);

        // Log per confermare che l'oggetto è stato disabilitato
        Debug.Log($"L'oggetto {gameObject.name} è stato disabilitato.");

        // Attiva il nuovo prefab se è stato assegnato
        if (newPrefab != null)
        {
            // Instanzia il nuovo prefab alla posizione specificata da spawnPosition
            GameObject newObject = Instantiate(newPrefab, spawnPosition, Quaternion.identity);

            // Imposta il nuovo oggetto come figlio del Canvas
            if (targetCanvas != null)
            {
                newObject.transform.SetParent(targetCanvas.transform, false);

                // Log per confermare che il nuovo prefab è stato creato
                Debug.Log($"Nuovo prefab creato: {newObject.name} e posizionato a {spawnPosition}");

                // Verifica che l'oggetto abbia un Renderer abilitato
                if (newObject.GetComponent<Renderer>() != null && newObject.GetComponent<Renderer>().enabled)
                {
                    Debug.Log("Il nuovo oggetto è visibile.");
                }
                else
                {
                    Debug.LogWarning("Il nuovo oggetto non ha un Renderer abilitato!");
                }
            }
            else
            {
                Debug.LogError("Canvas non assegnato correttamente. L'oggetto non sarà figlio di nessun Canvas.");
            }
        }
        /*else
        {
            Debug.LogError("Prefab dell'oggetto da attivare non assegnato!");
        }*/
    }
}
