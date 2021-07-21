using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using ShopSystem;


public class InventoryAnimation : MonoBehaviour
{
    [Header("InventoryPanel")]
    [Range(0, 7f)]
    [SerializeField] private float _duration;
    [SerializeField] private float _finalPositionOfPanel;
    [SerializeField] private  Transform _openerbutton;
    
   
    
    
    [Header("ShopButtons")]
    [Space(10f)]
    [SerializeField] private Transform _opener;
    [SerializeField] private float _finalPositionOfButton;
    [SerializeField] private Transform _closer;


    [Header("Animation Inside Panel")]
    [Space(10f)]
    [SerializeField] private Transform _firstType;
    [SerializeField] private Transform _secondType;
    [SerializeField] private Transform _thirdType;
    [SerializeField] private Transform _fourthType;
    [SerializeField] private Transform _fifthType;
    [SerializeField] private float _finalPositionOfTypes;
    private int _currentIndex;
    
    [Header("Inheritance")]
    [Space(10f)]
    public ShopSaveScriptable SaveHeadClothes;
    public Slots slots;
    public ShopController shopController;


    private void Start()
    {
        _currentIndex = shopController._currentIndex;
    }
    public void OpenInventory()
    {
        _opener.DOMoveY(_finalPositionOfButton, 0.4f);
        transform.DOMoveX(_finalPositionOfPanel, _duration);
        StartCoroutine(AnimationWhenPanelOpen());
     
        
    }

    public void CloseInventory()
    {
        _closer.DOScale(new Vector3(1, 1, 1), 0.2f);
        _firstType.DOMoveY(620, 0.5f);
        _secondType.DOMoveY(620, 0.7f);
        _thirdType.DOMoveY(620, 0.8f);
        _fourthType.DOMoveY(620, 0.9f);
        _fifthType.DOMoveY(620, 0.95f);
        _closer.DOScale(new Vector3(0, 0, 0), 0.2f);
        StartCoroutine(AnimationWhenPanelClose());
    }


    private IEnumerator AnimationWhenPanelOpen()
    {
        yield return new WaitForSeconds(1);
        _closer.DOScale(new Vector3(1, 1, 1), 0.2f);
        _firstType.DOMoveY(_finalPositionOfTypes, 0.5f);
        _secondType.DOMoveY(_finalPositionOfTypes, 0.7f);
        _thirdType.DOMoveY(_finalPositionOfTypes, 0.8f);
        _fourthType.DOMoveY(_finalPositionOfTypes, 0.9f);
        _fifthType.DOMoveY(_finalPositionOfTypes, 0.95f);
    }


    private IEnumerator AnimationWhenPanelClose()
    {
        yield return new WaitForSeconds(1);
        _opener.DOMoveY(70, 0.4f);
        transform.DOMoveX(2200, _duration);
    }


  
}
