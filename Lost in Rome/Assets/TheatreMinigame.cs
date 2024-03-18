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
    public GameObject questionPanel;
    public TMP_Text questionText;
    public Button[] answerButtons; 
    public Button restartButton;
    public Button exitButton;
    public List<Question> questions; //lista med f�rgor som ska st�llas i minigamet
    public GameObject endGamePanel; 
    public TMP_Text endGameText; 
    public Transform insideTheatrePosition;
    public Transform outsideTheatrePosition; 
    private GameObject player;
    private bool playerNear = false;
    private int currentQuestionIndex = 0;
    private int correctAnswers = 0;

    public GameObject hubCanvas;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //d�ligt s�tt att g�ra det p� I know
        endGamePanel.SetActive(false);

    }

    void Update()
    {
        //kontrollerar om spelaren �r n�ra, har l�st en bok (f�ruts�tter en extern kontroll) och trycker p� tangenten E f�r att starta minispelet
        if (playerNear && Book.hasReadBook && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(StartMinigameSequence()); 
        }
    }
    //corutine f�r att spela cutscene o flytta spelaren till teatern
    IEnumerator StartMinigameSequence()
    {
        player.GetComponent<PlayerController>().CanMove = false;
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        player.transform.position = insideTheatrePosition.position;
        blackScreen.SetActive(false);
        questionPanel.SetActive(true);
        StartMinigame(); //startar spelet
    }
    //�terst�ller minigamet och visar n�sta fr�ga
    private void StartMinigame()
    {
        currentQuestionIndex = 0;
        correctAnswers = 0;
        ShowNextQuestion();
    }
    //visar n�sta fr�ga i minispelet eller avslutar det om det inte finns fler fr�gor
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
                    int index = i;
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
    //hanterar valet av svar och spelar upp en ljudreaktion baserat p� om svaret var r�tt eller fel
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
    //avslutar spelet och visar resultatet genom att starta en coroutine
    void FinishMinigame()
    {
        StartCoroutine(FinishSequence());
    }
    //en coroutine som hanterar sekvensen f�r att avsluta spelet
    IEnumerator FinishSequence()
    {
        questionPanel.SetActive(false);
        endGamePanel.SetActive(true);
        if (correctAnswers == questions.Count)
        {
            restartButton.gameObject.SetActive(false); 
            exitButton.gameObject.SetActive(false);   
            audienceReaction.PlayOneShot(applauseSound);
            endGameText.text = "Congrats! You have learned about Medea, an important play from ancient Rome. You have learned all the acts from the play and how the actor portrayed Medea in the play.";
            yield return new WaitForSeconds(applauseSound.length);
            blackScreen.SetActive(true);
            endGamePanel.SetActive(false);
            yield return new WaitForSeconds(1.5f);
            player.transform.position = outsideTheatrePosition.position;
            blackScreen.SetActive(false);
            player.GetComponent<PlayerController>().CanMove = true;
            yield return new WaitForSeconds(2f);
            hubCanvas.SetActive(true);
        }
        else
        {
            //om inte alla svar var korrekta, visar knapparna f�r att starta om eller l�mna spelet
            restartButton.gameObject.SetActive(true);
            exitButton.gameObject.SetActive(true);
            audienceReaction.PlayOneShot(booSound);
            endGameText.text = "Tyv�rr, du klarade inte alla fr�gor. F�rs�k igen!";
        }
        yield return new WaitForSeconds(2f);  
    }

    public void RestartMinigame()
    {
        StartCoroutine(RestartMinigameSequence());
    }
    //en coroutine som startar om spelet, �terst�ller allt och kallar p� startminigame igen f�r att b�rja om
    IEnumerator RestartMinigameSequence()
    {
        endGamePanel.SetActive(false);
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(1f); 
        blackScreen.SetActive(false);

        player.transform.position = insideTheatrePosition.position;

        currentQuestionIndex = 0;
        correctAnswers = 0;

        questionPanel.SetActive(true);

        StartMinigame();
    }

    public void ExitMinigame()
    {
        StartCoroutine(ExitMinigameSequence()); 
    }
    //coroutine som hanterar n�r spelaren ska l�mna spelet 
    IEnumerator ExitMinigameSequence()
    {
        endGamePanel.SetActive(false);
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(1f);

        player.transform.position = outsideTheatrePosition.position;

        blackScreen.SetActive(false);
        questionPanel.SetActive(false);
        blackScreen.SetActive(false);
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