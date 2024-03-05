using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog 
{
    [SerializeField] List<string> rader;

    public List<string> Rader
    {
        get {  return rader; }
    }
}
