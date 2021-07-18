using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ShopSystem;
using DG.Tweening;


public class ShopController : MonoBehaviour
{
    [Header("Coins")]
    [Space(10f)]
    public int coins;
    [SerializeField] private Text _coinCounter;
    [SerializeField] private Text _coinCounterInventory;

    [Header("Inheritance")]
    [Space(10f)]
    public ShopSaveScriptable SaveScriptable;
    public ShopSaveScriptable saveHeadClothes;
    public ShopSaveScriptable saveGlases;
    public ShopSaveScriptable SaveBag;
    public Slots slots;
    public ShopAnimations shopAnimations;
    public InventoryAnimation inventoryAnimation;
    
    

    [Header("Items")]
    [Space(10f)]
    [SerializeField] private Text _name;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Text _priceText;
    [SerializeField] private Image _itemSprite;
    public int _currentIndex = 0;
    private int _selectedIndex = 0;

    private int _clothesCurrentIndex;

   
    [Header("Menu")]
    [Space(10f)]
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _previousButton;
    [SerializeField] private int[] _border;
    [SerializeField] private GameObject _buyPanel;
    

   
   

    private void Start()
    {
         //coins = PlayerPrefs.GetInt("coins", coins);
        _selectedIndex = SaveScriptable.selectedIndex;
        _currentIndex = _selectedIndex;

        

        _coinCounter.text = coins.ToString();
        _coinCounterInventory.text = coins.ToString();

        _itemSprite.sprite = SaveScriptable.itemsInfo[_currentIndex].image.sprite;
        _itemSprite.preserveAspect = true;
        SetItemInfo(_currentIndex);
        ControlButtonActivation();



        _nextButton.onClick.AddListener(() => GoToNextItem());
        _previousButton.onClick.AddListener(() => GoToPreviousItem());



      

    }

    private void Update()
    {
        if (SaveScriptable.itemsInfo[_currentIndex].isUnlocked == true)
        {
            _buyButton.interactable = false;
   
        }
        SetClothesItems();
    }

    private void SetItemInfo(int index)
    {
        _name.text = SaveScriptable.itemsInfo[index].name;
        _priceText.text = SaveScriptable.itemsInfo[index].price.ToString();
    }

    public void GoToNextItem()
    {
       
            if (_currentIndex < SaveScriptable.itemsInfo.Length - 1)
            {

               
                _currentIndex += 1;
                IsBought();
               _itemSprite.sprite = SaveScriptable.itemsInfo[_currentIndex].image.sprite;
               _itemSprite.preserveAspect = true;
                SetItemInfo(_currentIndex);
                ControlButtonActivation();
            }
    }

    public void GoToPreviousItem()
    {
        
        if (_currentIndex > 0)
        {
       
            
            _currentIndex -= 1;
            IsBought();
            _itemSprite.sprite = SaveScriptable.itemsInfo[_currentIndex].image.sprite;
            _itemSprite.preserveAspect = true;
            SetItemInfo(_currentIndex);
            ControlButtonActivation();
        }
    }

    public void OnClickBuyButton()
    {
        
        if (coins >= SaveScriptable.itemsInfo[_currentIndex].price)
        {
            _buyPanel.transform.DOScale(new Vector3(1, 1, 1), 0.4f);
            
        }
        else
        {
            shopAnimations.PurchaseFail();
        }
    }

    private void GetPrice()
    {
        shopAnimations._price.text = "-" + SaveScriptable.itemsInfo[_currentIndex].price;
        coins = coins - SaveScriptable.itemsInfo[_currentIndex].price;
        _coinCounter.text = coins.ToString();
        _coinCounterInventory.text = coins.ToString();
        PlayerPrefs.SetInt("coins", coins);
    }

    private void ChoseType()
    {
       
        //if (_items[_currentIndex].CompareTag("Furniture"))
        //{
            for (int i = 0; i < slots.slotsFurnitureInventory.Length; i++)
            {
                if (slots.isUsedFurnitureInventory[i] == false)
                {
                    slots.isUsedFurnitureInventory[i] = true;
                    slots.slotsFurnitureImages[i].sprite = SaveScriptable.itemsInfo[_currentIndex].image.sprite;
                    slots.slotsFurnitureImages[i].enabled = true;
                    slots.slotsFurnitureImages[i].transform.DOScale(new Vector3(1, 1, 1), 0.4f);
                    slots.furniture[i] = SaveScriptable.itemsInfo[_currentIndex].prefab;
                   
                    break;
                }

            }
        //}


       /* else if (_items[_currentIndex].CompareTag("Clothes"))
        {
            for (int i = 0; i < slots._slotsClothesInventory.Length; i++)
            {
                if (slots.isUsedClothesInventory[i] == false)
                {
                    slots.isUsedClothesInventory[i] = true;
                    Instantiate(_items[_currentIndex], slots._slotsClothesInventory[i].transform);
                    break;
                }

            }
        }*/
    }

  
    public void IsBought()
    {
        if(SaveScriptable.itemsInfo[_currentIndex].isUnlocked == true)
        {
            _buyButton.interactable = false;
           SaveScriptable.itemsInfo[_currentIndex].image.DOFade(0.5f, 0.01f);
        }
        else 
        {
            _buyButton.interactable = true;
            
        }
    }

