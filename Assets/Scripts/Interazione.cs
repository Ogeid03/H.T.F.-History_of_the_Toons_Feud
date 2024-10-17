using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionTrigger : MonoBehaviour
{
    public float interactionDistance = 2.0f;  // Distanza per attivare l'interazione
    private GameObject player;  // Il riferimento al player

    public GameObject buttonPrefab;  // Prefab del bottone
    private GameObject instantiatedButton;  // Riferimento al bottone istanziato
    private Canvas canvas;  // Riferimento al canvas della GUI

    public bool isButtonPressed = false;  // Variabile per controllare se il bottone è stato premuto
    private const float buttonPressedDuration = 0.5f;  // Durata per cui la variabile rimane true

    // Start viene chiamato una volta all'inizio
    void Start()
    {
        // Trova il player usando il tag "Player"
        player = GameObject.FindGameObjectWithTag("Player");

        // Controlla se il player è stato trovato
        if (player == null)
        {
            Debug.LogError("Oggetto con tag 'Player' non trovato!");
        }

        // Trova il Canvas nella scena
        canvas = FindObjectOfType<Canvas>();

        // Controlla se il Canvas è stato trovato
        if (canvas == null)
        {
            Debug.LogError("Canvas non trovato nella scena! Assicurati di avere un Canvas nella gerarchia.");
        }
    }

    // Update viene chiamato una volta per frame
    void Update()
    {
        // Se il player esiste, calcola la distanza tra il player e questo oggetto
        if (player != null)
        {
            float distance = Vector2.Distance(player.transform.position, transform.position);

            // Se la distanza è inferiore a interactionDistance, istanzia il bottone
            if (distance <= interactionDistance && instantiatedButton == null)
            {
                // Istanzia il prefab del bottone come figlio del canvas
                instantiatedButton = Instantiate(buttonPrefab, canvas.transform);

                // Posiziona il bottone sulla X dell'oggetto e sulla Y del player
                RectTransform buttonRect = instantiatedButton.GetComponent<RectTransform>();

                // Calcolare la posizione del bottone nel Canvas in base alla X dell'oggetto e la Y del player
                Vector3 worldPosition = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
                Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

                // Conversione della posizione dello schermo alla posizione locale del Canvas
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPosition, canvas.worldCamera, out Vector2 localPoint);

                // Imposta la posizione del bottone nel Canvas
                buttonRect.localPosition = localPoint;

                // Aggiungi il listener al pulsante
                Button button = instantiatedButton.GetComponent<Button>();
                button.onClick.AddListener(OnButtonPressed);
            }
            else if (distance > interactionDistance && instantiatedButton != null)
            {
                // Se il player si allontana, distruggi il bottone istanziato
                Destroy(instantiatedButton);
            }
        }
    }

    // Metodo chiamato quando il pulsante viene premuto
    private void OnButtonPressed()
    {
        if (!isButtonPressed)
        {
            isButtonPressed = true; // Imposta il flag a true
            StartCoroutine(ResetButtonPressed()); // Inizia la coroutine per resettare il flag
            Debug.Log("Il pulsante è stato premuto!"); // Qui puoi aggiungere altre azioni
        }
    }

    // Coroutine per resettare il flag isButtonPressed
    private IEnumerator ResetButtonPressed()
    {
        yield return new WaitForSeconds(buttonPressedDuration); // Aspetta per la durata specificata
        isButtonPressed = false; // Resetta il flag a false
    }
}
