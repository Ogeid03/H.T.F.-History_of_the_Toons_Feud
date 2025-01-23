using UnityEngine;

public class ScaleReducerWithReset : MonoBehaviour
{
    [Header("GameObject to Scale")]
    public GameObject targetObject; // Il GameObject da scalare

    [Header("Scaling Settings")]
    [Range(0f, 1f)]
    public float scaleReductionPercentage = 0.1f; // Percentuale di riduzione (0.1 = 10%)

    private Vector3 originalScale; // Scala originale del GameObject
    private bool isReduced = false; // Indica se la scala è stata ridotta

    void Start()
    {
        // Salva la scala originale al momento dell'avvio
        if (targetObject != null)
        {
            originalScale = targetObject.transform.localScale;
        }
        else
        {
            Debug.LogError("Target Object non assegnato nello script ScaleReducerWithReset.");
        }
    }

    /// <summary>
    /// Riduce la scala del GameObject di una percentuale specificata.
    /// </summary>
    public void ReduceScale()
    {
        if (targetObject != null && !isReduced)
        {
            Vector3 currentScale = targetObject.transform.localScale;
            float reductionFactor = 1f - scaleReductionPercentage;
            targetObject.transform.localScale = currentScale * reductionFactor;
            isReduced = true; // Indica che la scala è stata ridotta
        }
        else if (isReduced)
        {
            Debug.LogWarning("Il GameObject è già stato ridotto. Usa ResetScale per ripristinare la scala.");
        }
    }

    /// <summary>
    /// Ripristina la scala originale del GameObject.
    /// </summary>
    public void ResetScale()
    {
        if (targetObject != null && isReduced)
        {
            targetObject.transform.localScale = originalScale;
            isReduced = false; // Ripristina lo stato
        }
        else if (!isReduced)
        {
            Debug.LogWarning("La scala è già nella sua forma originale.");
        }
    }
}
