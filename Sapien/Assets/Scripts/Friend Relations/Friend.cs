using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : MonoBehaviour
{
    public string friendName;
    public Sprite friendAvatar;
    public FriendRelations friendRelations;

    public FriendQuest quest;

    public void GiveQuest()
    {
        quest.questGiverAvatar = friendAvatar;
        if (GameObject.Find("PhoneButton").GetComponent<QuestPanelManager>().AddQuestToActiveList(quest.questName))
        {
            quest.OnQuestComplete += () => friendRelations.GetFriendRelationPoints(quest.relationPoints , true);
        }
    }
    
}
