using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MouseAnimation : MonoBehaviour
{
    [Header("Click")]
    [SerializeField] private Image _backgroundClick;
    [SerializeField] private Image _mouseKey;
    [SerializeField] private Image _chooseButton;


    private void Start()
    {
        StartCoroutine(ChangeScaleMouse());
    }

    private IEnumerator ChangeScaleMouse()
    {
       _chooseButton.transform.DOMoveX(610, 0.001f);
       yield return new WaitForSeconds(0.6f);
       _backgroundClick.DOFade(1f, 0.5f);
       _mouseKey.transform.DOScale(new Vector3(1.1f,1.1f,1.1f), 0.5f);
       _chooseButton.transform.DOMoveY(255, 0.5f);
       _chooseButton.transform.DOMoveX(610, 0.01f);
       StartCoroutine(ChangeScaleMouseSecond());
    }


    private IEnumerator ChangeScaleMouseSecond()
    {
        yield return new WaitForSeconds(0.6f);
       _backgroundClick.DOFade(0.1f, 0.5f);
       _mouseKey.transform.DOScale(new Vector3(1f,1f,1f), 0.5f);
       _chooseButton.transform.DOMoveY(250, 0.5f);
       StartCoroutine(ChangeScaleMouse());
    }
  

}
