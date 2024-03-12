using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public bool questCompleted;

    public GameObject wiseOldMan;

    void Start()
    {
        questCompleted = false;
    }

    void Update()
    {
        if (!questCompleted)
        {
            wiseOldMan.SetActive(false);
        }
        else
        {
            wiseOldMan.SetActive(true);
        }
    }
}
