using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject IngameMenu;
    
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(IngameMenu);
        IngameMenu.SetActive(false);
    }

    public void PauseGame()
    {
        IngameMenu.SetActive(true);
        Time.timeScale = 0;
        
    }

    public void Continue()
    {
        IngameMenu.SetActive(false);
        Time.timeScale = 1;
    }
   
}
