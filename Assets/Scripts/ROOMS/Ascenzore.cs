using System.Collections;
using UnityEngine;

public class MoveUp : MonoBehaviour
{
    private Vector3 originalPosition;    // Posizione originale dell'oggetto
    private Vector3 endPosition;         // Posizione originale del figlio "END"
    public float speed = 2f;             // Velocità di movimento
    private float tolerance = 0.05f;     // Tolleranza per fermare il movimento
    public float pauseDuration = 1f;     // Durata della pausa in cima e alla base

    // Start viene chiamato una volta all'inizio
    void Start()
    {
        // Memorizza la posizione originale dell'oggetto
        originalPosition = transform.position;

        // Trova l'oggetto figlio "END" e memorizza la sua posizione iniziale
        Transform endPoint = transform.Find("END");
        if (endPoint == null)
        {
            Debug.LogError("Oggetto figlio 'END' non trovato! Assicurati che l'oggetto sia presente come figlio e nominato correttamente.");
        }
        else
        {
            endPosition = endPoint.position;  // Memorizza la posizione iniziale di "END"
        }

        // Avvia la coroutine per il movimento automatico
        StartCoroutine(MoveElevator());
    }

    // Coroutine per muovere l'ascensore su e giù
    private IEnumerator MoveElevator()
    {
        while (true) // Ciclo infinito
        {
            // Muovi l'oggetto verso la posizione iniziale di "END"
            while (Vector3.Distance(transform.position, endPosition) > tolerance)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
                yield return null; // Aspetta il prossimo frame
            }

            // Assicurati che l'oggetto arrivi esattamente alla posizione finale
            transform.position = endPosition;

            // Pausa in cima
            yield return new WaitForSeconds(pauseDuration);

            // Riporta l'oggetto alla posizione originale
            while (Vector3.Distance(transform.position, originalPosition) > tolerance)
            {
                transform.position = Vector3.MoveTowards(transform.position, originalPosition, speed * Time.deltaTime);
                yield return null; // Aspetta il prossimo frame
            }

            // Assicurati che l'oggetto arrivi esattamente alla posizione originale
            transform.position = originalPosition;

            // Pausa alla base
            yield return new WaitForSeconds(pauseDuration);
        }
    }
}
