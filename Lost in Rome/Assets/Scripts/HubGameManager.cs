using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HubGameManager : MonoBehaviour
{
    public GameObject instructionsCanvas;
    void Start()
    {
        instructionsCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void startGame()
    {
        instructionsCanvas.SetActive(false);
        Time.timeScale = 1;
    }
}
