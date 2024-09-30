using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    void Start()
    {
        // Blocca il cursore e lo nasconde
        Cursor.lockState = CursorLockMode.Locked;  // Blocca il cursore al centro della schermata
        Cursor.visible = false;                   // Nasconde il cursore
    }

    void Update()
    {
        // Controlla se premendo "Esc" vuoi liberare il cursore per l'uscita dal gioco o debugging
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Sblocca il cursore e lo rende visibile
            Cursor.lockState = CursorLockMode.None;  // Sblocca il cursore
            Cursor.visible = true;                   // Rende il cursore visibile
        }
    }
}
