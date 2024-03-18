using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    public string questionText; //texten f�r sj�lva fr�gan
    public string[] answers; //en array med svarsalternativ
    public int correctAnswerIndex; //index f�r det korrekta svarsalternativet i arrayen ovan

    //en konstruktor s� man kan dynamiskt skapa nya fr�gor 
    public Question(string questionText, string[] answers, int correctAnswerIndex)
    {
        this.questionText = questionText;
        this.answers = answers;
        this.correctAnswerIndex = correctAnswerIndex;
    }
}
