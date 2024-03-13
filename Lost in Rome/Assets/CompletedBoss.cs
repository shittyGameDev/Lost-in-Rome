using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletedBoss : MonoBehaviour
{
    public BossHealth bossHealth;
    public GameObject gameManager;

    public GameObject[] objectsToDeactivate;

    private void Start()
    {
        for (int i = 0; i < objectsToDeactivate.Length; i++)
        {
            objectsToDeactivate[i].SetActive(false);
        }
    }

    public void ShowObjects()
    {
        
        
            for (int i = 0; i < objectsToDeactivate.Length; i++)
            {
                objectsToDeactivate[i].SetActive(true);
            }
        
    }





    public void scoreUpdate()
    {
        
        
         gameManager.GetComponent<GameManager>().AddScore();
        
    }
    

    
}
