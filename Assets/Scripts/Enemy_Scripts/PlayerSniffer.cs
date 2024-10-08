using UnityEngine;

public class EnemyMoveOnPlayerProximity : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float detectionRange = 5f;  // Distanza minima per far muovere il nemico
    private GameObject player;
    private bool isPlayerInRange = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // Controlla la distanza tra il nemico e il giocatore
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRange)
        {
            // Se il giocatore è entro il raggio, il nemico si muove verso il giocatore
            isPlayerInRange = true;
        }
        else
        {
            // Se il giocatore è fuori dal raggio, il nemico si ferma
            isPlayerInRange = false;
        }

        // Se il giocatore è abbastanza vicino, il nemico si muove verso di lui
        if (isPlayerInRange)
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }
}
