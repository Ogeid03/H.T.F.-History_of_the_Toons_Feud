using UnityEngine;
using UnityEngine.UI;  // Per lavorare con i componenti UI come il Button

public class ObjectSwitcher : MonoBehaviour
{
    // Prefab dell'oggetto da attivare
    public GameObject newPrefab;

    // Il Button che attiverà la funzione
    public Button switchButton;

    // Nome del Canvas (da impostare tramite l'Inspector)
    public string canvasName = "GUI";  // Impostato a "GUI" di default

    // Posizione in cui generare il nuovo prefab (da settare tramite l'Inspector)
    public Vector3 spawnPosition;

    private Canvas targetCanvas;  // Riferimento al Canvas

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
            Debug.LogWarning("Il bottone non è stato assegnato!");
        }

        // Cerca il Canvas con il nome specificato nell'Inspector
        targetCanvas = GameObject.Find(canvasName)?.GetComponent<Canvas>();

        // Se il Canvas non è stato trovato, mostra un errore
        if (targetCanvas == null)
        {
            Debug.LogError($"Il Canvas con il nome '{canvasName}' non è stato trovato nella scena.");
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

        // Verifica se il prefab è stato assegnato
        if (newPrefab != null)
        {
            // Instanzia il nuovo prefab alla posizione specificata da spawnPosition
            GameObject newObject = Instantiate(newPrefab, spawnPosition, Quaternion.identity);

            // Verifica che targetCanvas sia valido e che il nuovo prefab venga creato nel Canvas corretto
            if (targetCanvas != null)
            {
                // Imposta il nuovo oggetto come figlio del Canvas trovato
                newObject.transform.SetParent(targetCanvas.transform, false);

                // Log per confermare che il nuovo prefab è stato creato
                Debug.Log($"Nuovo prefab creato: {newObject.name} e posizionato a {spawnPosition} nel Canvas: {targetCanvas.name}");

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
        else
        {
            // Invece di mostrare un errore, facciamo solo un log informativo
            Debug.LogWarning("Prefab dell'oggetto da attivare non assegnato. Nessun nuovo oggetto sarà creato.");
        }
    }
}
