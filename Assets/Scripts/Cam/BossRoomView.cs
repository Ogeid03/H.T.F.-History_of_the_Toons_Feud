using UnityEngine;

public class CameraZoomOut : MonoBehaviour
{
    public Transform player;                // Riferimento al player
    public float zoomedOutSize = 10f;       // Dimensione della telecamera quando è allontanata
    public float zoomSpeed = 2f;            // Velocità con cui la telecamera si allontana
    private Camera cam;
    private float originalSize;
    private bool isZoomedOut = false;       // Controlla se è attivo lo zoom

    private void Start()
    {
        // Ottieni il componente Camera e salva la dimensione originale
        cam = GetComponent<Camera>();
        originalSize = cam.orthographicSize;
        Debug.Log("CameraZoomOut Start - OriginalSize: " + originalSize);
    }

    private void Update()
    {
        // Verifica se lo zoom è attivato
        if (isZoomedOut)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomedOutSize, Time.deltaTime * zoomSpeed);
            Debug.Log("Zooming Out - Current Orthographic Size: " + cam.orthographicSize);
        }
        else
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, originalSize, Time.deltaTime * zoomSpeed);
            Debug.Log("Zooming In - Current Orthographic Size: " + cam.orthographicSize);
        }

        // Segui la posizione del player (opzionale)
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
    }

    // Metodo per attivare lo zoom out
    public void ZoomOut()
    {
        isZoomedOut = true;
        Debug.Log("ZoomOut() called - Zoom Out Activated");
    }

    // Metodo per resettare lo zoom
    public void ResetZoom()
    {
        isZoomedOut = false;
        Debug.Log("ResetZoom() called - Zoom Out Deactivated");
    }
}
