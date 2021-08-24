using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class TranslateQuestion
{
    public enum TranslateDiretion
    {
        EngToRus = 0, RusToEng = 1
    }

    public TranslateDiretion type;
    public string question;
    public string[] answers;
    public int rightAnswer;
}

[CreateAssetMenu(fileName = "NewTranslateQuestion", menuName = "Question/TranslationQuestions")]
public class TranslateQuestions : ScriptableObject
{
    public TranslateQuestion[] Questions;

    private static TranslateQuestions GetAllQuestions()
    {
        return Resources.LoadAll<TranslateQuestions>("TranslateQuestions").ToList()[0];
    }
    
    public static TranslateQuestion GetRandomQuestion()
    {
        TranslateQuestions loadedQuestions = GetAllQuestions();
        Debug.Log(loadedQuestions.Questions.Length);
        return loadedQuestions.Questions[Random.Range(0, loadedQuestions.Questions.Length)];
    }
}
