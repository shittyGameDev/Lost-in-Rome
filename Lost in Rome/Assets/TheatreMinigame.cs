using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TheatreMinigame : MonoBehaviour
{
    public GameObject blackScreen;
    public AudioSource audienceReaction;
    public AudioClip booSound, applauseSound, cheerSound;
    public TMP_Text questionText; // UI Text f�r att visa fr�gan
    public Button[] answerButtons; // Knappar f�r svarsalternativ
    public Button restartButton;
    public Button exitButton;
    public List<Question> questions; // Lista med alla fr�gor
    public GameObject endGamePanel; // Panel som visas i slutet av spelet
    public TMP_Text endGameText; // Text som visar resultatet (vunnit eller f�rlorat)
    public Transform insideTheatrePosition; // Spelarens position inuti teatern
    public Transform outsideTheatrePosition; // Spelarens position utanf�r teatern
    private GameObject player;
    private bool playerNear = false;
    private int currentQuestionIndex = 0;
    private int correctAnswers = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Antag att spelaren har taggen "Player"
        endGamePanel.SetActive(false);
    }

    void Update()
    {
        if (playerNear && Book.hasReadBook && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(StartMinigameSequence());
        }
    }

    IEnumerator StartMinigameSequence()
    {
        player.GetComponent<PlayerController>().CanMove = false; // Antag att spelarkontrollern har en CanMove-property
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        player.transform.position = insideTheatrePosition.position;
        blackScreen.SetActive(false);
        questionText.gameObject.SetActive(true);
        foreach(var button  in answerButtons)
        {
            button.gameObject.SetActive(true);
        }
        StartMinigame();
    }

    private void StartMinigame()
    {
        currentQuestionIndex = 0;
        correctAnswers = 0;
        ShowNextQuestion();
    }

    private void ShowNextQuestion()
    {
        if (currentQuestionIndex < questions.Count)
        {
            Question currentQuestion = questions[currentQuestionIndex];
            questionText.text = currentQuestion.questionText;
            for (int i = 0; i < answerButtons.Length; i++)
            {
                if (i < currentQuestion.answers.Length)
                {
                    answerButtons[i].gameObject.SetActive(true);
                    answerButtons[i].GetComponentInChildren<TMP_Text>().text = currentQuestion.answers[i];
                    answerButtons[i].onClick.RemoveAllListeners();
                    int index = i; // Capture index for lambda expression
                    answerButtons[i].onClick.AddListener(() => ChooseOption(index));
                }
                else
                {
                    answerButtons[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            FinishMinigame();
        }
    }

    public void ChooseOption(int optionIndex)
    {
        if (optionIndex == questions[currentQuestionIndex].correctAnswerIndex)
        {
            correctAnswers++;
            audienceReaction.PlayOneShot(cheerSound);
        }
        else
        {
            audienceReaction.PlayOneShot(booSound);
        }

        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Count)
        {
            ShowNextQuestion();
        }
        else
        {
            FinishMinigame();
        }
    }

    void FinishMinigame()
    {
        StartCoroutine(FinishSequence());
    }

    IEnumerator FinishSequence()
    {
        questionText.gameObject.SetActive(false);
        foreach (var button in answerButtons)
        {
            button.gameObject.SetActive(false);
        }
        endGamePanel.SetActive(true);
        if (correctAnswers == questions.Count)
        {
            audienceReaction.PlayOneShot(applauseSound);
            endGameText.text = "Grattis! Du klarade alla fr�gor!";
            yield return new WaitForSeconds(applauseSound.length);
            blackScreen.SetActive(true);
            endGamePanel.SetActive(false);
            yield return new WaitForSeconds(1.5f);
            player.transform.position = outsideTheatrePosition.position;
            player.GetComponent<PlayerController>().CanMove = true;
        }
        else
        {   
            restartButton.gameObject.SetActive(true);
            exitButton.gameObject.SetActive(true);
            audienceReaction.PlayOneShot(booSound);
            endGameText.text = "Tyv�rr, du klarade inte alla fr�gor. F�rs�k igen!";
        }
        yield return new WaitForSeconds(2f); // Ge spelaren tid att l�sa texten  
    }

    public void RestartMinigame()
    {
        StartCoroutine(RestartMinigameSequence());
    }

    IEnumerator RestartMinigameSequence()
    {
        // Visa en svart sk�rm f�r �verg�ngen
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(1f); // V�nta en sekund (eller hur l�ng tid du vill att cutscenen ska vara)
        endGamePanel.SetActive(false);
        // D�lj svarta sk�rmen och starta om minispelet
        blackScreen.SetActive(false);

        // �terst�ll spelarpositionen om det beh�vs
        player.transform.position = insideTheatrePosition.position;

        // �terst�ll nuvarande fr�geindex och korrekta svar till 0
        currentQuestionIndex = 0;
        correctAnswers = 0;

        // Aktivera n�dv�ndiga UI-element
        questionText.gameObject.SetActive(true);
        foreach (var button in answerButtons)
        {
            button.gameObject.SetActive(true);
        }
        endGamePanel.SetActive(false);

        // B�rja om minispelet
        StartMinigame();
    }

    public void ExitMinigame()
    {
        StartCoroutine(ExitMinigameSequence()); 
    }

    IEnumerator ExitMinigameSequence()
    {
        // Visa en svart sk�rm f�r �verg�ngen
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(1f); // V�nta en sekund (eller hur l�ng tid du vill att cutscenen ska vara)
                                             // D�lj minispel-UI
        endGamePanel.SetActive(false);

        // Flytta spelaren till positionen utanf�r teatern
        player.transform.position = outsideTheatrePosition.position;

        // G�m alla UI-element relaterade till minispelet
        blackScreen.SetActive(false);
        questionText.gameObject.SetActive(false);
        foreach (var button in answerButtons)
        {
            button.gameObject.SetActive(false);
        }
        // D�lj svarta sk�rmen och starta om minispelet
        blackScreen.SetActive(false);
        // Aktivera spelarr�relsen igen
        player.GetComponent<PlayerController>().CanMove = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
        }
    }
}