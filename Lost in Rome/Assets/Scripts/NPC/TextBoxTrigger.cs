using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Detta script är kopplat till en trigger som aktiverar en textruta när spelaren går in i en trigger
public class TextBoxTrigger : MonoBehaviour
{
    public GameObject textBox;
    public TextMeshProUGUI text;
    public GameObject questText;

    public GameObject questIcon;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetComponent<QuestManager>().questCompleted == false)
        {
            textBox.SetActive(true);
            questText.SetActive(true);
            questIcon.SetActive(false);
        }
        else if(other.GetComponent<QuestManager>().questCompleted == true)
        {
            text.text = "Thank you! This will help us build bridges and many other things!";
            textBox.SetActive(true);
            
        }
    }

    // Denna metod körs när spelaren går ut ur triggerområdet
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textBox.SetActive(false);
        }
    }
}
