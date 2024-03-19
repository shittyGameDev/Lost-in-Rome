using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public GameObject endGameScreen;
    void Start()
    {
        // st�nger av canvasen
        endGameScreen.SetActive(false);
    }

    public void showEndGame()
    {
        // S�tter p� endgame sk�rmen
        endGameScreen.SetActive(true);
        // Fryser spelaren medan sk�rmen �r aktiv
        Time.timeScale = 0;
    }

    public void hideEndGame()
    {
        // G�mmer end game sk�rmen
        endGameScreen.SetActive(false);
        // G�r s� att spelaren kan r�ra sig
        Time.timeScale = 1;
    }
}
