using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class LocationInfo : MonoBehaviour
{
    public Text locationName , locationDescrip;
    public RectTransform rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    private Coroutine CR_ShowLoc;
    public void ShowLocationInfo(Location loc)
    {
        if (CR_ShowLoc != null)
            StopCoroutine(CR_ShowLoc);
        CR_ShowLoc = StartCoroutine(ShowLocation(loc));
    }

    public void HideLocationInfo()
    {
        if (CR_ShowLoc != null)
            StopCoroutine(CR_ShowLoc);
        CR_ShowLoc = StartCoroutine(HideLoc());
    }

    IEnumerator ShowLocation(Location loc)
    {
        rect.anchoredPosition = loc.GetPosition();
        locationName.text = loc.locationName;
        locationDescrip.text = loc.description;
        CanvasGroup group = GetComponent<CanvasGroup>();
        group.alpha = 0;
        float elapsedTime = 0;
        while (elapsedTime < 2)
        {
            group.alpha = Mathf.Lerp(group.alpha, 1, 4 * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        group.alpha = 1;
        
    }

    IEnumerator HideLoc()
    {
        CanvasGroup group = GetComponent<CanvasGroup>();
        float elapsedTime = 0;
        while (elapsedTime < 2)
        {
            group.alpha = Mathf.Lerp(group.alpha, 0, 4 * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        group.alpha = 0;
    }
}
