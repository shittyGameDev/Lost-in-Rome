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
        endGamePanel.SetActive(true);
        if (correctAnswers == questions.Count)
        {
            audienceReaction.PlayOneShot(applauseSound);
            endGameText.text = "Grattis! Du klarade alla frågor!";
            yield return new WaitForSeconds(applauseSound.length);
            blackScreen.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            player.transform.position = outsideTheatrePosition.position;
        }
        else
        {
            audienceReaction.PlayOneShot(booSound);
            endGameText.text = "Tyvärr, du klarade inte alla frågor. Försök igen!";
        }
        yield return new WaitForSeconds(2f); // Ge spelaren tid att läsa texten
        endGamePanel.SetActive(false);
        blackScreen.SetActive(false);
        player.GetComponent<PlayerController>().CanMove = true; // Återaktivera spelarrörelse
        // Reset minigame or provide options to replay or exit
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