using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Location : MonoBehaviour
{
    public string locationName, description;
    public Image locationImage;
    public Color highlitedColor;
    public string sceneLocationName;
    
    public bool locked = false;
    public LocationLock _lock;

    public static MapInPhone mapManager;
    [HideInInspector]
    public Vector2 position;

    private void Start()
    {
        if (mapManager == null)
            mapManager = GameObject.FindObjectOfType<MapInPhone>();
    }

    public Vector2 GetPosition()
    {
        position = GetComponent<RectTransform>().anchoredPosition;
        return position;
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

    public void GoToLocationScene()
    {
        //StartCoroutine(ExtensionMethods.LoadSceneWithTransition(2 , sceneLocationName));
        if (mapManager.CanMoveBeetwenLocations() && !locked)
        {
            GameManager.Instance.StartCoroutine(ExtensionMethods.LoadSceneWithTransition(2, sceneLocationName));
            mapManager.CloseMap();
        }
        else
        {
            Debug.Log($"<color=red> Can't move to location </color> {locationName}");
        }
    }

    public void UnlockLocation()
    {
        if (locked)
        {
            _lock.Unlock();
            locked = false;
        }
    }
}
