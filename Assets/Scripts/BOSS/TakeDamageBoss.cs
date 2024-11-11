using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Per gestire UI come Button e Image

public class ButtonSpawner : MonoBehaviour
{
    public GameObject buttonPrefab; // Prefab del bottone da creare
    public GameObject imagePrefab;  // Prefab dell'immagine da visualizzare
    private Transform canvasTransform; // Canvas dove posizionare i bottone e l'immagine

    public BossHealth bossHealth; // Riferimento al BossHealth per ridurre la vita
    public float spawnInterval = 5f; // Tempo in secondi tra ogni generazione del bottone
    private bool isActive = false; // Variabile che determina se è attivo il meccanismo di spawn

    void Start()
    {
        // Cerca il Canvas nella scena
        canvasTransform = FindObjectOfType<Canvas>().transform; // Ottieni il componente Canvas e il suo Transform
        if (canvasTransform == null)
        {
            Debug.LogError("Canvas non trovato nella scena. Assicurati che ci sia un oggetto Canvas.");
            return;
        }

        // Avvia la coroutine per generare il bottone ogni tot secondi
        StartCoroutine(SpawnButton());
    }

    // Coroutine che genera il bottone ogni intervallo
    IEnumerator SpawnButton()
    {
        while (true)
        {
            if (isActive)
            {
                // Crea un nuovo bottone
                GameObject button = Instantiate(buttonPrefab, canvasTransform);
                button.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-300f, 300f), Random.Range(-200f, 200f));

                // Crea un'immagine accanto al bottone
                GameObject image = Instantiate(imagePrefab, canvasTransform);
                image.GetComponent<RectTransform>().anchoredPosition = new Vector2(button.transform.position.x, button.transform.position.y + 50f);

                // Aggiungi l'azione al bottone
                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(() => OnButtonClicked()); // Imposta la funzione da chiamare quando il bottone è premuto

                // Aspetta l'intervallo specificato prima di generare un nuovo bottone
                yield return new WaitForSeconds(spawnInterval);
            }
            else
            {
                // Se la variabile isActive è false, aspetta 1 secondo e controlla di nuovo
                yield return new WaitForSeconds(1f);
            }
        }
    }

    // Funzione chiamata quando il bottone viene premuto
    void OnButtonClicked()
    {
        // Se il boss esiste e ha la funzione TakeDamage, riduciamo la vita del boss di 5
        if (bossHealth != null)
        {
            bossHealth.TakeDamage(5f); // Riduci la vita del boss di 5
        }
    }

    // Funzione per attivare o disattivare lo spawn dei bottoni
    public void SetActive(bool active)
    {
        isActive = active;
    }
}
