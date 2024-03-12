using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxTrigger : MonoBehaviour
{
    public GameObject textBox;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetComponent<QuestManager>().questCompleted == false)
        {
            textBox.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textBox.SetActive(false);
        }
    }
}
