using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressivePrefabSpawner : MonoBehaviour
{
    // Lista di prefab da cui scegliere
    public GameObject[] prefabs;

    // Il giocatore
    public Transform player;

    // Distanza fissa tra i prefab generati
    public float spawnDistance = 10f;

    // Valore che incrementa la difficoltà
    private int difficultyLevel = 1;

    // Posizione sull'asse X dove verrà spawnato il prossimo prefab
    private float nextSpawnX;

    // Il prefab attualmente generato
    private GameObject currentPrefab;

    void Start()
    {
        // Imposta la posizione iniziale del primo prefab
        nextSpawnX = player.position.x + spawnDistance;

        // Spawna il primo prefab
        SpawnNextPrefab();
    }

    void Update()
    {
        // Controlla se il giocatore ha raggiunto o superato il prefab attuale
        if (player.position.x >= currentPrefab.transform.position.x)
        {
            SpawnNextPrefab(); // Spawna il prossimo prefab
            IncreaseDifficulty(); // Incrementa la difficoltà
        }
    }

    // Metodo che spawna un prefab alla distanza prefissata
    void SpawnNextPrefab()
    {
        // Genera un prefab randomico dalla lista
        int randomIndex = Random.Range(0, prefabs.Length);

        // Definisci la posizione di spawn sull'asse X mantenendo Z costante
        Vector3 spawnPosition = new Vector3(nextSpawnX, player.position.y, player.position.z);

        // Spawna il prefab
        currentPrefab = Instantiate(prefabs[randomIndex], spawnPosition, Quaternion.identity);

        // Aggiorna la posizione per il prossimo spawn
        nextSpawnX += spawnDistance;
    }

    // Metodo che incrementa la difficoltà
    void IncreaseDifficulty()
    {
        // Incrementa il livello di difficoltà
        difficultyLevel++;

        // Puoi usare "difficultyLevel" per regolare diversi parametri nel gioco,
        // come la velocità del player, il numero di ostacoli, etc.
    }
}
