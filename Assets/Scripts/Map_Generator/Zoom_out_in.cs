using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Collider2D myCollider;        // Riferimento al collider 2D dell'oggetto
    private Transform player;             // Riferimento al Transform del giocatore
    public float offset = 1f;             // Offset da aggiungere alla posizione X dell'oggetto
    public float tolerance = 0.1f;        // Tolleranza per considerare che il giocatore sia nella posizione corretta

    // Variabile booleana per lo zoom modificabile dall'Inspector
    public bool isZoomedIn = false;       // Se true, zoom in; se false, zoom out
    public float zoomInSize = 5f;         // Valore di zoom in
    public float zoomOutSize = 10f;       // Valore di zoom out
    public float zoomSpeed = 2f;          // Velocità dello zoom

    private Camera mainCamera;            // Riferimento alla telecamera principale

    void Start()
    {
        myCollider = GetComponent<Collider2D>();

        if (myCollider == null)
        {
            myCollider = GetComponentInChildren<Collider2D>();
        }

        if (myCollider == null)
        {
            Debug.LogError("Nessun collider 2D trovato su questo oggetto o sui suoi figli.");
            return;
        }

        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogError("Nessun oggetto con tag 'Player' trovato nella scena.");
            return;
        }

        // Trova la telecamera principale
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Telecamera principale non trovata nella scena.");
            return;
        }

        // Avvia la coroutine per monitorare la posizione del giocatore
        StartCoroutine(WaitForPlayerPosition());
    }

    private IEnumerator WaitForPlayerPosition()
    {
        float targetX = transform.position.x + offset;

        while (player.position.x < targetX - tolerance)
        {
            yield return null;
        }

        if (myCollider != null)
        {
            myCollider.isTrigger = false; // Imposta isTrigger su false
            Debug.Log("isTrigger è stato impostato su false.");

            // Applica lo zoom in base al valore della variabile booleana
            StartCoroutine(ZoomCamera(isZoomedIn ? zoomInSize : zoomOutSize));
        }
    }

    private IEnumerator ZoomCamera(float targetSize)
    {
        while (Mathf.Abs(mainCamera.orthographicSize - targetSize) > 0.01f)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetSize, Time.deltaTime * zoomSpeed);
            yield return null;
        }
        mainCamera.orthographicSize = targetSize;
    }
}