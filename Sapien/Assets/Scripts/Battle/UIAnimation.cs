using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  DG.Tweening;

public class UIAnimation : MonoBehaviour
{
   [SerializeField] private GameObject _background;
   [SerializeField] private Transform _backgroundUp;
   [SerializeField] private Transform _backgroundDown;


   public void OpenPanel()
   {
       var SeqUp = DOTween.Sequence();   
       var SeqDown = DOTween.Sequence(); 


       SeqUp.Append(_backgroundUp.DOMoveY(650, 0.8f));
       SeqUp.AppendInterval(0.1f);
       SeqUp.Append(_backgroundUp.DOMoveY(860, 0.8f));


       SeqDown.Append(_backgroundDown.DOMoveY(490, 0.8f));
       SeqDown.AppendInterval(0.1f);
       SeqDown.Append(_backgroundDown.DOMoveY(250, 0.8f));
       SeqDown.Join(_background.transform.DOScale(new Vector3(1, 1, 1), 0.6f));
    }

    public void ClosePanel()
    {
       var SeqUp = DOTween.Sequence();   
       var SeqDown = DOTween.Sequence(); 

       SeqUp.Append(_backgroundUp.DOMoveY(650, 0.8f));
       SeqUp.Join(_background.transform.DOScale(new Vector3(0, 0, 0), 0.6f));
       SeqUp.AppendInterval(0.1f);
       SeqUp.Append(_backgroundUp.DOMoveY(1200, 0.8f));

       SeqDown.Append(_backgroundDown.DOMoveY(490, 0.8f));
       SeqDown.AppendInterval(0.1f);
       SeqDown.Append(_backgroundDown.DOMoveY(-100, 0.8f));

    }




   
}
