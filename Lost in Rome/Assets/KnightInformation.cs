using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightInformation : MonoBehaviour
{
    public GameObject textBox;
    // Start is called before the first frame update
    void Start()
    {
        textBox.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
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

    public void CloseTextBox()
    {
        textBox.SetActive(false);
    }
}
