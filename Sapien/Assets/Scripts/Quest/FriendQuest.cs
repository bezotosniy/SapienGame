using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendQuest : Quest
{
    public int energy;
    public float relationPoints;
    private void Start()
    {
        QuestManager.instance.OnStoryComplete += TryOpen;
        TryOpen(QuestManager.instance.card);
    }

    private void Update()
    {
        if (activated)
        {
            if (Input.GetKeyDown(KeyCode.O))
                QuestComplete();
        }
    }

    public void TryOpen(CardInfo card)
    {
        bool completed;
        QuestManager.instance.completedQuest.TryGetValue(questName, out completed);
        if (!completed && QuestManager.instance.storyQuestStage == StoryQuestStage.Complete)
        {
            availible = true;
            Debug.Log($"<b>{questName}</b> <color=blue>Availible</color>");
        }
        else
        {
            Debug.Log($"<b>{questName}</b> <color=red>isn't availible , complete story mission</color>");
        }
    }
    
    public override void OpenQuest()
    {
        TryOpen(FragmentCard.instance.cardInfo);
    }
    public override void Activate()
    {
        base.Activate();
    }

    public override void QuestComplete()
    {
        if (activated)
        {
            QuestManager.instance.CompleteQuest(questName, false);
            Debug.Log($"<b>{questName}</b> <color=yellow>Complete</color>");
        }
        else
        {
            Debug.Log($"<b>{questName}</b> <color=red>didn't activated</color>");
        }
        base.QuestComplete();
    }

}
