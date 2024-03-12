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
    public TMP_Text questionText; // UI Text för att visa frågan
    public Button[] answerButtons; // Knappar för svarsalternativ
    public Button restartButton;
    public Button exitButton;
    public List<Question> questions; // Lista med alla frågor
    public GameObject endGamePanel; // Panel som visas i slutet av spelet
    public TMP_Text endGameText; // Text som visar resultatet (vunnit eller förlorat)
    public Transform insideTheatrePosition; // Spelarens position inuti teatern
    public Transform outsideTheatrePosition; // Spelarens position utanför teatern
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
            endGameText.text = "Grattis! Du klarade alla frågor!";
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
            endGameText.text = "Tyvärr, du klarade inte alla frågor. Försök igen!";
        }
        yield return new WaitForSeconds(2f); // Ge spelaren tid att läsa texten  
    }

    public void RestartMinigame()
    {
        StartCoroutine(RestartMinigameSequence());
    }

    IEnumerator RestartMinigameSequence()
    {
        // Visa en svart skärm för övergången
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(1f); // Vänta en sekund (eller hur lång tid du vill att cutscenen ska vara)
        endGamePanel.SetActive(false);
        // Dölj svarta skärmen och starta om minispelet
        blackScreen.SetActive(false);

        // Återställ spelarpositionen om det behövs
        player.transform.position = insideTheatrePosition.position;

        // Återställ nuvarande frågeindex och korrekta svar till 0
        currentQuestionIndex = 0;
        correctAnswers = 0;

        // Aktivera nödvändiga UI-element
        questionText.gameObject.SetActive(true);
        foreach (var button in answerButtons)
        {
            button.gameObject.SetActive(true);
        }
        endGamePanel.SetActive(false);

        // Börja om minispelet
        StartMinigame();
    }

    public void ExitMinigame()
    {
        StartCoroutine(ExitMinigameSequence()); 
    }

    IEnumerator ExitMinigameSequence()
    {
        // Visa en svart skärm för övergången
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(1f); // Vänta en sekund (eller hur lång tid du vill att cutscenen ska vara)
                                             // Dölj minispel-UI
        endGamePanel.SetActive(false);

        // Flytta spelaren till positionen utanför teatern
        player.transform.position = outsideTheatrePosition.position;

        // Göm alla UI-element relaterade till minispelet
        blackScreen.SetActive(false);
        questionText.gameObject.SetActive(false);
        foreach (var button in answerButtons)
        {
            button.gameObject.SetActive(false);
        }
        // Dölj svarta skärmen och starta om minispelet
        blackScreen.SetActive(false);
        // Aktivera spelarrörelsen igen
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