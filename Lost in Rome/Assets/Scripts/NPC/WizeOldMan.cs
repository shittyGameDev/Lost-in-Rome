using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizeOldMan : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject canvas;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.AddScore();
            Debug.Log("Score: " + gameManager.completedLevels);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.SetActive(true);
        }
    }
}
