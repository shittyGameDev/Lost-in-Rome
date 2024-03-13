using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string CompletedLevelsKey = "completedLevels";

    public int completedLevels = 0;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        LoadCompletedLevels();
        Debug.Log("Completed levels: " + completedLevels);
    }

    void Start()
    {
    }

    public void AddScore()
    {
        completedLevels += 1;

        SaveCompletedLevels();
    }

    private void SaveCompletedLevels()
    {
        PlayerPrefs.SetInt(CompletedLevelsKey, completedLevels);
        PlayerPrefs.Save();
    }

    private void LoadCompletedLevels()
    {
        if (PlayerPrefs.HasKey(CompletedLevelsKey))
        {
            completedLevels = PlayerPrefs.GetInt(CompletedLevelsKey);
        }
    }
}
