using UnityEngine;

public class PrefabVisibilitySwitcher : MonoBehaviour
{
    // Prefab da rendere visibile quando questo GameObject scompare
    public GameObject targetPrefab;

    // Indica se il prefab � stato gi� reso visibile
    private bool isPrefabVisible = false;

    void Start()
    {
        // Verifica se il prefab � stato assegnato
        if (targetPrefab != null)
        {
            // Nasconde il prefab allo start
            targetPrefab.SetActive(false);
            Debug.Log($"Prefab '{targetPrefab.name}' � stato inizialmente nascosto.");
        }
        else
        {
            Debug.LogError("Prefab non assegnato! Assicurati di trascinare un GameObject nel campo 'Target Prefab' nell'Inspector.");
        }
    }

    void Update()
    {
        // Controlla se il GameObject � disabilitato e il prefab non � ancora visibile
        if (!gameObject.activeSelf && !isPrefabVisible)
        {
            // Rende visibile il prefab
            ShowTargetPrefab();
        }
    }

    void ShowTargetPrefab()
    {
        // Verifica se il prefab � stato assegnato
        if (targetPrefab != null)
        {
            // Assicurati che il prefab esista e sia disattivato prima di attivarlo
            if (!targetPrefab.activeSelf)
            {
                targetPrefab.SetActive(true); // Rendi il prefab visibile
                Debug.Log($"Prefab '{targetPrefab.name}' � stato reso visibile.");
            }

            // Segna che il prefab � stato reso visibile per evitare chiamate multiple
            isPrefabVisible = true;
        }
        else
        {
            Debug.LogError("Il prefab non � assegnato o non esiste nella scena. Assicurati di averlo configurato correttamente.");
        }
    }
}
