using UnityEngine;

[CreateAssetMenu(fileName = "New Quiz", menuName = "Quiz/QuizData")]
public class QuizData : ScriptableObject
{
    public QuestionData[] questions;
}

[System.Serializable]
public class QuestionData
{
    public string question;
    public string[] answers;
}
