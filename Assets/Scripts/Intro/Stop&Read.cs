using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RaycastBlocker : MonoBehaviour
{
    [Header("Elementi da bloccare")]
    public List<Graphic> elementsToBlock; // Lista degli elementi da bloccare

    [Header("Parent del pulsante")]
    public Transform buttonParent;        // Genitore che contiene il pulsante

    private Button existingButton;        // Riferimento al pulsante esistente

    private int touchCount = 0;           // Contatore dei tocchi (clic sul pulsante)

    private bool raycastsEnabled = false; // Variabile per gestire lo stato dei raycast

    void Start()
    {
        // Disabilita i raycast degli elementi nella lista
        BlockRaycasts();

        // Cerca il pulsante esistente nel parent
        if (buttonParent != null)
        {
            existingButton = buttonParent.GetComponentInChildren<Button>();
            if (existingButton != null)
            {
                // Assegna la funzione al click del pulsante
                existingButton.onClick.AddListener(OnButtonClick);
            }
            else
            {
                Debug.LogError("Nessun pulsante trovato come figlio di " + buttonParent.name);
            }
        }
        else
        {
            Debug.LogError("Il parent del pulsante non è assegnato!");
        }
    }

    private void OnButtonClick()
    {
        // Incrementa il contatore ogni volta che il pulsante viene cliccato
        touchCount++;
        Debug.Log("Tocchi: " + touchCount);

        // Se sono stati cliccati 5 volte, abilita i raycast
        if (touchCount >= 5)
        {
            EnableRaycasts();
            Debug.Log("Raycast riattivati dopo 5 clic sul pulsante.");
        }

        // Disabilita il pulsante dopo il click
        if (existingButton != null)
        {
            existingButton.gameObject.SetActive(false);
        }

        Debug.Log("Pulsante premuto e disabilitato.");
    }

    private void BlockRaycasts()
    {
        foreach (Graphic element in elementsToBlock)
        {
            if (element != null)
            {
                element.raycastTarget = false; // Disabilita il raycast
            }
        }

        Debug.Log("Raycast bloccati.");
    }

    private void EnableRaycasts()
    {
        foreach (Graphic element in elementsToBlock)
        {
            if (element != null)
            {
                element.raycastTarget = true; // Riabilita il raycast
            }
        }

        raycastsEnabled = true; // I raycast sono stati abilitati
        Debug.Log("Raycast riattivati.");
    }
}
