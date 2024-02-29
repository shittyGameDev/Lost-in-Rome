using System.Collections;
using UnityEngine;

public class SymbolSpawning : MonoBehaviour
{
    [SerializeField] private GameObject[] symbolPrefabs; // Array av symbol prefabs
    

    private void Start()
    {
        // Startar en coroutine för att spawna symboler
        StartCoroutine(SpawnSymbol());
    }



    IEnumerator SpawnSymbol()
    {
        while (true)
        {
            // Tittar ifall det finns symbol prefabs 
            if (symbolPrefabs != null && symbolPrefabs.Length > 0)
            {
                //Tar fram en random symbol från arrayen
                int randomIndex = Random.Range(0, symbolPrefabs.Length);
                //Spawnpunkten för symbolen
                Vector3 spawnPosition = transform.position + Vector3.up * 1.5f;

                //Spawnar symbol
                GameObject symbolInstance = Instantiate(symbolPrefabs[randomIndex], spawnPosition, Quaternion.identity);

                //Förstör symbolen efter 10 sek
                Destroy(symbolInstance, 10f);
                //Väntar innan nästa symbol spawnas
                yield return new WaitForSeconds(10f);
            }

        }
    }
}
