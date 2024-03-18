using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    // Ett script som håller koll på om en quest är klar eller inte
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
