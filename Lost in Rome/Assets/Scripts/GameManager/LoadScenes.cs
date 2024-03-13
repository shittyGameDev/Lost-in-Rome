using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    public void Colloseum ()
    {
        SceneManager.LoadScene("Collo");
    }
    
    public void Cement()
    {
        SceneManager.LoadScene("Victor");
    }

    public void Theater()
    {
        SceneManager.LoadScene("Kingpin");
    }

    public void Senat()
    {
        SceneManager.LoadScene("Senat");
    }
}
