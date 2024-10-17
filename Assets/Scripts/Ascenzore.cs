using System.Collections;
using UnityEngine;

public class MoveUp : MonoBehaviour
{
    private Vector3 originalPosition;  // Posizione originale dell'oggetto
    private InteractionTrigger interactionTrigger;  // Riferimento allo script di interazione

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
        // Alza l'oggetto di 2 unità (puoi cambiare l'altezza)
        Vector3 targetPosition = originalPosition + new Vector3(0, 2f, 0);
        float elapsedTime = 0f;
        float duration = 3f; // Tempo di salita

        // Muovi l'oggetto verso l'alto
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null; // Aspetta il prossimo frame
        }

        // Assicurati che l'oggetto arrivi alla posizione finale
        transform.position = targetPosition;

        // Aspetta per un secondo prima di tornare indietro
        yield return new WaitForSeconds(1f);

        // Riporta l'oggetto alla posizione originale
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(targetPosition, originalPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null; // Aspetta il prossimo frame
        }

        // Assicurati che l'oggetto arrivi alla posizione originale
        transform.position = originalPosition;
    }
}
