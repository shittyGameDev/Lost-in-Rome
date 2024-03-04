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
            // Spawn positionen f�r symbolerna
            Vector3 spawnPosition = transform.position + Vector3.up * 1.5f;

            // Spawnar symbolen
            currentSymbolInstance = Instantiate(symbolPrefabs[randomIndex], spawnPosition, Quaternion.identity);

            // F�rst�r symbolen efter 10 sekunder
            Destroy(currentSymbolInstance, 10f);

            // V�ntar innan den spawnar n�sta symbol
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void CheckButtonPressed(Button buttonPressed)
    {
        // Get the sprite for the button pressed by the player
        Sprite pressedButtonSprite = buttonPressed.GetComponent<Image>().sprite;

        // Tittar ifall det finns en aktiv symbol att j�mf�ra med
        if (currentSymbolInstance != null)
        {
            // Tar fram spriten f�r den nuvarande symbolen
            Sprite currentSymbolSprite = currentSymbolInstance.GetComponent<SpriteRenderer>().sprite;

            // J�mf�r the nuvarande symbolen med knappen man tr�ck p�
            if (currentSymbolSprite == pressedButtonSprite)
            {
                // F�rst�r symbolen ifall man tryckt p� r�tt knapp
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
