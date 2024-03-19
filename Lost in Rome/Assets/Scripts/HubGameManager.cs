using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HubGameManager : MonoBehaviour
{
    public GameObject instructionsCanvas;
    void Start()
    {
        // Sätter på canvasen när scenen startar
        instructionsCanvas.SetActive(true);
        // Fryser spelaren
        Time.timeScale = 0;
    }

    public void startGame()
    {
        // Stänger av canvasen
        instructionsCanvas.SetActive(false);
        // Gör att spelaren kan röra sig
        Time.timeScale = 1;
    }
}
