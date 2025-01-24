using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;  // Aggiungi questa libreria per gestire il caricamento delle scene

public class TextAudioManager : MonoBehaviour
{
    public TMP_Text dialogueText;       // Riferimento al TextMeshPro
    public AudioSource audioSource;     // Riferimento all'AudioSource
    public string[] dialogues;          // Array di frasi da visualizzare
    public AudioClip[] audioClips;     // Array di audio da riprodurre
    public string sceneName;            // Nome della scena da caricare al termine

    private int currentDialogueIndex = 0;

    void Start()
    {
        StartCoroutine(PlayDialogue());
    }

    IEnumerator PlayDialogue()
    {
        // Cicla attraverso ogni frase e audio
        for (int i = 0; i < dialogues.Length; i++)
        {
            // Mostra il testo
            dialogueText.text = dialogues[i];

            // Riproduci l'audio
            audioSource.clip = audioClips[i];
            audioSource.Play();

            // Aspetta che l'audio finisca prima di passare alla frase successiva
            yield return new WaitForSeconds(audioClips[i].length);

            // Eventualmente aggiungi un delay prima della prossima frase, se necessario
            yield return new WaitForSeconds(0.5f); // Aggiungi un piccolo ritardo tra le frasi
        }

        // Quando tutte le frasi sono passate, carica la scena
        LoadNextScene();
    }

    void LoadNextScene()
    {
        // Carica la scena specificata tramite il nome
        SceneManager.LoadScene(sceneName);
    }
}
