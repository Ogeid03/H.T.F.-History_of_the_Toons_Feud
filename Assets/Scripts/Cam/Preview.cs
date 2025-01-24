using UnityEngine;

public class MoveToTargetXWithControl : MonoBehaviour
{
    public string targetObjectName = "Target(Clone)"; // Nome (o parte del nome) dell'oggetto target
    public float moveSpeed = 2f;                      // Velocità di movimento

    public string[] prefabTags = { "Enemy", "Ally", "Obstacle" }; // Tag dei prefab da disabilitare
    public Camera mainCamera;                        // La telecamera principale attuale
    public Camera targetCamera;                      // La telecamera che prenderà il controllo alla fine

    private Transform target;                        // Riferimento alla posizione del target
    private bool isMoving = false;                   // Indica se il movimento è attivo

    void Start()
    {
        // Trova dinamicamente l'oggetto target nella scena
        GameObject targetObject = FindTargetObject();
        if (targetObject != null)
        {
            target = targetObject.transform;

            // Disabilita il movimento dei prefab specificati ed avvia il movimento
            DisablePrefabMovement();
            isMoving = true;

            // Assicurati che solo la mainCamera sia attiva all'inizio
            if (mainCamera != null) mainCamera.enabled = true;
            if (targetCamera != null) targetCamera.enabled = false;
        }
        else
        {
            Debug.LogError($"Oggetto con nome '{targetObjectName}' non trovato nella scena!");
        }
    }

    void Update()
    {
        // Se il target è stato trovato e il movimento è attivo
        if (isMoving && target != null)
        {
            MoveTowardsTargetX();
        }
    }

    // Funzione per trovare dinamicamente l'oggetto target
    GameObject FindTargetObject()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name.Contains(targetObjectName)) // Controlla se il nome contiene il nome specificato
            {
                return obj; // Restituisce il primo oggetto trovato
            }
        }
        return null; // Nessun oggetto trovato
    }

    // Funzione per muovere l'oggetto solo sull'asse X
    void MoveTowardsTargetX()
    {
        // Calcola la posizione target limitata all'asse X
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = new Vector3(target.position.x, currentPosition.y, currentPosition.z);

        // Sposta l'oggetto gradualmente verso la posizione target
        transform.position = Vector3.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.deltaTime);

        // Debug per verificare il movimento
        Debug.Log($"Posizione corrente: {transform.position.x}, Target: {targetPosition.x}");

        // Controlla se l'oggetto ha raggiunto il target
        if (Mathf.Approximately(transform.position.x, target.position.x))
        {
            isMoving = false; // Ferma il movimento
            EnablePrefabMovement(); // Riabilita il movimento dei prefab
            SwitchToTargetCamera(); // Cambia telecamera
            Debug.Log("Raggiunto il target! Movimento dei prefab riabilitato e visuale passata.");
        }
    }

    // Funzione per disabilitare il movimento dei prefab specificati
    void DisablePrefabMovement()
    {
        foreach (string tag in prefabTags)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in objects)
            {
                // Disabilita gli script di movimento (se presenti)
                MonoBehaviour[] scripts = obj.GetComponents<MonoBehaviour>();
                foreach (MonoBehaviour script in scripts)
                {
                    script.enabled = false; // Disabilita lo script
                }
            }
        }
        Debug.Log("Movimento prefab disabilitato.");
    }

    // Funzione per riabilitare il movimento dei prefab specificati
    void EnablePrefabMovement()
    {
        foreach (string tag in prefabTags)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in objects)
            {
                // Riabilita gli script di movimento (se presenti)
                MonoBehaviour[] scripts = obj.GetComponents<MonoBehaviour>();
                foreach (MonoBehaviour script in scripts)
                {
                    script.enabled = true; // Riabilita lo script
                }
            }
        }
        Debug.Log("Movimento prefab riabilitato.");
    }

    // Funzione per cambiare telecamera
    void SwitchToTargetCamera()
    {
        if (mainCamera != null) mainCamera.enabled = false; // Disabilita la telecamera principale
        if (targetCamera != null) targetCamera.enabled = true; // Abilita la telecamera target
    }
}
