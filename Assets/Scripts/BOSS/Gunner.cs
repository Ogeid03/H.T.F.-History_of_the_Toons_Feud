using System.Collections;
using UnityEngine;

public class RandomMoveBetweenPoints : MonoBehaviour
{
    private Transform startPoint;           // Riferimento all'oggetto "Start"
    private Transform endPoint;             // Riferimento all'oggetto "End"
    public float speed = 3f;                // Velocità di movimento dell'oggetto

    // Intervallo minimo e massimo per il cambio casuale di direzione
    public float minRandomDirectionInterval = 1f;
    public float maxRandomDirectionInterval = 3f;

    private bool movingRight = true;        // Direzione attuale (true: destra, false: sinistra)

    // Start viene chiamato una volta all'inizio
    void Start()
    {
        // Trova gli oggetti chiamati "Start" e "End" nella scena
        startPoint = GameObject.Find("START.")?.transform;
        endPoint = GameObject.Find("END.")?.transform;

        // Verifica se entrambi i punti sono stati trovati
        if (startPoint == null || endPoint == null)
        {
            Debug.LogError("Gli oggetti 'Start' o 'End' non sono stati trovati nella scena.");
            enabled = false; // Disabilita lo script se mancano i punti
            return;
        }

        // Avvia la routine per il cambio casuale
        StartCoroutine(ChangeDirectionRandomly());
    }

    // Update viene chiamato una volta per frame
    void Update()
    {
        // Muovi l'oggetto nella direzione attuale
        if (movingRight)
        {
            // Muovi a destra
            transform.position += Vector3.right * speed * Time.deltaTime;

            // Se l'oggetto raggiunge o supera "End", inverte la direzione
            if (transform.position.x >= endPoint.position.x)
            {
                movingRight = false; // Cambia direzione a sinistra
            }
        }
        else
        {
            // Muovi a sinistra
            transform.position += Vector3.left * speed * Time.deltaTime;

            // Se l'oggetto raggiunge o supera "Start", inverte la direzione
            if (transform.position.x <= startPoint.position.x)
            {
                movingRight = true; // Cambia direzione a destra
            }
        }
    }

    // Coroutine per cambiare direzione in modo casuale ogni intervallo casuale
    private IEnumerator ChangeDirectionRandomly()
    {
        while (true)
        {
            // Scegli un intervallo casuale tra il minimo e il massimo specificato
            float randomInterval = Random.Range(minRandomDirectionInterval, maxRandomDirectionInterval);
            yield return new WaitForSeconds(randomInterval);

            // Cambia la direzione casualmente
            movingRight = !movingRight; // Inverte la direzione
        }
    }
}
