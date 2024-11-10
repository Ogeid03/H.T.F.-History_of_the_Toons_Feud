using System.Collections;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn; // Il prefab che vuoi istanziare
    public Vector3 spawnOffset = Vector3.zero; // Offset opzionale per la posizione dello spawn
    public float scaleMultiplier = 5f; // Fattore di scala per ingrandire il prefab

    private Transform corpoTransform; // Riferimento al sotto-oggetto "Corpo"

    void Start()
    {
        corpoTransform = transform.Find("Corpo"); // Trova il sotto-oggetto "Corpo" per il flipping

        if (prefabToSpawn == null)
        {
            Debug.LogError("Prefab non assegnato!");
        }
        if (corpoTransform == null)
        {
            Debug.LogError("Sotto-oggetto 'Corpo' non trovato!");
        }
    }

    // Metodo per spawnare il prefab, chiamato da EnemyDamage
    public void SpawnPrefab()
    {
        if (prefabToSpawn != null && corpoTransform != null)
        {
            // Posizione di spawn con l'offset
            Vector3 spawnPosition = corpoTransform.position + spawnOffset;

            // Istanzia il prefab come figlio del sotto-oggetto "Corpo"
            GameObject instance = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity, corpoTransform);

            // Ingrandisce il prefab
            instance.transform.localScale = Vector3.one * scaleMultiplier; // Usa Vector3.one per ripristinare la scala originale

            // Flippa il prefab in base alla direzione del genitore, senza alterare l'animazione
            FlipPrefabBasedOnParentDirection(instance);

            // Distrugge il prefab al termine dell'animazione, se presente
            Animator prefabAnimator = instance.GetComponent<Animator>();
            if (prefabAnimator != null)
            {
                Debug.Log("Prefab con Animator instanziato!");
                StartCoroutine(DestroyAfterAnimation(prefabAnimator, instance));
            }
            else
            {
                Destroy(instance, 2f); // Distrugge dopo 2 secondi se non ha Animator
            }

            Debug.Log("Prefab spawnato in posizione: " + spawnPosition);
        }
    }

    // Gestisce il flipping del prefab in base alla direzione del genitore, senza alterare la scala
    private void FlipPrefabBasedOnParentDirection(GameObject prefabInstance)
    {
        // Flippa il prefab ruotandolo, invece di cambiare la scala
        if (corpoTransform != null)
        {
            // Se il genitore ha una scala negativa sull'asse X, ruota il prefab di 180 gradi
            if (corpoTransform.localScale.x < 0)
            {
                prefabInstance.transform.rotation = Quaternion.Euler(0, 180, 0); // Ruota di 180 gradi
            }
            else
            {
                prefabInstance.transform.rotation = Quaternion.Euler(0, 0, 0); // Restituisci la rotazione originale
            }
        }
    }

    IEnumerator DestroyAfterAnimation(Animator animator, GameObject instance)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = stateInfo.length;

        // Assicurati che l'animazione inizi correttamente
        yield return new WaitForSeconds(animationDuration + 0.1f);

        Destroy(instance);
        Debug.Log("Prefab distrutto alla fine dell'animazione.");
    }
}
