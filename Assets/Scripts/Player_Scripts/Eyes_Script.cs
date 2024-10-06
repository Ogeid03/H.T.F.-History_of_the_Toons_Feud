using UnityEngine;

public class LockZPositionUntilParent : MonoBehaviour
{
    // La posizione Z fissa della camera
    public float fixedZ = 0f;

    // La soglia Z del padre oltre la quale la camera può muoversi
    public float parentZThreshold = 10f;

    // Il riferimento all'oggetto padre
    private Transform parentTransform;

    void Start()
    {
        // Assegna il transform del padre
        parentTransform = transform.parent;
    }

    void Update()
    {
        // Controlla se il padre ha superato la soglia Z
        if (parentTransform != null && parentTransform.position.z < parentZThreshold)
        {
            // Mantieni la camera fissa sulla posizione Z impostata
            transform.position = new Vector3(transform.position.x, transform.position.y, fixedZ);
        }
        // Se il padre supera la soglia, la camera può muoversi liberamente
        // (altrimenti lascia che Unity gestisca la posizione Z)
    }
}
