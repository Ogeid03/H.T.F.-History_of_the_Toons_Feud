using System.Collections;
using UnityEngine;

public class TriggerLockMove : MonoBehaviour
{
    private Transform lockObject;  // Riferimento all'oggetto figlio "LOCK"
    private Vector3 endPosition;   // Posizione originale del figlio "END"
    public float speed = 2f;       // Velocità di movimento di "LOCK"

    // Start viene chiamato una volta all'inizio
    void Start()
    {
        // Trova l'oggetto figlio "LOCK"
        lockObject = transform.Find("LOCK");
        if (lockObject == null)
        {
            Debug.LogError("Oggetto figlio 'LOCK' non trovato! Assicurati che l'oggetto sia presente come figlio e nominato correttamente.");
            return;
        }
        Debug.Log("'LOCK' trovato con successo.");

        // Trova l'oggetto "END" all'interno di "LOCK" e memorizza la sua posizione iniziale
        Transform endPoint = transform.Find("END");
        if (endPoint == null)
        {
            Debug.LogError("Oggetto figlio 'END' non trovato all'interno di 'LOCK'! Assicurati che l'oggetto sia presente come figlio e nominato correttamente.");
            return;
        }
        endPosition = endPoint.position;  // Memorizza la posizione iniziale di "END"
        Debug.Log($"Posizione di 'END' memorizzata: {endPosition}");
    }

    // Questo metodo viene chiamato automaticamente quando un altro collider entra in contatto
    private void OnTriggerEnter(Collider other)
    {
        // Controlla se il collider appartiene a un oggetto con il tag "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("Il giocatore ha attivato il trigger. Inizio movimento di 'LOCK'.");
            // Avvia la coroutine per muovere "LOCK" verso il basso
            StartCoroutine(MoveLockDownCoroutine());
        }
    }

    // Coroutine per muovere "LOCK" verso il basso fino a raggiungere "END"
    private IEnumerator MoveLockDownCoroutine()
    {
        Debug.Log("Inizio movimento di 'LOCK' verso 'END'.");

        // Muovi l'oggetto "LOCK" verso la posizione di "END"
        while (Vector3.Distance(lockObject.position, endPosition) > 0.01f)
        {
            // Interpola la posizione di "LOCK" verso "endPosition" a una certa velocità
            lockObject.position = Vector3.MoveTowards(lockObject.position, endPosition, speed * Time.deltaTime);
            yield return null; // Aspetta il prossimo frame
        }

        // Assicurati che "LOCK" arrivi esattamente alla posizione finale
        lockObject.position = endPosition;
        Debug.Log($"'LOCK' è arrivato alla posizione finale: {endPosition}");
    }
}
