using System.Collections;
using UnityEngine;

public class DoorBoss : MonoBehaviour
{
    private Collider2D myCollider;        // Riferimento al collider 2D dell'oggetto
    private Transform player;             // Riferimento al Transform del giocatore
    public float offset = 1f;             // Offset da aggiungere alla posizione X dell'oggetto
    public float tolerance = 0.1f;        // Tolleranza per considerare che il giocatore sia nella posizione corretta

    // Riferimento al BossBattleManager e alla Camera
    public BossBattleManager bossBattleManager;
    public CameraZoomOut cameraZoomOut;   // Riferimento allo script della telecamera per l'allontanamento
    public ButtonSpawner bs;

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

        // Cerca la camera con il tag "MainCamera" se cameraZoomOut non è già assegnato
        if (cameraZoomOut == null && Camera.main != null)
        {
            cameraZoomOut = Camera.main.GetComponent<CameraZoomOut>();
            if (cameraZoomOut == null)
            {
                Debug.LogError("CameraZoomOut non trovato sul GameObject della telecamera principale.");
            }
            else
            {
                Debug.Log("CameraZoomOut assegnato automaticamente dalla telecamera principale.");
            }
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

            // Attiva la battaglia
            if (bossBattleManager != null)
            {

                bossBattleManager.StartBossBattle();
                Debug.Log("Battaglia boss attivata.");

                // Attiva lo zoom out della telecamera
                if (cameraZoomOut != null)
                {
                    cameraZoomOut.ZoomOut();
                    Debug.Log("Zoom Out attivato dalla porta del boss");
                }
                else
                {
                    Debug.LogWarning("cameraZoomOut non è assegnato e non è stato trovato automaticamente.");
                }
                yield return new WaitForSeconds(20f); // 2 secondi di ritardo
                bs.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Il giocatore ha attivato il trigger.");
            StartCoroutine(WaitForPlayerPosition());
        }
    }
}