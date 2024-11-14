using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Per gestire UI come Button
using TMPro; // Importazione del namespace TextMeshPro per usare TextMeshProUGUI

public class ButtonSpawner : MonoBehaviour
{
    public GameObject prefabWithButton; // Prefab che contiene il bottone per infliggere danno
    private Transform guiCanvasTransform; // Canvas "GUI" dove posizionare il prefab

    public BossHealth bossHealth; // Riferimento al BossHealth per ridurre la vita
    public PlayerHealth playerHealth;
    public float spawnInterval = 5f; // Tempo in secondi tra ogni generazione del prefab
    public float despawnInterval = 3f; // Tempo in secondi dopo il quale il prefab viene distrutto
    public float pressDamage = 5f;
    private bool isActive = false; // Variabile che determina se è attivo il meccanismo di spawn

    // Lista di frasi preconfigurate che vogliamo mostrare nel testo del bottone
    public string[] buttonTexts = {
        "Sono un Tortellino Muhaha",
        "A Terroneee",
        "Ma poooo",
        "Daje Roma",
        "yahoooo"
    };

    void Start()
    {
        // Cerca il Canvas specifico chiamato "GUI" nella scena
        GameObject guiCanvas = GameObject.Find("GUI");
        if (guiCanvas != null)
        {
            guiCanvasTransform = guiCanvas.transform;
        }
        else
        {
            Debug.LogError("Canvas 'GUI' non trovato nella scena. Assicurati che ci sia un oggetto Canvas con questo nome.");
            return;
        }

        // Avvia la coroutine per generare il prefab ogni tot secondi
        StartCoroutine(SpawnPrefab());
    }

    // Coroutine che genera il prefab ogni intervallo
    IEnumerator SpawnPrefab()
    {
        while (true)
        {
            //yield return new WaitForSeconds(10f);
            if (isActive && bossHealth.GetCurrentHealth() > 0 /*&& playerHealth.GetCurrentHealth() > 0*/)
            {
                // Crea un nuovo prefab contenente il bottone all'interno del Canvas GUI
                GameObject newPrefab = Instantiate(prefabWithButton, guiCanvasTransform);

                // Posiziona il prefab in una posizione casuale nel Canvas
                newPrefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                    Random.Range(-300f, 300f),
                    Random.Range(-200f, 200f)
                );
                Debug.Log("GENERATO BOSSHEAD");

                // Metti in pausa il gioco (tutti i movimenti e azioni saranno interrotti)
                Time.timeScale = 0f;

                // Aggiungi l'azione al bottone nel prefab
                Button btn = newPrefab.GetComponentInChildren<Button>();
                if (btn != null)
                {
                    btn.onClick.AddListener(() => OnButtonClicked(newPrefab)); // Passa il riferimento al prefab
                }
                else
                {
                    Debug.LogError("Nessun componente Button trovato nel prefab.");
                }

                // Avvia una coroutine per distruggere il prefab dopo un certo intervallo
                StartCoroutine(DespawnPrefab(newPrefab));

                // Aspetta l'intervallo specificato prima di generare un nuovo prefab
                yield return new WaitForSecondsRealtime(spawnInterval); // Usa WaitForSecondsRealtime per ignorare Time.timeScale
            }
            else
            {
                // Se la variabile isActive è false, aspetta 1 secondo e controlla di nuovo
                yield return new WaitForSeconds(1f);
            }
        }
    }

    // Coroutine per distruggere il prefab dopo un intervallo di tempo
    IEnumerator DespawnPrefab(GameObject prefab)
    {
        // Aspetta per il tempo di despawn specificato (ignorando Time.timeScale)
        yield return new WaitForSecondsRealtime(despawnInterval);

        // Distrugge il prefab dal Canvas
        Destroy(prefab);

        // Ripristina il Time.timeScale (sospensione del gioco finisce)
        Time.timeScale = 1f;
    }

    // Funzione chiamata quando il bottone nel prefab viene premuto
    void OnButtonClicked(GameObject clickedPrefab)
    {
        // Se il boss esiste e ha la funzione TakeDamage, riduciamo la vita del boss di 5
        if (bossHealth != null)
        {
            bossHealth.TakeDamage(pressDamage); // Riduci la vita del boss di 5
        }

        // Cerca tra tutti i figli del prefab per trovare un componente TextMeshProUGUI il cui nome contiene "Parole"
        TextMeshProUGUI[] allTextComponents = clickedPrefab.GetComponentsInChildren<TextMeshProUGUI>();

        // Trova il primo TextMeshProUGUI il cui GameObject contiene "Parole" nel nome
        TextMeshProUGUI targetText = null;
        foreach (var textComponent in allTextComponents)
        {
            if (textComponent.gameObject.name.Contains("Parole"))
            {
                targetText = textComponent;
                break; // Esci dal ciclo appena troviamo il primo che soddisfa la condizione
            }
        }

        // Se il testo è stato trovato
        if (targetText != null)
        {
            // Cambia il testo del bottone scegliendo una nuova frase casuale
            targetText.text = buttonTexts[Random.Range(0, buttonTexts.Length)];
            Debug.Log("Testo del bottone cambiato su: " + targetText.text);

            // Aggiungi una rotazione casuale al testo (solo il testo, non il RectTransform)
            float randomRotation = Random.Range(-11f, 11f); // Rotazione casuale tra -21 e 21 gradi

            // Ruota solo il componente TextMeshProUGUI (non il RectTransform)
            targetText.transform.rotation = Quaternion.Euler(0f, 0f, randomRotation);
            Debug.Log("Rotazione del testo cambiata su: " + randomRotation);
        }
        else
        {
            Debug.LogError("Non è stato trovato un componente 'TextMeshProUGUI' con il nome che contiene 'Parole' nel prefab.");
        }
    }

    // Funzione per attivare o disattivare lo spawn del prefab
    public void SetActive(bool active)
    {
        isActive = active;
    }
}
