using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class QuizManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public TMP_InputField answerInputField;
    public TextMeshProUGUI resultText;

    public QuizData quizData;
    private List<QuestionData> questions;
    private int currentQuestionIndex;

    public GameObject[] zombie_male;
    private Animator[] anim;
    private void Start()
    {
        anim = new Animator[zombie_male.Length];
        for(int i=0; i<anim.Length; i++)
        {
            anim[i] = zombie_male[i].GetComponent<Animator>();
        }

        questions = new List<QuestionData>(quizData.questions);
        ShuffleQuestions();
        currentQuestionIndex = 0;
        DisplayQuestion();
    }

    private void ShuffleQuestions()
    {
        for (int i = 0; i < questions.Count - 1; i++)
        {
            int randomIndex = Random.Range(i, questions.Count);
            QuestionData temp = questions[i];
            questions[i] = questions[randomIndex];
            questions[randomIndex] = temp;
        }
    }

    public void DisplayQuestion()
    {
        if (currentQuestionIndex < questions.Count)
        {
            QuestionData currentQuestion = questions[currentQuestionIndex];
            string question = currentQuestion.question;
            questionText.text = question;
            answerInputField.text = "";
            resultText.text = "";

            for (int i =0; i<anim.Length; i++)
            {
                if(anim[i] != null)
                {
                    anim[i].SetBool("idle", true);
                    anim[i].SetBool("dead", false);
                    anim[i].SetBool("hurt", false);
                    anim[i].SetBool("attack", false);
                }
            }
        }
        else
        {
            AudioManager.Instance.PlaySFX("win");
            Debug.Log("Quiz completed!");
            for (int i = 0; i < anim.Length; i++)
            {
                if (anim[i] != null)
                {
                    anim[i].SetBool("dead", true);
                    anim[i].SetBool("idle", false);
                    anim[i].SetBool("hurt", false);
                }
            }
        }
    }

    public void SubmitAnswer()
    {
        if (currentQuestionIndex < questions.Count)
        {
            QuestionData currentQuestion = questions[currentQuestionIndex];
            string userAnswer = answerInputField.text.Trim().ToLower();
            string[] answers = currentQuestion.answers;

            bool isAnswerCorrect = false;
            foreach (string answer in answers)
            {
                if (userAnswer == answer.ToLower().Trim())
                {
                    isAnswerCorrect = true;
                    break;
                }
            }

            if (isAnswerCorrect)
            {
                for (int i = 0; i < anim.Length; i++)
                {
                    if (anim[i] != null)
                    {
                        anim[i].SetBool("hurt", true);
                        anim[i].SetBool("idle", false);
                        anim[i].SetBool("attack", false);
                    }
                }
                AudioManager.Instance.PlaySFX("correct");

                resultText.text = "Jawaban Benar!";
                currentQuestionIndex++;
                StartCoroutine(NextQuestionWithDelay(1f));
            }
            else
            {
                for (int i = 0; i < anim.Length; i++)
                {
                    if (anim[i] != null)
                    {
                        anim[i].SetBool("attack", true);
                        anim[i].SetBool("idle", false);
                    }
                }
                resultText.text = "Jawaban Salah!";
            }
        }
    }

    private IEnumerator NextQuestionWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        DisplayQuestion();
    }
}
