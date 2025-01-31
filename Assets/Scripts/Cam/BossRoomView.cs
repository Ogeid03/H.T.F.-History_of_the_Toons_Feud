using UnityEngine;

public class CameraZoomOut : MonoBehaviour
{
    public Transform player;                // Riferimento al player
    public float zoomedOutSize = 15f;       // Dimensione della telecamera quando � allontanata
    public float zoomSpeed = 0.5f;          // Velocit� con cui la telecamera si allontana (modificato per essere pi� lento)
    public float zoomedOutHeight = 5f;      // Altezza della telecamera quando � allontanata

    public float minY = -5f;                // Limite inferiore per l'asse Y
    public float maxY = 50f;                // Limite superiore per l'asse Y

    private Camera cam;
    private float originalSize = 5f;        // Imposta la dimensione originale a 5 (prima di qualsiasi zoom)
    private Vector3 originalPosition;
    private bool isZoomedOut = false;       // Controlla se � attivo lo zoom

    private void Start()
    {
        // Ottieni il componente Camera e salva la dimensione originale
        cam = GetComponent<Camera>();
        cam.orthographicSize = originalSize; // Imposta subito la telecamera a dimensione 5
        originalPosition = transform.position;  // Salva la posizione originale della telecamera
    }

    private void Update()
    {
        if (player == null)
            return; // Esci se il player non � stato assegnato

        // Controlla se lo zoom � attivato
        if (isZoomedOut)
        {
            // Interpola la dimensione della telecamera (zoom)
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomedOutSize, Time.deltaTime * zoomSpeed);

            // Interpola la posizione della telecamera (muovila gradualmente in alto)
            Vector3 targetPosition = new Vector3(
                player.position.x,
                Mathf.Clamp(originalPosition.y + zoomedOutHeight, minY, maxY), // Clampa la Y
                transform.position.z
            );
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * zoomSpeed);
        }
        else
        {
            // Interpola la dimensione della telecamera (zoom normale)
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, originalSize, Time.deltaTime * zoomSpeed);

            // Segui la posizione del player su X e Y (applica limiti opzionali su Y)
            float clampedY = Mathf.Clamp(player.position.y, minY, maxY);

            // Quando la telecamera � zoomata indietro, mantieni la posizione Y pi� vicina a quella originale.
            // Evita di avere una Y troppo bassa a causa dell'altezza della telecamera precedente.
            if (!isZoomedOut)
            {
                transform.position = Vector3.Lerp(
                    transform.position,
                    new Vector3(player.position.x, Mathf.Max(clampedY, originalPosition.y), transform.position.z), // Usa originalPosition.y per evitare scivolamenti
                    Time.deltaTime * zoomSpeed
                );
            }
        }

    }

    // Metodo per attivare lo zoom out
    public void ZoomOut()
    {
        isZoomedOut = true;
    }

    // Metodo per resettare lo zoom
    public void ResetZoom()
    {
        isZoomedOut = false;
    }
}