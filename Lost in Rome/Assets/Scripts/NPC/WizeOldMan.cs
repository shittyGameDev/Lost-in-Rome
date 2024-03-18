using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Detta script är kopplat till en trigger som aktiverar en textruta när spelaren går in i en trigger
public class WizeOldMan : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject canvas;

    void Start()
    {
        canvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.GetComponent<GameManager>().AddScore();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canvas.SetActive(true);
        }
    }
}
