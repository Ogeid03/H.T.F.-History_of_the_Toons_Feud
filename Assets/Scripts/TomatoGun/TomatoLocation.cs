using UnityEngine;

public class MaintainLocalPosition : MonoBehaviour
{
    private Vector3 initialLocalPosition;

    void Start()
    {
        // Salva la posizione locale iniziale dell'entità rispetto al genitore
        initialLocalPosition = transform.localPosition;
    }

    void LateUpdate()
    {
        // Mantieni la posizione locale iniziale
        transform.localPosition = initialLocalPosition;
    }
}
