using UnityEngine;

public class EnemyActivateOnCameraView : MonoBehaviour
{
    private Renderer enemyRenderer;
    private EnemyLauncherAI enemyLauncherAI;

    void Start()
    {
        // Ottiene il Renderer per verificare la visibilit�
        enemyRenderer = GetComponent<Renderer>();

        // Ottiene il riferimento allo script EnemyLauncherAI presente sullo stesso GameObject
        enemyLauncherAI = GetComponent<EnemyLauncherAI>();

        // Disattiva l'AI del lanciatore all'inizio, finch� l'oggetto non � visibile
        if (enemyLauncherAI != null)
        {
            enemyLauncherAI.enabled = false;
        }
    }

    void Update()
    {
        // Se l'oggetto � visibile nella camera, attiva il comportamento dell'EnemyLauncherAI
        if (enemyRenderer.isVisible)
        {
            if (enemyLauncherAI != null && !enemyLauncherAI.enabled)
            {
                // Attiva l'EnemyLauncherAI se non � gi� attivo
                enemyLauncherAI.enabled = true;
            }
        }
        else
        {
            // Disattiva l'EnemyLauncherAI se l'oggetto non � pi� visibile
            if (enemyLauncherAI != null && enemyLauncherAI.enabled)
            {
                enemyLauncherAI.enabled = false;
            }
        }
    }
}
