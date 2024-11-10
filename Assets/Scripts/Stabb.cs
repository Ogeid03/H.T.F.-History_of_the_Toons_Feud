using UnityEngine;
using System.Collections;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn; // Il prefab che vuoi istanziare
    public Vector3 spawnOffset = Vector3.zero; // Offset opzionale per la posizione dello spawn
    public float scaleMultiplier = 5f; // Fattore di scala per ingrandire il prefab

    private Rigidbody2D rb; // Riferimento al Rigidbody2D dell'oggetto
    private Transform corpoTransform; // Riferimento al sotto-oggetto "Corpo"

    void Start()
    {
        // Trova il Rigidbody2D e il sotto-oggetto "Corpo"
        rb = GetComponent<Rigidbody2D>();
        corpoTransform = transform.Find("Corpo");

        // Verifica se il prefab e il sotto-oggetto "Corpo" sono assegnati
        if (prefabToSpawn != null && corpoTransform != null)
        {
            // Istanzia il prefab come figlio del sotto-oggetto "Corpo"
            SpawnPrefab();
        }
        else
        {
            if (prefabToSpawn == null)
                Debug.LogError("Prefab non assegnato!");

            if (corpoTransform == null)
                Debug.LogError("Sotto-oggetto 'Corpo' non trovato!");
        }
    }

    void SpawnPrefab()
    {
        // Posizione di spawn con l'offset
        Vector3 spawnPosition = corpoTransform.position + spawnOffset;

        // Istanzia il prefab come figlio del sotto-oggetto "Corpo"
        GameObject instance = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity, corpoTransform);

        // Modifica la scala per ingrandire l'oggetto
        instance.transform.localScale *= scaleMultiplier;

        // Verifica se l'oggetto instanziato ha un Animator
        Animator animator = instance.GetComponent<Animator>();
        if (animator != null)
        {
            // Avvia l'animazione e la coroutine per la distruzione dopo l'animazione
            Debug.Log("Prefab con Animator instanziato con successo!");
            animator.Play("AnimazioneIniziale");  // Sostituisci con il nome della tua animazione
            StartCoroutine(DestroyAfterAnimation(animator, instance));
        }
        else
        {
            // Se non c'è un Animator, distruggi l'oggetto dopo un tempo predefinito come fallback
            Debug.LogWarning("Il prefab non ha un Animator. Verrà distrutto dopo 2 secondi.");
            Destroy(instance, 2f);
        }

        // Log della posizione di spawn
        Debug.Log("Prefab spawnato in posizione: " + spawnPosition);
    }

    IEnumerator DestroyAfterAnimation(Animator animator, GameObject instance)
    {
        // Attendi la fine dell'animazione corrente
        float animationDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationDuration);

        // Distruggi l'oggetto istanziato alla fine dell'animazione
        Destroy(instance);
        Debug.Log("Prefab distrutto alla fine dell'animazione.");
    }

    void Update()
    {
        // Se il Rigidbody2D è presente, controlla la direzione e flippa "Corpo" di conseguenza
        if (rb != null)
        {
            if (rb.velocity.x < 0)
            {
                // Se si muove verso sinistra, flippa "Corpo"
                corpoTransform.localScale = new Vector3(-1, 1, 1);
            }
            else if (rb.velocity.x > 0)
            {
                // Se si muove verso destra, ripristina la scala originale
                corpoTransform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
