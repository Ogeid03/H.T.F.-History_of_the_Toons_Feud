using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnDestroy : MonoBehaviour
{
    // Riferimento al GameObject che deve essere disabilitato quando questo GameObject viene distrutto
    public GameObject targetObject;

    // Metodo che viene chiamato quando il GameObject viene distrutto
    private void OnDestroy()
    {
        // Verifica che il targetObject non sia null
        if (targetObject != null)
        {
            // Disabilita il targetObject
            targetObject.SetActive(false);

            // Log per confermare che il targetObject è stato disabilitato
            Debug.Log($"{targetObject.name} è stato disabilitato.");
        }
        else
        {
            Debug.LogWarning("targetObject non assegnato. Impossibile disabilitarlo.");
        }
    }
}
