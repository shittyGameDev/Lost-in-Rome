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
        if (playerIsNearDoor && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(EnterLibraryCutscene());
        }
    }

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
