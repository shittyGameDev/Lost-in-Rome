using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SymbolSpawning : MonoBehaviour
{
    [SerializeField] private GameObject[] symbolPrefabs; // Array av symbol prefabs
    [SerializeField] private float spawnInterval = 10f;


    private int reputation;
    private int maxReputation = 40;
    public Slider slider;

    private bool gameWon;

    private GameObject lawSymbols;
    private GameObject foodSymbols;
    private GameObject warSymbols;
    private GameObject knowledgeSymbols;

    public GameObject winParticles;
    public GameObject winScreen;

    private GameObject currentSymbolInstance; // Referens till den nuvarande spawnade symbolen

    void Start()
    {
        float delay = Random.Range(2, 5);
        // Startar symbol spawnadet
        StartCoroutine(DelayedSymbolSpawn(delay));
        slider.maxValue = maxReputation;
        slider.value = reputation;
    }

    IEnumerator DelayedSymbolSpawn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        StartCoroutine(SpawnSymbol());
    }

    IEnumerator SpawnSymbol()
    {
        while (true)
        {
            // Tar fram random symboler
            int randomIndex = Random.Range(0, symbolPrefabs.Length);
            // Spawn positionen f�r symbolerna
            Vector3 spawnPosition = transform.position + Vector3.up * .5f;

            // Spawnar symbolen
            currentSymbolInstance = Instantiate(symbolPrefabs[randomIndex], spawnPosition, Quaternion.identity);

            // F�rst�r symbolen efter 10 sekunder
            Destroy(currentSymbolInstance, 5f);

            // V�ntar innan den spawnar n�sta symbol
            yield return new WaitForSeconds(Random.Range(spawnInterval - 5, spawnInterval + 10));
        }
    }

    public void DestroyLawSymbol()
    {
        lawSymbols = GameObject.FindGameObjectWithTag("Law");
        if (lawSymbols != null)
        {
            Destroy(lawSymbols);
            reputation += 2;
            slider.value = reputation;
            CheckWinCondition();
        }
    }

    public void DestroyFoodNCultureSymbols()
    {
        foodSymbols = GameObject.FindGameObjectWithTag("FoodNCulture");
        if (foodSymbols != null)
        {
            Destroy(foodSymbols);
            reputation += 2;
            slider.value = reputation;
            CheckWinCondition();
        }
    }

    public void DestroyWarSymbols()
    {
        warSymbols = GameObject.FindGameObjectWithTag("War");
        if (warSymbols != null)
        {
            Destroy(warSymbols);
            reputation += 2;
            slider.value = reputation;
            CheckWinCondition();
        }
    }
    public void DestroyKnowledgeSymbols()
    {
        knowledgeSymbols = GameObject.FindGameObjectWithTag("Knowledge");
        if (knowledgeSymbols != null)
        {
            Destroy(knowledgeSymbols);
            reputation += 2;
            slider.value = reputation;
            CheckWinCondition();
        }
    }

    public void CheckWinCondition()
    {
        if (reputation >= maxReputation)
        {
            winParticles.SetActive(true);
            gameWon = true;
            if(gameWon == true)
            {
                Time.timeScale = 0;
                winScreen.SetActive(true);
            }
        }
    }
}
