using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TraslationTaskResult
{
    WaitingForAnswer = 0 , RightAnswer = 1 , WrongAnswer = 2
}
public class TranslationTaskUI : MonoBehaviour
{
    
    [HideInInspector]public TranslateQuestion currentTask;
    public Text wordToTranslate;
    public Text[] variants;

    public event Action<bool> OnAnswerGive;

    public void UpdateTask(TranslateQuestion task)
    {
        for (int i = 0; i < Mathf.Min(task.answers.Length , variants.Length); ++i)
            variants[i].text = task.answers[i];
        wordToTranslate.text = task.question;
        currentTask = task;
    }

    public void TrySolve(int variantIDX)
    {
        if (currentTask != null)
        {
            if (variantIDX == currentTask.rightAnswer)
            {
                OnCorrectAnswer();
            }
            else
            {
                OnWrongAnswer();
            }
        }
    }

    private void OnWrongAnswer()
    {
        currentTask = null;
        OnAnswerGive?.Invoke(false);
        gameObject.SetActive(false);
    }

    private void OnCorrectAnswer()
    {
        currentTask = null;
        OnAnswerGive?.Invoke(true);
        gameObject.SetActive(false);
    }
}
