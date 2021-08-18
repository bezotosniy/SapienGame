using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestType
{
    QuestForGiveCard = 0 , StoryQuest = 1, Battle = 2 , BattleCrystal = 3 , WishMission = 4, FriendQuest = 5
}
public class Quest : MonoBehaviour
{
    public string questName;
    public string questDescription;
    public Sprite questGiverAvatar;
    public int price;
    [HideInInspector] public bool activated = false;
    [HideInInspector] public bool availible = false;
    public QuestType type;
    public event Action OnQuestComplete;

    public virtual void OpenQuest()
    {
        availible = true;
        Debug.Log($"<b>{questName}</b> <color=blue>Availible</color>");
    }

    public virtual bool Activate()
    {
        if (availible && !activated)
        {
            activated = true;
            Debug.Log($"<b>{questName}</b> <color=green>Activated</color>");
            return true;
        }
        else
        {
            Debug.Log($"<b>{questName}</b> <color=red>don't availible , complete all quests</color>");
            return false;
        }
    }

    public virtual Location GetCurrentQuestLocation()
    {
        return null;
    }

    public virtual void QuestComplete()
    {
        if (activated)
        {
            OnQuestComplete?.Invoke();
            Debug.Log($"<b>{questName}</b> <color=yellow>Complete</color>");
            activated = false;
        }
        else
        {
            Debug.Log($"<b>{questName}</b> <color=red>didn't activated</color>");
        }
    }


    
    
}
