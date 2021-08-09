using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DoneAndMissed : MonoBehaviour
{

     [Header("Good")]
    [SerializeField] private Transform _good;
    [SerializeField] private Image _circeGood;
    [SerializeField] private int _counterAnimationGood;

    [Header("Missed")]
    [SerializeField] private Transform _missed;
    [SerializeField] private Image _cross;


    public IEnumerator ChangeScale()
    {
        yield return new WaitForSeconds(0.5f);
        _circeGood.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.4f);
        //_good.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.4f);
        StartCoroutine(ChangeScaleCircle());
    }
    private IEnumerator ChangeScaleCircle()
    {
        yield return new WaitForSeconds(0.5f);
        _counterAnimationGood++;
        _circeGood.transform.DOScale(new Vector3(1, 1, 1), 0.4f);
        //_good.transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f);
        StartCoroutine(ChangeScale());
       
        if(_counterAnimationGood == 3)
        {
            _counterAnimationGood = 0;
            yield break;
        }
    }

     public  IEnumerator ChangeScaleMissed()
    {
        yield return new WaitForSeconds(0.5f);
        _cross.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.4f);
        //_missed.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.4f);
        StartCoroutine(ChangeScaleMissedSecond());
    }
    private IEnumerator ChangeScaleMissedSecond()
    {
        yield return new WaitForSeconds(0.5f);
        _counterAnimationGood++;
        _cross.transform.DOScale(new Vector3(1, 1, 1), 0.4f);
        //_missed.transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f);
        StartCoroutine(ChangeScaleMissed());
        if(_counterAnimationGood == 3)
        {
            _counterAnimationGood = 0;
            yield break;
        }
      
    }


    public void ScaleGood(int scale, float  position)
    {
        _good.DOScale(new Vector3(scale, scale, scale), 0.4f);
        _good.DOMoveY(position, 0.7f);
    }

    public void ScaleMissed(int scale, float position)
    {
        _missed.DOScale(new Vector3(scale, scale, scale), 0.4f);
        _missed.DOMoveY(position, 0.7f);
    }
}
