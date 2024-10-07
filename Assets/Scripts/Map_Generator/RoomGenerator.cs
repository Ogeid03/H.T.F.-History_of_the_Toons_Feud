using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    // Lista di prefab
    public List<GameObject> prefabs;

    // Riferimento al prefab iniziale
    public GameObject initialPrefab;

    // Distanza di spawn per il prossimo prefab (23.31 lungo l'asse X)
    public Vector3 spawnOffset = new Vector3(23.31f, 0f, 0f);

    // Posizione del primo spawn
    private Transform initialPrefabChild;

    void Start()
    {
        // Assicurati che ci sia almeno un prefab nella lista e che il prefab iniziale sia assegnato
        if (prefabs.Count > 0 && initialPrefab != null)
        {
            // Trova un figlio del prefab iniziale da cui calcolare la posizione (esempio: il primo figlio)
            initialPrefabChild = initialPrefab.transform.GetChild(0);

            // Spawna il primo prefab alla distanza specificata rispetto al figlio
            SpawnPrefabAtOffset();
        }
        else
        {
            Debug.LogError("Prefab o lista di prefabs non assegnati.");
        }
    }

    // Metodo per spawnare il prefab alla distanza desiderata
    void SpawnPrefabAtOffset()
    {
        // Scegli un prefab random dalla lista
        GameObject prefabToSpawn = prefabs[Random.Range(0, prefabs.Count)];

        // Calcola la posizione del nuovo prefab rispetto al figlio del prefab iniziale
        Vector3 spawnPosition = initialPrefabChild.position + spawnOffset;

        // Istanzia il nuovo prefab nella posizione calcolata
        GameObject newPrefab = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

        // Aggiungi un trigger per rilevare il contatto con il player
        Collider newPrefabCollider = newPrefab.GetComponent<Collider>();
        if (newPrefabCollider != null)
        {
            newPrefabCollider.isTrigger = true;
        }
        else
        {
            Debug.LogError("Il prefab non ha un collider.");
        }

        // Associa lo script di rilevazione collisione
        newPrefab.AddComponent<PrefabCollisionHandler>().spawner = this;
    }

    // Funzione per gestire lo spawn quando il player collide con un prefab
    public void OnPrefabCollision()
    {
        // Spawna un nuovo prefab alla distanza specificata quando il player collide
        SpawnPrefabAtOffset();
    }
}

public class PrefabCollisionHandler : MonoBehaviour
{
    public PrefabSpawner spawner;

    // Metodo per rilevare l'ingresso nel trigger
    void OnTriggerEnter(Collider other)
    {
        // Se l'oggetto che entra in contatto ha il tag "Player"
        if (other.CompareTag("Player"))
        {
            // Chiama il metodo nel PrefabSpawner per spawnare un altro prefab
            spawner.OnPrefabCollision();
        }
    }
}
