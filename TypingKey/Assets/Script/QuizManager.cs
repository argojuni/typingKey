using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuizManager : MonoBehaviour
{
    public Text questionText;
    public InputField answerInputField;
    public Text resultText;

    public QuizData quizData;
    private QuestionData[] questions;
    private int currentQuestionIndex;

    private void Start()
    {
        questions = quizData.questions;
        currentQuestionIndex = 0;
        DisplayQuestion();
    }

    public void DisplayQuestion()
    {
        if (currentQuestionIndex < questions.Length)
        {
            QuestionData currentQuestion = questions[currentQuestionIndex];
            string question = currentQuestion.question;
            questionText.text = question;
            answerInputField.text = "";
            resultText.text = "";
        }
        else
        {
            Debug.Log("Quiz completed!");
        }
    }

    public void SubmitAnswer()
    {
        if (currentQuestionIndex < questions.Length)
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
                resultText.text = "Jawaban Benar!";
                currentQuestionIndex++;
                StartCoroutine(NextQuestionWithDelay(1f));
            }
            else
            {
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
