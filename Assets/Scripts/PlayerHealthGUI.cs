using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Importa il namespace per UI

public class HealthBar : MonoBehaviour
{
    public Image healthBar; // Riferimento all'immagine della barra della vita

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth; // Aggiorna la barra della vita
        }
    }
}
