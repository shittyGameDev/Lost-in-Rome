using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public GameObject player;
    public Transform outsideDoorPosition; 
    public GameObject blackScreen;
    private bool playerIsNearDoor = false;

    private void Update()
    {
        if (playerIsNearDoor && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(ExitLibraryCutscene());
        }
    }

    private IEnumerator ExitLibraryCutscene()
    {
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        player.transform.position = outsideDoorPosition.position;

        yield return new WaitForSeconds(1.5f);
        blackScreen.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNearDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNearDoor = false;
        }
    }
}
