using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public GameObject endGameScreen;
    void Start()
    {
        // stänger av canvasen
        endGameScreen.SetActive(false);
    }

    public void showEndGame()
    {
        // Sätter på endgame skärmen
        endGameScreen.SetActive(true);
        // Fryser spelaren medan skärmen är aktiv
        Time.timeScale = 0;
    }

    public void hideEndGame()
    {
        // Gömmer end game skärmen
        endGameScreen.SetActive(false);
        // Gör så att spelaren kan röra sig
        Time.timeScale = 1;
    }
}
