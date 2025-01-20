using System.Collections.Generic;
using UnityEngine;

public class MoveCameraThroughObjects : MonoBehaviour
{
    public List<GameObject> targetObjects; // Lista di GameObject da passare nell'Inspector
    public float moveSpeed = 5f;          // Velocità dello spostamento (0 per movimento istantaneo)

    private int currentIndex = 0;         // Indice attuale nella lista
    private Vector3 targetPosition;      // Posizione verso cui si sposta la telecamera
    private bool isMoving = false;       // Indica se la telecamera è in movimento

    void Start()
    {
        // Imposta la posizione iniziale della telecamera come il primo oggetto nella lista
        if (targetObjects.Count > 0)
        {
            targetPosition = targetObjects[currentIndex].transform.position;
            transform.position = targetPosition;
        }
        else
        {
            Debug.LogWarning("La lista targetObjects è vuota! Aggiungi almeno un GameObject.");
        }
    }

    void Update()
    {
        // Controlla se il tasto "D" viene premuto e la telecamera non è già in movimento
        if (Input.GetKeyDown(KeyCode.D) && !isMoving)
        {
            MoveToNextObject();
        }

        // Movimento graduale della telecamera verso la posizione target
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Controlla se la telecamera ha raggiunto la destinazione
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition; // Correggi la posizione per evitare oscillazioni
                isMoving = false; // Movimento completato
            }
        }
    }

    private void MoveToNextObject()
    {
        if (targetObjects.Count == 0)
        {
            Debug.LogWarning("La lista targetObjects è vuota. Non ci sono oggetti verso cui muoversi.");
            return;
        }

        // Incrementa l'indice e torna al primo oggetto se si supera la lista
        currentIndex = (currentIndex + 1) % targetObjects.Count;

        // Imposta la posizione target come la posizione del prossimo oggetto
        targetPosition = targetObjects[currentIndex].transform.position;
        isMoving = moveSpeed > 0; // Se la velocità è 0, il movimento sarà istantaneo

        // Movimento istantaneo (se la velocità è 0)
        if (!isMoving)
        {
            transform.position = targetPosition;
        }
    }
}
