using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Vita massima del boss
    private float currentHealth; // Vita corrente del boss

    public Animator animator; // Riferimento all'animatore del boss (opzionale)
    public AudioSource hurtSound; // Suono di danno (opzionale)

    public GameObject deathEffect; // Effetto visivo alla morte del boss (opzionale)

    void Start()
    {
        // Imposta la vita iniziale del boss
        currentHealth = maxHealth;
    }

    // Funzione per danneggiare il boss
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        // Se il boss ha ancora vita, esegui le azioni di danno
        if (currentHealth > 0)
        {
            // Puoi aggiungere un'animazione o un suono quando il boss prende danno
            if (animator != null)
            {
                animator.SetTrigger("Hurt"); // Imposta un trigger di animazione "Hurt" se esiste
            }

            if (hurtSound != null)
            {
                hurtSound.Play(); // Esegui il suono di danno, se disponibile
            }
        }
        else
        {
            Die(); // Se la vita arriva a 0 o meno, il boss muore
        }
    }

    // Funzione per la morte del boss
    private void Die()
    {
        // Imposta la vita del boss a 0 e fa scomparire il boss
        currentHealth = 0;

        // Esegui l'animazione di morte, se disponibile
        if (animator != null)
        {
            animator.SetTrigger("Die"); // Imposta un trigger di animazione "Die" se esiste
        }

        // Crea un effetto di morte, se disponibile
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity); // Crea un effetto di morte
        }

        // Disabilita il boss (o distruggilo)
        gameObject.SetActive(false); // Disabilita il boss, oppure usa Destroy(gameObject) se vuoi distruggerlo
        Debug.Log("Il boss è morto!");
    }

    // Funzione per recuperare la vita del boss
    public void Heal(float healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth); // Evita di superare la vita massima
    }

    // Funzione per ottenere la vita corrente
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
