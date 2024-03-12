using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : MonoBehaviour
{
    public string questionText; // Texten för själva frågan
    public string[] answers; // En array med svarsalternativ
    public int correctAnswerIndex; // Index för det korrekta svarsalternativet i arrayen ovan

    // En konstruktor är inte nödvändig men kan vara användbar om du vill skapa frågor dynamiskt via kod
    public Question(string questionText, string[] answers, int correctAnswerIndex)
    {
        this.questionText = questionText;
        this.answers = answers;
        this.correctAnswerIndex = correctAnswerIndex;
    }
}
