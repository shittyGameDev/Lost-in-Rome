using UnityEngine;
using UnityEngine.UI;

public class SymbolGameManager : MonoBehaviour
{
    public GameObject[] symbolButtons; // Array av UI-knappar som motsvarar symboler

    public void CheckSymbol(int buttonIndex)
    {
        // Hämta spriten för den tryckta knappen
        Sprite pressedSymbol = symbolButtons[buttonIndex].GetComponent<Image>().sprite;

        // Hämta spriten för den aktuella symbolen över NPC:ers huvuden
        Sprite currentSymbol = SymbolSpawning.currentSymbolObject.GetComponent<SpriteRenderer>().sprite;

        // Kontrollera om den tryckta symbolen matchar den aktuella symbolen över NPC:ers huvuden
        if (pressedSymbol == currentSymbol)
        {
            // Hantera korrekt inmatning
            Debug.Log("Korrekt symbol tryckt!");
        }
        else
        {
            // Hantera felaktig inmatning
            Debug.Log("Felaktig symbol tryckt!");
        }
    }
}