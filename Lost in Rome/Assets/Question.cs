using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    public string questionText; //texten för själva frågan
    public string[] answers; //en array med svarsalternativ
    public int correctAnswerIndex; //index för det korrekta svarsalternativet i arrayen ovan

    //en konstruktor så man kan dynamiskt skapa nya frågor 
    public Question(string questionText, string[] answers, int correctAnswerIndex)
    {
        this.questionText = questionText;
        this.answers = answers;
        this.correctAnswerIndex = correctAnswerIndex;
    }
}
