using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Icons : MonoBehaviour
{
    public GameObject iconPrefab;
    public QuestPanelManager _questPanelManager;

    public Sprite[] iconSprites;  
    private List <GameObject> activeIcons = new List<GameObject>();
    private Location[] locations;
    private int activeQuests = 0;
    private void OnEnable()
    {
        //locations = GameObject.FindObjectsOfType<Location>();
        UpdateIcons();
    }

    private void UpdateIcons()
    {
        activeQuests = _questPanelManager.QuestIcon.Count(panel => panel.activeSelf);
        for (int i = 0; i < _questPanelManager.QuestIcon.Length; ++i)
        {
            GameObject questIcon = _questPanelManager.QuestIcon[i];
            if (questIcon.activeSelf)
            {
                Quest quest = questIcon.GetComponent<QuestPanel>().targetQuest;
                SpawnIcon(quest.type , quest.GetCurrentQuestLocation() , quest.questName , i + 1);
                quest.OnQuestComplete += () => DeleteIcon(quest.questName);
            }
        }
    }
    
    private void SpawnIcon(QuestType iconType , Location location , string questName , int iconNum)
    {
        GameObject icon = Instantiate(iconPrefab, this.transform);
        activeIcons.Add(icon);
        
        Vector2 iconSize = icon.GetComponent<RectTransform>().sizeDelta;
        float emptySpace = (3 - activeQuests) * iconSize.x;
        float offsetX = (-iconSize.x * 1.5f) + (emptySpace / 2) + (iconSize.x / 2) + (iconNum - 1) * (iconSize.x),
            offsetY = -(iconSize.y / 2);
        icon.GetComponent<RectTransform>().anchoredPosition = location.GetPosition() + new Vector2(offsetX , offsetY);
        icon.GetComponent<Image>().sprite = iconSprites[(int)iconType];
        icon.GetComponent<IconIdle>().StartIdle(iconType);
        icon.name = questName;
    }

    private void DeleteIcon(string QuestName)
    {
        GameObject toDelete = activeIcons.Find((icon => icon.name == QuestName));
        activeIcons.Remove(toDelete);
        Destroy(toDelete);

    }

    private void DestroyIcons()
    {
        foreach (GameObject icon in activeIcons)
        {
            Destroy(icon);
        }
        activeIcons.Clear();
    }
    
    private void OnDisable()
    {
        DestroyIcons();
    }
}
