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

    private GameObject lawSymbols;
    private GameObject foodSymbols;
    private GameObject warSymbols;
    private GameObject knowledgeSymbols;

    private GameObject currentSymbolInstance; // Referens till den nuvarande spawnade symbolen

    private void Start()
    {
        // Startar symbol spawnadet
        StartCoroutine(SpawnSymbol());
        slider.maxValue = maxReputation;
        slider.value = reputation;
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
            yield return new WaitForSeconds(Random.Range(spawnInterval - 5, spawnInterval + 5));
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
        }
    }
}
