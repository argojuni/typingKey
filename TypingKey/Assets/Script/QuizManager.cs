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

    private void Start()
    {
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
        }
        else
        {
            Debug.Log("Quiz completed!");
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
