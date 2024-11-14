using UnityEngine;

public class FullScreenPanel : MonoBehaviour
{
    void Start()
    {
        // Ottieni il RectTransform del Panel
        RectTransform rectTransform = GetComponent<RectTransform>();

        // Imposta gli ancoraggi per occupare tutto lo schermo
        rectTransform.anchorMin = new Vector2(0f, 0f);  // Angolo in basso a sinistra
        rectTransform.anchorMax = new Vector2(1f, 1f);  // Angolo in alto a destra

        // Imposta gli offset a zero per far sì che il Panel si adatti perfettamente alla finestra
        rectTransform.offsetMin = new Vector2(0f, 0f);  // Offset per il lato sinistro e in basso
        rectTransform.offsetMax = new Vector2(0f, 0f);  // Offset per il lato destro e in alto
    }
}
