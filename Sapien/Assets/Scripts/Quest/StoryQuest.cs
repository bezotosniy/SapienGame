using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryQuest : Quest
{
    public CardInfo questFromCard;
    public int questOrder;
    public Action<CardInfo> destroyer;
    private void Start()
    {
        destroyer = (CardInfo card) =>
        {
            if (card == questFromCard)
                Destroy(this);
        };
        type = QuestType.StoryQuest;
    }
    public override void OpenQuest()
    {
        availible = true;
        Debug.Log($"Story quest <b>{questName}</b> <color=blue>Availible</color>");
    }

    public override bool Activate()
    {
        if (availible)
        {
            activated = true;
            Debug.Log($"Story quest <b>{questName}</b> <color=green>Activated</color>");
            return true;
            //QuestManager.instance.OnStoryComplete += destroyer;
        }
        else
        {
            Debug.Log($"Story quest <b>{questName}</b> <color=red>don't availible , complete all quests</color>");
            return false;
        }
    }

    public override Location GetCurrentQuestLocation()
    {
        return GameObject.Find("MarketPlace").GetComponent<Location>();
    }

    private void OnDestroy()
    {
        //QuestManager.instance.OnStoryComplete -= destroyer;
    }
}

