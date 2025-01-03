using UnityEngine;
using UnityEngine.UI;  // Per lavorare con i componenti UI come il Button

public class ObjectSwitcher : MonoBehaviour
{
    // Prefab dell'oggetto da attivare
    public GameObject newPrefab;

    // Il Button che attiver� la funzione
    public Button switchButton;

    // Riferimento al Canvas (se necessario per altre interazioni future)
    public Transform canvasTransform;

    // Posizione in cui generare il nuovo prefab (da settare tramite l'Inspector)
    public Vector3 spawnPosition;

    void Start()
    {
        // Verifica se il bottone � stato assegnato
        if (switchButton != null)
        {
            // Aggiungi un listener al bottone che chiama il metodo SwitchObject() quando viene cliccato
            switchButton.onClick.AddListener(SwitchObject);
        }
        else
        {
            Debug.LogError("Il bottone non � stato assegnato!");
        }

        // Trova il Canvas chiamato "GUI" se non � stato assegnato manualmente
        if (canvasTransform == null)
        {
            canvasTransform = GameObject.Find("GUI")?.transform;
        }

        if (canvasTransform == null)
        {
            Debug.LogError("Il Canvas chiamato 'GUI' non � stato trovato nella scena.");
        }
    }

    void SwitchObject()
    {
        // Disabilita l'oggetto a cui � associato questo script
        gameObject.SetActive(false);

        // Log per confermare che l'oggetto � stato disabilitato
        Debug.Log($"L'oggetto {gameObject.name} � stato disabilitato.");

        // Attiva il nuovo prefab se � stato assegnato
        if (newPrefab != null)
        {
            // Instanzia il nuovo prefab alla posizione specificata da spawnPosition
            GameObject newObject = Instantiate(newPrefab, spawnPosition, Quaternion.identity);

            // Imposta il nuovo oggetto come figlio del Canvas
            if (canvasTransform != null)
            {
                newObject.transform.SetParent(canvasTransform, false);

                // Log per confermare che il nuovo prefab � stato creato
                Debug.Log($"Nuovo prefab creato: {newObject.name} e posizionato a {spawnPosition}");

                // Verifica che l'oggetto abbia un Renderer abilitato
                if (newObject.GetComponent<Renderer>() != null && newObject.GetComponent<Renderer>().enabled)
                {
                    Debug.Log("Il nuovo oggetto � visibile.");
                }
                else
                {
                    Debug.LogWarning("Il nuovo oggetto non ha un Renderer abilitato!");
                }
            }
            else
            {
                Debug.LogError("Canvas non assegnato correttamente. L'oggetto non sar� figlio di nessun Canvas.");
            }
        }
        /*else
        {
            Debug.LogError("Prefab dell'oggetto da attivare non assegnato!");
        }*/
    }
}
