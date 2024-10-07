using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HideVideoAfterTime : MonoBehaviour
{
    public RawImage videoRawImage;   // La RawImage che contiene il video
    public float hideAfterSeconds = 5f;  // Tempo di attesa prima della scomparsa
    public float fadeDuration = 1f;  // Durata della dissolvenza (in secondi)

    void Start()
    {
        // Avvia la coroutine che attende e poi nasconde la RawImage con dissolvenza
        StartCoroutine(FadeOutVideo());
    }

    IEnumerator FadeOutVideo()
    {
        // Attende per il tempo specificato prima di iniziare la dissolvenza
        yield return new WaitForSeconds(hideAfterSeconds);

        // Ottiene il colore attuale della RawImage
        Color currentColor = videoRawImage.color;
        float fadeSpeed = 1f / fadeDuration;  // Velocità della dissolvenza
        float progress = 0f;

        // Ciclo che gestisce la dissolvenza
        while (progress < 1f)
        {
            progress += Time.deltaTime * fadeSpeed;
            currentColor.a = Mathf.Lerp(1f, 0f, progress);  // Riduce l'alpha gradualmente
            videoRawImage.color = currentColor;
            yield return null;  // Aspetta il frame successivo
        }

        // Nasconde la RawImage dopo la dissolvenza
        videoRawImage.enabled = false;
    }
}
