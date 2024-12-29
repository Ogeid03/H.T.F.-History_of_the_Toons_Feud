using UnityEngine;

public class CameraZoomOut : MonoBehaviour
{
    public Transform player;                // Riferimento al player
    public float zoomedOutSize = 10f;       // Dimensione della telecamera quando è allontanata
    public float zoomSpeed = 0.5f;          // Velocità con cui la telecamera si allontana (modificato per essere più lento)
    public float zoomedOutHeight = 5f;      // Altezza della telecamera quando è allontanata

    public float minY = -5f;                // Limite inferiore per l'asse Y
    public float maxY = 10f;                // Limite superiore per l'asse Y

    private Camera cam;
    private float originalSize;
    private Vector3 originalPosition;
    private bool isZoomedOut = false;       // Controlla se è attivo lo zoom

    private void Start()
    {
        // Ottieni il componente Camera e salva la dimensione originale
        cam = GetComponent<Camera>();
        originalSize = cam.orthographicSize;
        originalPosition = transform.position;  // Salva la posizione originale della telecamera
    }

    private void Update()
    {
        if (player == null)
            return; // Esci se il player non è stato assegnato

        // Controlla se lo zoom è attivato
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
            transform.position = Vector3.Lerp(
                transform.position,
                new Vector3(player.position.x, clampedY, transform.position.z),
                Time.deltaTime * zoomSpeed
            );
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