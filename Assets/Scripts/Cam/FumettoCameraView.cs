using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Posizioni specifiche in cui la telecamera deve spostarsi, compresa la posizione iniziale
    private Vector3[] positions = new Vector3[5];
    private int currentStep = 0; // Tieni traccia del passo corrente

    // Velocità del movimento della telecamera
    public float moveSpeed = 5f;

    // Variabili per rilevare lo swipe
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    public float minSwipeDistance = 50f; // Distanza minima per considerare uno swipe

    void Start()
    {
        // Definisci le posizioni di movimento della telecamera
        positions[0] = transform.position;
        positions[1] = new Vector3(6f, transform.position.y, transform.position.z);
        positions[2] = new Vector3(22f, transform.position.y, transform.position.z);
        positions[3] = new Vector3(46f, transform.position.y, transform.position.z);
        positions[4] = new Vector3(66f, transform.position.y, transform.position.z);

        // Imposta la posizione iniziale della telecamera
        transform.position = positions[0];
    }

    void Update()
    {
        // Gestione dello swipe
        DetectSwipe();

        // Per debugging o uso su PC, mantieni i tasti "D" e "A"
        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveCameraToNextPosition();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveCameraToPreviousPosition();
        }
    }

    void DetectSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;
                HandleSwipe();
            }
        }
    }

    void HandleSwipe()
    {
        Vector2 swipeDelta = endTouchPosition - startTouchPosition;

        // Controlla se lo swipe è lungo abbastanza
        if (swipeDelta.magnitude > minSwipeDistance)
        {
            // Controlla la direzione dello swipe
            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
            {
                // Swipe orizzontale
                if (swipeDelta.x > 0)
                {
                    // Swipe verso destra
                    MoveCameraToPreviousPosition();
                }
                else
                {
                    // Swipe verso sinistra
                    MoveCameraToNextPosition();
                }
            }
        }
    }

    void MoveCameraToNextPosition()
    {
        // Passa alla posizione successiva solo se non sei all'ultima posizione
        if (currentStep < positions.Length - 1)
        {
            currentStep++;
            // Muovi la telecamera
            StopAllCoroutines();
            StartCoroutine(MoveToPosition(positions[currentStep]));
        }
    }

    void MoveCameraToPreviousPosition()
    {
        // Passa alla posizione precedente solo se non sei alla prima posizione
        if (currentStep > 0)
        {
            currentStep--;
            // Muovi la telecamera
            StopAllCoroutines();
            StartCoroutine(MoveToPosition(positions[currentStep]));
        }
    }

    // Coroutine per spostare la telecamera verso una posizione specifica
    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        float timeElapsed = 0f;
        Vector3 startPosition = transform.position;

        while (timeElapsed < 1f)
        {
            // Calcola il movimento
            transform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed);

            timeElapsed += Time.deltaTime * moveSpeed; // Aggiorna il tempo trascorso
            yield return null; // Attendi il prossimo frame
        }

        // Assicurati che la telecamera arrivi esattamente alla destinazione
        transform.position = targetPosition;
    }
}
