using UnityEngine;

public class EnemyActivateOnCameraView : MonoBehaviour
{
    private Renderer enemyRenderer;
    private EnemyLauncherAI enemyLauncherAI;

    void Start()
    {
        // Ottiene il Renderer per verificare la visibilità
        enemyRenderer = GetComponent<Renderer>();

        // Ottiene il riferimento allo script EnemyLauncherAI presente sullo stesso GameObject
        enemyLauncherAI = GetComponent<EnemyLauncherAI>();

        // Disattiva l'AI del lanciatore all'inizio, finché l'oggetto non è visibile
        if (enemyLauncherAI != null)
        {
            enemyLauncherAI.enabled = false;
        }
    }

    void Update()
    {
        // Se l'oggetto è visibile nella camera, attiva il comportamento dell'EnemyLauncherAI
        if (enemyRenderer.isVisible)
        {
            if (enemyLauncherAI != null && !enemyLauncherAI.enabled)
            {
                // Attiva l'EnemyLauncherAI se non è già attivo
                enemyLauncherAI.enabled = true;
            }
        }
        else
        {
            // Disattiva l'EnemyLauncherAI se l'oggetto non è più visibile
            if (enemyLauncherAI != null && enemyLauncherAI.enabled)
            {
                enemyLauncherAI.enabled = false;
            }
        }
    }
}
