using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class InfentlyButtonInformation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
   [SerializeField] private Image _background;
   [SerializeField] private Text _target;
   [SerializeField] private Text _damage;
   [SerializeField] private Text _type;
   [SerializeField] private BattleController _battleController;

   

   private void Start()
   {
     _damage.text = "Damage: " + _battleController.CurrentUron;
     _target.text = "Target:";
     _type.text = "Type:";
   }
    public void OnPointerExit(PointerEventData eventData)
    {
        _background.transform.DOScale(new Vector3(0, 0, 0), 0.4f);
        _background.transform.DOMoveY(100, 0.7f);
       
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _background.transform.DOScale(new Vector3(1, 1, 1), 0.4f);
        _background.transform.DOMoveY(280, 0.5f);
    }
}
