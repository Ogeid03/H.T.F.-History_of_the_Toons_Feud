using UnityEngine;

public class ObjectDisappearSwitcher : MonoBehaviour
{
    // Prefab da creare quando l'oggetto scompare
    public GameObject newPrefab;

    // Nome del GameObject a cui il nuovo prefab sar� aggiunto come figlio
    public string targetObjectName = "TopoPensieroView (1)";

    // Posizione in cui generare il nuovo prefab (opzionale, da settare tramite l'Inspector)
    public Vector3 spawnPosition = Vector3.zero;

    private GameObject targetObject;  // Riferimento al GameObject target

    void Start()
    {
        // Cerca il GameObject con il nome specificato
        targetObject = GameObject.Find(targetObjectName);

        // Verifica se il GameObject � stato trovato
        if (targetObject == null)
        {
            Debug.LogError($"Il GameObject con il nome '{targetObjectName}' non � stato trovato nella scena.");
        }
    }

    void Update()
    {
        // Controlla se questo GameObject � disabilitato (� scomparso)
        if (!gameObject.activeSelf)
        {
            // Se il GameObject � disabilitato, crea il nuovo prefab
            CreateNewPrefab();
        }
    }

    void CreateNewPrefab()
    {
        // Verifica se il prefab � stato assegnato e se il targetObject � valido
        if (newPrefab != null && targetObject != null)
        {
            // Instanzia il nuovo prefab alla posizione specificata
            GameObject newObject = Instantiate(newPrefab, spawnPosition, Quaternion.identity);

            // Imposta il nuovo oggetto come figlio del GameObject target
            newObject.transform.SetParent(targetObject.transform, false);

            // Log per confermare che il nuovo prefab � stato creato
            Debug.Log($"Nuovo prefab creato: {newObject.name} e posizionato nel GameObject '{targetObjectName}'");
        }
        else
        {
            // Se il prefab non � stato assegnato o il targetObject non � trovato
            if (newPrefab == null)
            {
                Debug.LogError("Prefab non assegnato. Assicurati di aver assegnato il prefab nell'Inspector.");
            }

            if (targetObject == null)
            {
                Debug.LogError($"Il GameObject '{targetObjectName}' non � stato trovato nella scena.");
            }
        }
    }
}
