using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabList; // Lista dei prefab da generare
    [SerializeField] private int numberOfSpawns = 5; // Numero di prefab da generare
    [SerializeField] private float spacing = 23.31f; // Spaziatura tra i prefab
    [SerializeField] private Transform initialSpawnPoint; // Punto di riferimento per lo spawn del primo prefab

    void Start()
    {
        GeneratePrefabs();
    }

    void GeneratePrefabs()
    {
        // Usa la posizione di initialSpawnPoint come punto di partenza
        Vector3 lastPosition = initialSpawnPoint != null ? initialSpawnPoint.position : Vector3.zero;

        for (int i = 0; i < numberOfSpawns; i++)
        {
            // Scegli un prefab casuale dalla lista
            GameObject prefabToSpawn = prefabList[Random.Range(0, prefabList.Count)];

            // Crea un'istanza del prefab
            GameObject spawnedPrefab = Instantiate(prefabToSpawn, lastPosition, Quaternion.identity);

            // Cerca il figlio LinkToOthers nel prefab appena generato
            Transform linkToOthers = spawnedPrefab.transform.Find("LinkToOthers");

            if (linkToOthers != null)
            {
                // Posiziona il prossimo prefab a 23.31 unità a destra, mantenendo le coordinate y e z
                lastPosition = new Vector3(linkToOthers.position.x + spacing, linkToOthers.position.y, linkToOthers.position.z);
            }
            else
            {
                Debug.LogWarning("Il figlio 'LinkToOthers' non è stato trovato nel prefab " + spawnedPrefab.name);
                // Se non troviamo LinkToOthers, manteniamo l'ultima posizione
                lastPosition += new Vector3(spacing, 0, 0);
            }

            // Imposta il livello da passare ai figli
            int livello = i + 1; // O qualsiasi logica tu voglia usare per il livello
            SetLevelToChildren(spawnedPrefab, livello);
        }
    }

    void SetLevelToChildren(GameObject parent, int level)
    {
        foreach (Transform child in parent.transform)
        {
            ChildComponent childComponent = child.GetComponent<ChildComponent>();
            if (childComponent != null)
            {
                childComponent.SetLevel(level);
            }
        }
    }
}
