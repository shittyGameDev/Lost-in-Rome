using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HubGameManager : MonoBehaviour
{
    public GameObject instructionsCanvas;
    void Start()
    {
        // S�tter p� canvasen n�r scenen startar
        instructionsCanvas.SetActive(true);
        // Fryser spelaren
        Time.timeScale = 0;
    }

    public void startGame()
    {
        // St�nger av canvasen
        instructionsCanvas.SetActive(false);
        // G�r att spelaren kan r�ra sig
        Time.timeScale = 1;
    }
}
