using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : MonoBehaviour
{
    public string questionText; // Texten f�r sj�lva fr�gan
    public string[] answers; // En array med svarsalternativ
    public int correctAnswerIndex; // Index f�r det korrekta svarsalternativet i arrayen ovan

    // En konstruktor �r inte n�dv�ndig men kan vara anv�ndbar om du vill skapa fr�gor dynamiskt via kod
    public Question(string questionText, string[] answers, int correctAnswerIndex)
    {
        this.questionText = questionText;
        this.answers = answers;
        this.correctAnswerIndex = correctAnswerIndex;
    }
}
