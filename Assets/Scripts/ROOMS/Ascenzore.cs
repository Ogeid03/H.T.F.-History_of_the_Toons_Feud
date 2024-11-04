using System.Collections;
using UnityEngine;

public class MoveUp : MonoBehaviour
{
    private Vector3 originalPosition;    // Posizione originale dell'oggetto
    private Vector3 endPosition;         // Posizione originale del figlio "END"
    private InteractionTrigger interactionTrigger;  // Riferimento allo script di interazione
    public float speed = 2f;             // Velocità di movimento
    private float tolerance = 0.05f;     // Tolleranza per fermare il movimento

    // Start viene chiamato una volta all'inizio
    void Start()
    {
        // Memorizza la posizione originale dell'oggetto
        originalPosition = transform.position;

        // Trova il componente InteractionTrigger
        interactionTrigger = GetComponent<InteractionTrigger>();
        if (interactionTrigger == null)
        {
            Debug.LogError("Componente InteractionTrigger non trovato! Assicurati di avere questo script assegnato allo stesso oggetto.");
        }

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
    }

    // Update viene chiamato una volta per frame
    void Update()
    {
        // Controlla se il pulsante è stato premuto
        if (interactionTrigger != null && interactionTrigger.isButtonPressed)
        {
            StartCoroutine(MoveUpCoroutine());
        }
    }

    // Coroutine per alzare e poi riportare l'oggetto
    private IEnumerator MoveUpCoroutine()
    {
        // Muovi l'oggetto verso la posizione iniziale di "END"
        while (Vector3.Distance(transform.position, endPosition) > tolerance)
        {
            // Interpola la posizione dell'oggetto verso "endPosition" a una certa velocità
            transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
            yield return null; // Aspetta il prossimo frame
        }

        // Assicurati che l'oggetto arrivi esattamente alla posizione finale
        transform.position = endPosition;

        // Aspetta per un secondo al contatto con "endPosition"
        yield return new WaitForSeconds(1f);

        // Riporta l'oggetto alla posizione originale
        while (Vector3.Distance(transform.position, originalPosition) > tolerance)
        {
            // Interpola la posizione dell'oggetto verso la posizione originale
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, speed * Time.deltaTime);
            yield return null; // Aspetta il prossimo frame
        }

        // Assicurati che l'oggetto arrivi esattamente alla posizione originale
        transform.position = originalPosition;
    }
}
