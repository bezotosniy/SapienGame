using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DigQuest : QuestAfterStoryQuest
{
    [FormerlySerializedAs("RequireClicks")] public int requireClicks;
    [FormerlySerializedAs("ClickCoolDown")] public float clickCoolDown = 0;
    private int _currentClickCnt = 0;
    private float _lastClickTime = 0;

    public override bool Activate()
    {
        if (base.Activate())
        {
            StartCoroutine(QuestLogic());
            return true;
        }

        return false;
    }

    IEnumerator QuestLogic()
    {
        while (_currentClickCnt < requireClicks)
        {
            if (Input.GetMouseButtonDown(0) && Time.time - _lastClickTime >= clickCoolDown)
                AfterClick();
            yield return null;
        }    
        QuestComplete();
        Debug.Log($"Completed dig mission");
    }

    public void AfterClick()
    {
        _currentClickCnt++;
        Debug.Log($"{_currentClickCnt} clicked , require {requireClicks}");
        _lastClickTime = Time.time;
    }
}
