using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ShopSystem;
using UnityEngine.UI;
using DG.Tweening;

public class InventoryItem : MonoBehaviour, IPointerDownHandler
{
    public int index;
    public Slots slots;
    public ShopController shopController;
    public ShopSaveScriptable shopSave;
    [SerializeField] private GameObject _empty;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _refuseButton;
   


    private void Start()
    {
        _confirmButton.onClick.AddListener(() => Confirmed());
        _refuseButton.onClick.AddListener(() => Refused());
    }

   

    public void OnPointerDown(PointerEventData eventData)
    {
       if(eventData.button == PointerEventData.InputButton.Right)
        {
          
            if (slots.isUsedFurnitureInventory[index] == true)
            {

                _panel.SetActive(true);  
               
            }
        }
    }

    
    

    private void InstantiateObject()
    {
            if (slots.furniture[index].CompareTag("Carpet"))
            {
          

                if (slots.type[0] == null)
                {
                    slots.type[0] = Instantiate(slots.furniture[index], new Vector3(1.04f, 0.01f, -3.56f), Quaternion.identity);
                    
                }
                else if(slots.type[0] != null)
                {
                    Destroy(slots.type[0]);
                    slots.type[0] = Instantiate(slots.furniture[index], new Vector3(1.04f, 0.01f, -3.56f), Quaternion.identity);
                   

                }
            }
            else if (slots.furniture[index].CompareTag("Paint"))
            {
                
                if (slots.type[1] == null)
                {
                    slots.type[1] = Instantiate(slots.furniture[index], new Vector3(2.211f, 3, -0.498f), Quaternion.Euler(0f, 188.27f, 0));

                }
                else if(slots.type[1] != null)
                {
                    Destroy(slots.type[1]);
                    slots.type[1] = Instantiate(slots.furniture[index], new Vector3(2.211f, 3, -0.498f), Quaternion.Euler(0f, 188.27f, 0));
                }
            }
    }


    public void Confirmed()
    {
        InstantiateObject();
        _panel.SetActive(false);
    }


    public void Refused()
    {
        _panel.SetActive(false);
    }


   
       
        
            
      
}