    public void GoToNextType(int index)
    {
        _itemSprite.sprite = SaveScriptable.itemsInfo[index].image.sprite;
        _itemSprite.preserveAspect = true;
        SetAnotherType(_currentIndex);
        IsBought();
       
    }

    public void SetAnotherType(int index)
    {
        SetItemInfo(index);
        _currentIndex = index;
        ControlButtonActivation();
    }

    public void ControlButtonActivation()
    {
        if (_currentIndex == _border[0])
        {
            _nextButton.interactable = false;

        }
        else
        {
            _nextButton.interactable = true;
        }
      
        
      if(_currentIndex == _border[1])
        {
            _previousButton.interactable = false;
        }
       else
        {
            _previousButton.interactable = true;
        }

        if (_currentIndex == 0)
        {
            _previousButton.interactable = false;
        }
       
     

        if (_currentIndex == SaveScriptable.itemsInfo.Length - 1)
        {
            _nextButton.interactable = false;
        }
     
       
    }
   
     
    public void BuyItem()
    {
        inventoryAnimation.OpenInventory();
        GetPrice();
        SaveScriptable.itemsInfo[_currentIndex].isUnlocked = true;
        shopAnimations.PurchaseSuccess();
        SaveScriptable.itemsInfo[_currentIndex].image.transform.DOScale(new Vector3(0.2f, 0.2f, 0.2f), 0.4f);
        ChoseType();
       _buyPanel.transform.DOScale(new Vector3(0, 0, 0), 0.4f);
    }


    public void DontBuyItem()
    {
        _buyPanel.transform.DOScale(new Vector3(0, 0, 0), 0.4f);
    }


    private void SetClothesItems()
    {
        for (int i = 0; i < saveHeadClothes.itemsInfo.Length; i++)
        {
            if (slots.isUsedClothesInventory[i] == false  && saveHeadClothes.itemsInfo[i].isUnlocked == true)
            {

                slots.isUsedClothesInventory[i] = true;
                slots.slotsClothesImages[i].sprite = saveHeadClothes.itemsInfo[i].image.sprite;
                slots.slotsClothesImages[i].enabled = true;
                slots._prefabs[i] = saveHeadClothes.itemsInfo[i].prefab;

                break;

            }
            else if(slots.isUsedClothesInventory[i] == false && saveGlases.itemsInfo[i].isUnlocked == true)
            {
                slots.isUsedClothesInventory[i] = true;
                slots.slotsClothesImages[i].sprite = saveGlases.itemsInfo[i].image.sprite;
                slots.slotsClothesImages[i].enabled = true;
                slots.slotsClothesImages[i].preserveAspect = true;
                slots._prefabs[i] = saveGlases.itemsInfo[i].prefab;

                break;

            }
            else if (slots.isUsedClothesInventory[i] == false && SaveBag.itemsInfo[i].isUnlocked == true)
            {
                slots.isUsedClothesInventory[i] = true;
                slots.slotsClothesImages[i].sprite = SaveBag.itemsInfo[i].image.sprite;
                slots.slotsClothesImages[i].enabled = true;
                slots.slotsClothesImages[i].preserveAspect = true;
                slots._prefabs[i] = SaveBag.itemsInfo[i].prefab;

                break;

            }


        }

        /*for (int i = 0; i < slots._slotsClothesInventory.Length; i++)
        {
            if (slots.isUsedClothesInventory[i] == false && saveGlases.itemsInfo[i].isUnlocked == true && i < 30)
            {

                slots.isUsedClothesInventory[i] = true;
                slots.slotsClothesImages[i].sprite = saveGlases.itemsInfo[i].image.sprite;
                slots.slotsClothesImages[i].enabled = true;
                slots.slotsClothesImages[i].preserveAspect = true;
                slots.prefabOfClothes.Add(saveGlases.itemsInfo[i].prefab);

                break;

            }

        }*/

    }

}



















/* else if (_items[_currentIndex].CompareTag("Plot"))
           {
               for (int i = 0; i < slots._slotsClothesInventory.Length; i++)
               {
                   if (slots.isUsedPlotInventory[i] == false)
                   {
                       slots.isUsedPlotInventory[i] = true;
                       Instantiate(_items[_currentIndex], slots._slotsPlotInventory[i].transform);
                       break;
                   }

               }
           }
           else if (_items[index].CompareTag("OtherObjectsInventory"))
           {
               for (int i = 0; i < slots._slotsOtherInventory.Length; i++)
               {
                   if (slots.isUsedOtherInventory[i] == false)
                   {
                       slots.isUsedOtherInventory[i] = true;
                       Instantiate(_items[index], slots._slotsOtherInventory[i].transform);
                       break;
                   }

               }
           }*/