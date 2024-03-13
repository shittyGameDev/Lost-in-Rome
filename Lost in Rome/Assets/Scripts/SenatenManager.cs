using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenatenManager : MonoBehaviour
{
    public GameObject gameCanvas;
    public GameObject instructionsCanvas;
    // Start is called before the first frame update
    void Start()
    {
        gameCanvas.SetActive(false);
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        gameCanvas.SetActive(true);
        instructionsCanvas.SetActive(false);
        Time.timeScale = 1;
    }
}
