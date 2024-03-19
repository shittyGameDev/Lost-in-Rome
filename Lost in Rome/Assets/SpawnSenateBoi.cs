using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSenateBoi : MonoBehaviour
{
    public GameObject SenateBoi;
    public GameManager gameManager;
    void Start()
    {
        SenateBoi.SetActive(false);
    }

    private void FixedUpdate()
    {
        spawnSenate();
    }

    public void spawnSenate()
    {
        if(gameManager.completedLevels >= 3)
        {
            SenateBoi.SetActive(true);
        }
    }
}
