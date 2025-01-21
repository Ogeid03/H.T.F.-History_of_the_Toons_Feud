using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Necessario per gestire il caricamento delle scene

public class ObjectSwitcherWithDynamicJumpButton : MonoBehaviour
{
    // Prefab dell'oggetto da attivare
    public GameObject newPrefab;

    // Nome del Canvas (da impostare tramite l'Inspector)
    public string canvasName = "GUI";  // Impostato a "GUI" di default

    // Nome del JumpButton (nell'altra scena)
    public string jumpButtonName = "JumpButton";

    // Posizione in cui generare il nuovo prefab
    public Vector3 spawnPosition;

    private Canvas targetCanvas;      // Riferimento al Canvas
    private Button jumpButton;        // Riferimento al JumpButton

    void Start()
    {
        // Cerca il Canvas con il nome specificato
        targetCanvas = GameObject.Find(canvasName)?.GetComponent<Canvas>();
        if (targetCanvas == null)
        {
            Debug.LogError($"Il Canvas con il nome '{canvasName}' non è stato trovato nella scena.");
            return;
        }

        // Cerca il JumpButton (se già esistente nella gerarchia)
        AssignJumpButton();

        // Registra un listener per riassegnare il JumpButton dopo il caricamento di una nuova scena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Rimuove il listener per evitare errori quando l'oggetto viene distrutto
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void AssignJumpButton()
    {
        // Cerca l'oggetto JumpButton nella gerarchia
        GameObject jumpButtonObject = GameObject.Find(jumpButtonName);
        if (jumpButtonObject != null)
        {
            jumpButton = jumpButtonObject.GetComponent<Button>();

            if (jumpButton != null)
            {
                // Aggiungi il listener al JumpButton
                jumpButton.onClick.RemoveListener(SwitchObject); // Rimuove eventuali listener duplicati
                jumpButton.onClick.AddListener(SwitchObject);
                Debug.Log($"JumpButton trovato e configurato: {jumpButton.name}");
            }
            else
            {
                Debug.LogError($"Il GameObject '{jumpButtonName}' non ha un componente Button.");
            }
        }
        else
        {
            Debug.LogWarning($"Il bottone '{jumpButtonName}' non è stato trovato nella gerarchia attuale. Assicurati che la scena corretta sia caricata.");
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Riassegna il JumpButton dopo il caricamento della scena
        Debug.Log($"Scena '{scene.name}' caricata. Ricerca del JumpButton...");
        AssignJumpButton();
    }

    void SwitchObject()
    {
        // Disabilita l'oggetto a cui è associato questo script
        gameObject.SetActive(false);

        Debug.Log($"L'oggetto {gameObject.name} è stato disabilitato.");

        // Verifica se il prefab è stato assegnato
        if (newPrefab != null)
        {
            // Instanzia il nuovo prefab alla posizione specificata
            GameObject newObject = Instantiate(newPrefab, spawnPosition, Quaternion.identity);

            if (targetCanvas != null)
            {
                // Imposta il nuovo oggetto come figlio del Canvas
                newObject.transform.SetParent(targetCanvas.transform, false);

                Debug.Log($"Nuovo prefab creato: {newObject.name} e posizionato a {spawnPosition} nel Canvas: {targetCanvas.name}");
            }
            else
            {
                Debug.LogError("Canvas non assegnato correttamente. L'oggetto non sarà figlio di nessun Canvas.");
            }
        }
        else
        {
            Debug.LogWarning("Prefab dell'oggetto da attivare non assegnato. Nessun nuovo oggetto sarà creato.");
        }
    }
}
