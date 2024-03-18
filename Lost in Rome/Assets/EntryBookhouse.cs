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
        //kollar om spelaren �r n�ra d�rren och har tryckt p� E f�r att starta coroutinen
        if (playerIsNearDoor && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(EnterLibraryCutscene());//startar den f�r n�r man g�r in i biblioteket
        }
    }
    //coroutine f�r att hantera n�r spelaren g�r in i biblioteket med en enkel "cutscene"
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
