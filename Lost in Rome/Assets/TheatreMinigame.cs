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
    public List<Question> questions; 
    public GameObject endGamePanel; 
    public TMP_Text endGameText; 
    public Transform insideTheatrePosition;
    public Transform outsideTheatrePosition; 
    private GameObject player;
    private bool playerNear = false;
    private int currentQuestionIndex = 0;
    private int correctAnswers = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); 
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
        player.GetComponent<PlayerController>().CanMove = false;
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        player.transform.position = insideTheatrePosition.position;
        blackScreen.SetActive(false);
        questionPanel.SetActive(true);
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
        questionPanel.SetActive(false);
        endGamePanel.SetActive(true);
        if (correctAnswers == questions.Count)
        {
            restartButton.gameObject.SetActive(false); 
            exitButton.gameObject.SetActive(false);   
            audienceReaction.PlayOneShot(applauseSound);
            endGameText.text = "Grattis! Du klarade alla frågor!";
            yield return new WaitForSeconds(applauseSound.length);
            blackScreen.SetActive(true);
            endGamePanel.SetActive(false);
            yield return new WaitForSeconds(1.5f);
            player.transform.position = outsideTheatrePosition.position;
            blackScreen.SetActive(false);
            player.GetComponent<PlayerController>().CanMove = true;
        }
        else
        {   
            restartButton.gameObject.SetActive(true);
            exitButton.gameObject.SetActive(true);
            audienceReaction.PlayOneShot(booSound);
            endGameText.text = "Tyvärr, du klarade inte alla frågor. Försök igen!";
        }
        yield return new WaitForSeconds(2f);  
    }

    public void RestartMinigame()
    {
        StartCoroutine(RestartMinigameSequence());
    }

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