using System.Collections;
using UnityEngine;

public class DisableTriggerOnPlayerPosition : MonoBehaviour
{
    private Collider2D myCollider; // Riferimento al collider 2D dell'oggetto
    private Transform player;        // Riferimento al Transform del giocatore
    public float offset = 1f;       // Offset da aggiungere alla posizione X dell'oggetto
    public float tolerance = 0.1f;   // Tolleranza per considerare che il giocatore sia nella posizione corretta

    void Start()
    {
        // Ottieni il componente Collider2D dell'oggetto
        myCollider = GetComponent<Collider2D>();

        // Se non trovato, cerca nei figli
        if (myCollider == null)
        {
            myCollider = GetComponentInChildren<Collider2D>();
        }

        if (myCollider == null)
        {
            Debug.LogError("Nessun collider 2D trovato su questo oggetto o sui suoi figli.");
            return; // Esci se non trovi un collider
        }

        Debug.Log("Collider 2D trovato: " + myCollider.name);

        // Trova il giocatore in base al tag
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogError("Nessun oggetto con tag 'Player' trovato nella scena.");
            return; // Esci se non trovi il giocatore
        }

        // Avvia la coroutine per monitorare la posizione del giocatore
        StartCoroutine(WaitForPlayerPosition());
    }

    private IEnumerator WaitForPlayerPosition()
    {
        // Calcola la posizione targetX basata sulla posizione dell'oggetto con lo script
        float targetX = transform.position.x + offset;

        // Aspetta che il giocatore sia oltre il targetX
        while (player.position.x < targetX - tolerance)
        {
            yield return null; // Aspetta il prossimo frame
        }

        // Quando il giocatore supera il targetX, imposta isTrigger su false
        if (myCollider != null)
        {
            myCollider.isTrigger = false; // Imposta isTrigger su false
            Debug.Log("isTrigger è stato impostato su false.");
        }
    }

    // Metodo chiamato automaticamente quando un altro collider entra in contatto
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Controlla se il collider appartiene a un oggetto con il tag "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("Il giocatore ha attivato il trigger.");
            // Avvia la coroutine per monitorare la posizione del giocatore
            StartCoroutine(WaitForPlayerPosition());
        }
    }
}
