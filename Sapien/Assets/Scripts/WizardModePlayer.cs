using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardModePlayer : MonoBehaviour
{
    [SerializeField] private Material WizardModeMat;
    [SerializeField] private float targetStrength = 5;
    [SerializeField] private float defaultStrength = 80;
    private string strenghtParam = "Vector1_5484fde6f4f4445282a61561eb4c40f8";
    private Coroutine CR_Changing;
    private void Start()
    {
        WizardModeMat.SetFloat(strenghtParam, defaultStrength);
        GameManager.Instance.OnWizardModeEnabled += WizardModeOn;
        GameManager.Instance.OnWizardModeDisable += WizardModeOff;
    }

    void WizardModeOn()
    {
        if (CR_Changing != null)
            StopCoroutine(CR_Changing);
        CR_Changing = StartCoroutine(ChangeMatStrength(targetStrength));
    }

    private IEnumerator ChangeMatStrength(float strength , float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        float elapsedTime = 0 , duration = 0.5f , speed = (strength - WizardModeMat.GetFloat(strenghtParam)) / duration;
        while (elapsedTime < duration)
        {
            float nextStrength = WizardModeMat.GetFloat(strenghtParam) + (speed * Time.deltaTime);
            WizardModeMat.SetFloat(strenghtParam , nextStrength);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    
    void WizardModeOff()
    {
        if (CR_Changing != null)
            StopCoroutine(CR_Changing);
        CR_Changing = StartCoroutine(ChangeMatStrength(defaultStrength));
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnWizardModeEnabled += WizardModeOn;
        GameManager.Instance.OnWizardModeDisable += WizardModeOff;
    }
}
