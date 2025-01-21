using UnityEngine;

public class ObjectDisappearSwitcher : MonoBehaviour
{
    // Prefab da creare quando l'oggetto scompare
    public GameObject newPrefab;

    // Nome del GameObject a cui il nuovo prefab sarà aggiunto come figlio
    public string targetObjectName = "TopoPensieroView (1)";

    // Posizione in cui generare il nuovo prefab (opzionale, da settare tramite l'Inspector)
    public Vector3 spawnPosition = Vector3.zero;

    private GameObject targetObject;  // Riferimento al GameObject target

    void Start()
    {
        // Cerca il GameObject con il nome specificato
        targetObject = GameObject.Find(targetObjectName);

        // Verifica se il GameObject è stato trovato
        if (targetObject == null)
        {
            Debug.LogError($"Il GameObject con il nome '{targetObjectName}' non è stato trovato nella scena.");
        }
    }

    void Update()
    {
        // Controlla se questo GameObject è disabilitato (è scomparso)
        if (!gameObject.activeSelf)
        {
            // Se il GameObject è disabilitato, crea il nuovo prefab
            CreateNewPrefab();
        }
    }

    void CreateNewPrefab()
    {
        // Verifica se il prefab è stato assegnato e se il targetObject è valido
        if (newPrefab != null && targetObject != null)
        {
            // Instanzia il nuovo prefab alla posizione specificata
            GameObject newObject = Instantiate(newPrefab, spawnPosition, Quaternion.identity);

            // Imposta il nuovo oggetto come figlio del GameObject target
            newObject.transform.SetParent(targetObject.transform, false);

            // Log per confermare che il nuovo prefab è stato creato
            Debug.Log($"Nuovo prefab creato: {newObject.name} e posizionato nel GameObject '{targetObjectName}'");
        }
        else
        {
            // Se il prefab non è stato assegnato o il targetObject non è trovato
            if (newPrefab == null)
            {
                Debug.LogError("Prefab non assegnato. Assicurati di aver assegnato il prefab nell'Inspector.");
            }

            if (targetObject == null)
            {
                Debug.LogError($"Il GameObject '{targetObjectName}' non è stato trovato nella scena.");
            }
        }
    }
}
