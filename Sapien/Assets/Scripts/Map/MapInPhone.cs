using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapInPhone : MonoBehaviour
{
    public float Increment;
    public GameObject[] levelsOfScaleIcon;
    public PhoneManager phone;
    public Text mapLabel;
    
    [HideInInspector] public GameObject lastClicked = null;
    [HideInInspector] public int currentLevelOfScale;
    [SerializeField] private Color highlitedColor;

    private string mapOpener;
    private void Awake()
    {
        currentLevelOfScale = 2;
        ScaleMapUp();
        ScaleMapDown();
    }

    public void OpenMap(string opener)
    {
        mapOpener = opener;
        if (opener == "Taxi")
            mapLabel.text = "Taxi road map";
        else
            mapLabel.text = "Map";
        phone.anim.Play("MapOpen");
    }

    public void CloseMap()
    {
        phone.anim.Play("MapClose");
    }

    public void IncreaseSize(GameObject target)
    {
        target.GetComponent<RectTransform>().localScale = new Vector3(Increment, Increment, 1f);
    }
    
    public void DecreaseSize(GameObject target)
    {
        target.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1f);
    }

    public void OnPointerClick(GameObject target)
    {
        return;
        if (lastClicked != target)
        {
            target.GetComponent<Image>().color = Color.green;
            
            if (lastClicked != null)
            {
                lastClicked.GetComponent<Image>().color = Color.white;
                DecreaseSize(lastClicked);
            }
            
            lastClicked = target;
            IncreaseSize(target);
        }
    }

    public void ScaleMapUp()
    {
        if (currentLevelOfScale < 3)
        {
            currentLevelOfScale++;
            levelsOfScaleIcon[currentLevelOfScale - 1].GetComponent<Image>().color = highlitedColor;
            
            if (levelsOfScaleIcon[currentLevelOfScale - 2] != null)
            {
                levelsOfScaleIcon[currentLevelOfScale - 2].GetComponent<Image>().color = Color.white;
                DecreaseSize(levelsOfScaleIcon[currentLevelOfScale - 2]);
            }
            
            IncreaseSize(levelsOfScaleIcon[currentLevelOfScale - 1]);
        }
    }

    public void ScaleMapDown()
    {
        if (currentLevelOfScale > 1)
        {
            currentLevelOfScale--;
            levelsOfScaleIcon[currentLevelOfScale - 1].GetComponent<Image>().color = highlitedColor;
            
            if (levelsOfScaleIcon[currentLevelOfScale] != null)
            {
                levelsOfScaleIcon[currentLevelOfScale].GetComponent<Image>().color = Color.white;
                DecreaseSize(levelsOfScaleIcon[currentLevelOfScale]);
            }
            
            IncreaseSize(levelsOfScaleIcon[currentLevelOfScale - 1]);
        }
    }

    public bool CanMoveBeetwenLocations()
    {
        if (mapOpener == "Taxi")
            return true;
        return false;
    }
}
