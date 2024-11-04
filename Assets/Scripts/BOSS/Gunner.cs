using System.Collections;
using UnityEngine;

public class RandomMoveBetweenPoints : MonoBehaviour
{
    private Transform startPoint;           // Riferimento all'oggetto "Start"
    private Transform endPoint;             // Riferimento all'oggetto "End"
    private Transform targetPoint;          // Il punto di destinazione corrente
    public float speed = 3f;                // Velocità di movimento dell'oggetto

    // Intervallo minimo e massimo per il cambio casuale di direzione
    public float minRandomDirectionInterval = 1f;
    public float maxRandomDirectionInterval = 5f;

    private bool movingTowardsEnd = true;   // Direzione attuale (true: verso End, false: verso Start)

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

        // Imposta il primo target verso "End" e avvia la routine per il cambio casuale
        targetPoint = endPoint;
        movingTowardsEnd = true;
        StartCoroutine(ChangeDirectionRandomly());
    }

    // Update viene chiamato una volta per frame
    void Update()
    {
        // Muovi l'oggetto verso il target point corrente
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        // Se l'oggetto raggiunge uno dei punti, cambia direzione
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.01f)
        {
            ChangeTargetPoint();
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

            // Cambia direzione casualmente solo se non è già vicinissimo a uno dei due punti
            if (Vector3.Distance(transform.position, startPoint.position) > 0.1f &&
                Vector3.Distance(transform.position, endPoint.position) > 0.1f)
            {
                ChangeTargetPoint();
            }
        }
    }

    // Cambia il target point tra Start e End
    private void ChangeTargetPoint()
    {
        if (movingTowardsEnd)
        {
            targetPoint = startPoint;
            movingTowardsEnd = false;
        }
        else
        {
            targetPoint = endPoint;
            movingTowardsEnd = true;
        }
    }
}
