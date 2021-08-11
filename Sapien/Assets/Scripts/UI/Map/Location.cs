using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class Location : MonoBehaviour
{
    public string locationName, description;
    public Image locationImage;
    public Color highlitedColor;
    public bool locked = false;
    public string sceneLocationName;
    
    [HideInInspector]
    public Vector2 position;

    private void Start()
    {
        position = GetComponent<RectTransform>().anchoredPosition;
    }

    private Coroutine CR_Highlight;
    public void Highlight()
    {
        if (!locked)
        {
            if (CR_Highlight != null)
                StopCoroutine(CR_Highlight);
            CR_Highlight = StartCoroutine(changeImageColor(highlitedColor));
            FindObjectOfType<LocationInfo>().ShowLocationInfo(this);
        }
    }

    public void BackToNormalColor()
    {
        if (!locked)
        {
            if (CR_Highlight != null)
                StopCoroutine(CR_Highlight);
            CR_Highlight = StartCoroutine(changeImageColor(Color.white));
            FindObjectOfType<LocationInfo>().HideLocationInfo();
        }
    }
    
    IEnumerator changeImageColor(Color targetColor)
    {
        float elapsedTime = 0;
        while (elapsedTime < 1)
        {
            locationImage.color = Color.Lerp(locationImage.color, targetColor, 8 * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        locationImage.color = targetColor;
    }
}
