using UnityEngine;

public class MapPreview : MonoBehaviour
{
    public Transform[] cameraPath;   // Percorso lungo cui la telecamera si sposterà
    public float moveSpeed = 2f;     // Velocità di movimento della telecamera
    public float freezeTime = 3f;    // Tempo in cui la telecamera rimane ferma per la preview
    private Camera mainCamera;       // Riferimento alla telecamera principale
    private bool isPreviewing = false; // Indica se la telecamera è in modalità preview
    private int pathIndex = 0;       // Indice del punto corrente nel percorso della telecamera

    private Vector3 playerInitialPosition; // Posizione iniziale del giocatore
    private bool isPlayerControlEnabled = true; // Controllo del giocatore abilitato o disabilitato

    void Start()
    {
        mainCamera = Camera.main; // Ottieni la telecamera principale
        playerInitialPosition = transform.position; // Posizione iniziale del giocatore
    }

    void Update()
    {
        if (isPreviewing)
        {
            // La telecamera si muove lungo il percorso definito
            MoveCameraAlongPath();

            // Dopo aver finito il movimento, congeliamo la telecamera per un po'
            if (pathIndex >= cameraPath.Length)
            {
                Invoke("EndPreview", freezeTime); // Congeliamo la telecamera per un po'
                isPreviewing = false;
            }
        }
        else
        {
            // Restituiamo il controllo al giocatore (se desiderato)
            if (!isPlayerControlEnabled)
            {
                EnablePlayerControls();
            }
        }
    }

    // Funzione per avviare la preview della mappa
    public void StartPreview()
    {
        isPreviewing = true;
        isPlayerControlEnabled = false; // Disabilita il controllo del giocatore
        pathIndex = 0;
        transform.position = cameraPath[pathIndex].position; // Posiziona la telecamera al primo punto
    }

    // Funzione per muovere la telecamera lungo il percorso
    void MoveCameraAlongPath()
    {
        if (pathIndex < cameraPath.Length)
        {
            transform.position = Vector3.MoveTowards(transform.position, cameraPath[pathIndex].position, moveSpeed * Time.deltaTime);

            // Quando la telecamera arriva al prossimo punto, passa al successivo
            if (transform.position == cameraPath[pathIndex].position)
            {
                pathIndex++;
            }
        }
    }

    // Funzione per congelare la telecamera alla fine della preview
    void EndPreview()
    {
        // Congela la telecamera per un po'
        // Dopo il tempo di freeze, puoi riattivare il controllo del giocatore
        Invoke("EnablePlayerControls", freezeTime);
    }

    // Funzione per abilitare i controlli del giocatore
    void EnablePlayerControls()
    {
        isPlayerControlEnabled = true;
        // Se hai un altro script che gestisce i controlli della telecamera del giocatore, abilitalo
        // Esempio:
        // playerControlScript.enabled = true;
        // Se la telecamera ha un sistema di controllo, riabilita i controlli qui
    }

    // Funzione per disabilitare i controlli del giocatore
    void DisablePlayerControls()
    {
        isPlayerControlEnabled = false;
        // Se hai un altro script che gestisce i controlli della telecamera del giocatore, disabilitalo
        // Esempio:
        // playerControlScript.enabled = false;
    }
}
