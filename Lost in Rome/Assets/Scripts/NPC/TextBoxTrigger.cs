using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBoxTrigger : MonoBehaviour
{
    public GameObject textBox;
    public TextMeshProUGUI text;
    public GameObject questText;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetComponent<QuestManager>().questCompleted == false)
        {
            textBox.SetActive(true);
            questText.SetActive(true);
        }
        else if(other.GetComponent<QuestManager>().questCompleted == true)
        {
            text.text = "Thank you! This will help us build bridges and many other things!";
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
