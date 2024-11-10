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

            // Imposta la scala desiderata, forzandola a mantenere il valore del parametro scaleMultiplier
            instance.transform.localScale = Vector3.one * scaleMultiplier;

            // Distrugge il prefab al termine dell'animazione, se presente
            Animator prefabAnimator = instance.GetComponent<Animator>();
            if (prefabAnimator != null)
            {
                Debug.Log("Prefab con Animator instanziato!");
                StartCoroutine(DestroyAfterAnimation(prefabAnimator, instance));
            }
            else
            {
                Destroy(instance, 0.5f); // Distrugge dopo 2 secondi se non ha Animator
            }

            Debug.Log("Prefab spawnato in posizione: " + spawnPosition);
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
