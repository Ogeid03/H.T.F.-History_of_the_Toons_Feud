using UnityEngine;

public class PrefabVisibilitySwitcher : MonoBehaviour
{
    // Prefab da rendere visibile quando questo GameObject scompare
    public GameObject targetPrefab;

    // Indica se il prefab è stato già reso visibile
    private bool isPrefabVisible = false;

    void Start()
    {
        // Verifica se il prefab è stato assegnato
        if (targetPrefab != null)
        {
            // Nasconde il prefab allo start
            targetPrefab.SetActive(false);
            Debug.Log($"Prefab '{targetPrefab.name}' è stato inizialmente nascosto.");
        }
        else
        {
            Debug.LogError("Prefab non assegnato! Assicurati di trascinare un GameObject nel campo 'Target Prefab' nell'Inspector.");
        }
    }

    void Update()
    {
        // Controlla se il GameObject è disabilitato e il prefab non è ancora visibile
        if (!gameObject.activeSelf && !isPrefabVisible)
        {
            // Rende visibile il prefab
            ShowTargetPrefab();
        }
    }

    void ShowTargetPrefab()
    {
        // Verifica se il prefab è stato assegnato
        if (targetPrefab != null)
        {
            // Assicurati che il prefab esista e sia disattivato prima di attivarlo
            if (!targetPrefab.activeSelf)
            {
                targetPrefab.SetActive(true); // Rendi il prefab visibile
                Debug.Log($"Prefab '{targetPrefab.name}' è stato reso visibile.");
            }

            // Segna che il prefab è stato reso visibile per evitare chiamate multiple
            isPrefabVisible = true;
        }
        else
        {
            Debug.LogError("Il prefab non è assegnato o non esiste nella scena. Assicurati di averlo configurato correttamente.");
        }
    }
}
