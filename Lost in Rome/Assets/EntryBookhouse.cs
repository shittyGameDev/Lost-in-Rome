using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryBookhouse : MonoBehaviour
{
    public GameObject player;
    public Transform libraryInsidePosition;
    public GameObject blackScreen;
    public GameObject npcLucius;
    public GameObject npcLucius2;
    private bool playerIsNearDoor = false;

    private void Update()
    {
        //kollar om spelaren är nära dörren och har tryckt på E för att starta coroutinen
        if (playerIsNearDoor && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(EnterLibraryCutscene());//startar den för när man går in i biblioteket
        }
    }
    //coroutine för att hantera när spelaren går in i biblioteket med en enkel "cutscene"
    private IEnumerator EnterLibraryCutscene()
    {
        blackScreen.SetActive(true); 
        yield return new WaitForSeconds(1.5f); 
        if(npcLucius != null ) { Destroy(npcLucius); }
        if(npcLucius2 != null ) { npcLucius2.SetActive(true); }
        player.transform.position = libraryInsidePosition.position;

        yield return new WaitForSeconds(1.5f);
        blackScreen.SetActive(false); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNearDoor = true;
            Debug.Log(playerIsNearDoor);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNearDoor = false;
            Debug.Log(playerIsNearDoor);
        }
    }
}
