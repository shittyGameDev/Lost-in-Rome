using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SymbolSpawning : MonoBehaviour
{
    [SerializeField] private GameObject[] symbolPrefabs; // Array av symbol prefabs
    [SerializeField] private float spawnInterval = 15f;

    private GameObject currentSymbolInstance; // Referens till den nuvarande spawnade symbolen

    private void Start()
    {
        // Startar symbol spawnadet
        StartCoroutine(SpawnSymbol());
    }

    IEnumerator SpawnSymbol()
    {
        while (true)
        {
            // Tar fram random symboler
            int randomIndex = Random.Range(0, symbolPrefabs.Length);
            // Spawn positionen för symbolerna
            Vector3 spawnPosition = transform.position + Vector3.up * 1.5f;

            // Spawnar symbolen
            currentSymbolInstance = Instantiate(symbolPrefabs[randomIndex], spawnPosition, Quaternion.identity);

            // Förstör symbolen efter 10 sekunder
            Destroy(currentSymbolInstance, 10f);

            // Väntar innan den spawnar nästa symbol
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void CheckButtonPressed(Button buttonPressed)
    {
        // Get the sprite for the button pressed by the player
        Sprite pressedButtonSprite = buttonPressed.GetComponent<Image>().sprite;

        // Tittar ifall det finns en aktiv symbol att jämföra med
        if (currentSymbolInstance != null)
        {
            // Tar fram spriten för den nuvarande symbolen
            Sprite currentSymbolSprite = currentSymbolInstance.GetComponent<SpriteRenderer>().sprite;

            // Jämför the nuvarande symbolen med knappen man tröck på
            if (currentSymbolSprite == pressedButtonSprite)
            {
                // Förstör symbolen ifall man tryckt på rätt knapp
                Destroy(currentSymbolInstance);
                Debug.Log("Correct button pressed!");
            }
            else
            {
                Debug.Log("Incorrect button pressed!");
            }
        }
        else
        {
            Debug.Log("No current symbol to compare with.");
        }
    }
}
