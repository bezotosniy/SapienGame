using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card" , menuName = "Card")]
public class CardInfo : ScriptableObject
{
    public int cardID;
    public string cardName;
    public string cardDescription;
    public Sprite cardSprite;
    public int cardEnergy = 150;
    public string cardReview;

    public string cardSeason;

    public int storyQuestCount;

    [HideInInspector]public bool passed = false;
    //[HideInInspector]
    public float quality, money, time;
}
