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

    public GameObject zombie_male;
    private Animator anim;

    private void Start()
    {
        anim = zombie_male.GetComponent<Animator>();

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
            anim.SetBool("idle", true);
            anim.SetBool("dead", false);
            anim.SetBool("hurt", false);
            anim.SetBool("attack", false);
        }
        else
        {
            Debug.Log("Quiz completed!");
            anim.SetBool("dead", true);
            anim.SetBool("idle", false);
            anim.SetBool("hurt", false);
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
                anim.SetBool("hurt", true);
                anim.SetBool("idle", false);
                anim.SetBool("attack", false);

                resultText.text = "Jawaban Benar!";
                currentQuestionIndex++;
                StartCoroutine(NextQuestionWithDelay(1f));
            }
            else
            {
                anim.SetBool("attack", true);
                anim.SetBool("idle", false);
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
