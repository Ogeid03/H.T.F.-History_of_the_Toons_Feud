using UnityEngine;

public class CameraZoomOut : MonoBehaviour
{
    public Transform player;                // Riferimento al player
    public float zoomedOutSize = 10f;       // Dimensione della telecamera quando è allontanata
    public float zoomSpeed = 0.5f;          // Velocità con cui la telecamera si allontana (modificato per essere più lento)
    public float zoomedOutHeight = 5f;      // Altezza della telecamera quando è allontanata
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
        //#Debug.Log("CameraZoomOut Start - OriginalSize: " + originalSize + ", OriginalPosition: " + originalPosition);
    }

    private void Update()
    {
        // Verifica se lo zoom è attivato
        if (isZoomedOut)
        {
            // Interpola la dimensione della telecamera (zoom)
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomedOutSize, Time.deltaTime * zoomSpeed);

            // Interpola la posizione della telecamera (muovila gradualmente in alto)
            Vector3 targetPosition = new Vector3(player.position.x, originalPosition.y + zoomedOutHeight, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * zoomSpeed);

            //#Debug.Log("Zooming Out - Current Orthographic Size: " + cam.orthographicSize + ", Position: " + transform.position);
        }
        else
        {
            // Interpola la dimensione della telecamera (zoom)
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, originalSize, Time.deltaTime * zoomSpeed);

            // Ripristina gradualmente la posizione originale della telecamera
            transform.position = Vector3.Lerp(transform.position, originalPosition, Time.deltaTime * zoomSpeed);

            //#Debug.Log("Zooming In - Current Orthographic Size: " + cam.orthographicSize + ", Position: " + transform.position);
        }

        // Segui la posizione del player (in X) se lo zoom non è attivo
        if (!isZoomedOut)
        {
            transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        }
    }

    // Metodo per attivare lo zoom out
    public void ZoomOut()
    {
        isZoomedOut = true;
        //#Debug.Log("ZoomOut() called - Zoom Out Activated");
    }

    // Metodo per resettare lo zoom
    public void ResetZoom()
    {
        isZoomedOut = false;
        //#Debug.Log("ResetZoom() called - Zoom Out Deactivated");
    }
}
