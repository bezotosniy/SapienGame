using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ShopAnimations : MonoBehaviour
{
    [Header("Panel Animation")]
    [SerializeField] private Transform _shopPanel;
    [SerializeField] private Transform[] _typeSelectors;
    [SerializeField] private GameObject _closeButton;
   

    [Header("Purchase Fail")]
    [Space(10f)]
    [SerializeField] private GameObject _buyButton;
    [SerializeField] private GameObject _notEnoughCoinPanel;

    [Header("Purchase Success")]
    [Space(10f)]
    [SerializeField] public Text _price;

   




 




    public void PurchaseFail()
    {
        _buyButton.transform.DOPunchPosition(new Vector3(5, 5, 5), 0.5f);
        _notEnoughCoinPanel.transform.DOScale(new Vector3(1, 1, 1), 0.4f);
    }


    public void DissapearFailPanel()
    {
        _notEnoughCoinPanel.transform.DOScale(new Vector3(0, 0, 0), 0.4f);
    }
  
    public void PurchaseSuccess()
    {
        _price.transform.DOMoveY(715,0.5f);
        _price.DOFade(1, 0.4f);
        StartCoroutine(DissapearSpendingValue());
        
    }


    private IEnumerator DissapearSpendingValue()
    {
        yield return new WaitForSeconds(3);
        _price.DOFade(0, 0.7f);

    }


    public void OpenShopPanel()
    {
        _shopPanel.DOMoveX(280, 0.5f);
        GetComponent<ShopController>().IsBought();
        StartCoroutine(OpenPanel());

    }

    public void CloseShopPanel()
    {
        _shopPanel.DOMoveX(-300, 0.5f);
    }


    private IEnumerator OpenPanel()
    {
        yield return new WaitForSeconds(0.5f);
        _buyButton.transform.DOScale(new Vector3(1, 1, 1), 0.4f);
        _closeButton.transform.DOScale(new Vector3(1, 1, 1), 0.4f);
        for(int i = 0; i < _typeSelectors.Length; i++)
        {
            _typeSelectors[i].DOScale(new Vector3(1,1,1), 0.3f);
        }
       
    }



    public void GoToClothes()
    {
        SceneManager.LoadScene(3);
    }
}
