using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class FadePanel : MonoBehaviour
{
    public static FadePanel SceneFadePanel;
    
    //public Image panel;
    public bool allSceneFade = false;
    private CanvasGroup _canvasGroup;
    [HideInInspector] public Slider progressBar;
    private void Awake()
    {
        if (SceneFadePanel == null && allSceneFade)
        {
            SceneFadePanel = this;
        }
        else
        {
            if (allSceneFade)
                Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        if (allSceneFade)
            progressBar = GetComponentInChildren<Slider>();
        _canvasGroup = GetComponent<CanvasGroup>();
        ChangePanelAlpha(0.01f , 1);
        ChangePanelAlpha(2, 0);
    }

    public void ChangePanelAlpha(float duration, float targetAlpha)
    {
        StartCoroutine(CR_ChangePanelAlpha(duration, targetAlpha));
    }
    
    public IEnumerator CR_ChangePanelAlpha(float duration , float targetAlpha)
    {
        float elapsedTime = 0;

        float fadeSpeed = 1 / duration * (targetAlpha < _canvasGroup.alpha ? -1 : 1); 
        while (elapsedTime < duration)
        {
            _canvasGroup.alpha += fadeSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        _canvasGroup.alpha = targetAlpha;
    } 
}
