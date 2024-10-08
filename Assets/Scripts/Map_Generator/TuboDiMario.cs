using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float liftForce = 10f; // Forza di sollevamento
    private bool isPlayerOnElevator = false; // Controlla se il giocatore � sull'ascensore

    void Update()
    {
        // Solleva il giocatore se � in contatto
        if (isPlayerOnElevator)
        {
            // Trova il player e applica la forza di sollevamento
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
                if (playerRb != null)
                {
                    // Applica una forza verso l'alto
                    playerRb.AddForce(Vector2.up * liftForce, ForceMode2D.Force);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Controlla se l'oggetto in contatto � il giocatore
        if (other.CompareTag("Player"))
        {
            isPlayerOnElevator = true; // Il giocatore � sull'ascensore
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Controlla se il giocatore esce dal contatto
        if (other.CompareTag("Player"))
        {
            isPlayerOnElevator = false; // Il giocatore non � pi� sull'ascensore
        }
    }
}
