using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interection : MonoBehaviour
{
   
   
    public Slots slots;
    [SerializeField] private GameObject _furniturePanel;
    [SerializeField] private GameObject _clothesPanel;
   
  

    private void Start()
    {
        for(int i = 0; i < _furniturePanel.transform.childCount; i++)
        {
            _furniturePanel.transform.GetChild(i).GetComponent<InventoryItem>().index = i;
        }
        for(int i = 0; i < _clothesPanel.transform.childCount; i++)
        {
            _clothesPanel.transform.GetChild(i).GetComponent<InventoryItemClothes>().index = i;
        }
    }




    /*public void TakeItem()
    {
        _ranfomObject = Random.Range(0, imageofObject.Length);

        if (imageofObject[_ranfomObject].CompareTag("Furniture"))
        {
            for (int i = 0; i < slots._slotsFurnitureInventory.Length; i++)
            {
                if (slots.isUsedFurnitureInventory[i] == false)
                {
                    slots.isUsedFurnitureInventory[i] = true;
                    Instantiate(imageofObject[_ranfomObject], slots._slotsFurnitureInventory[i].transform);
                    break;
                }
              
            }
        }


        else if(imageofObject[_ranfomObject].CompareTag("Clothes"))
        {
            for (int i = 0; i < slots._slotsClothesInventory.Length; i++)
            {
                if (slots.isUsedClothesInventory[i] == false)
                {
                    slots.isUsedClothesInventory[i] = true;
                    Instantiate(imageofObject[_ranfomObject], slots._slotsClothesInventory[i].transform);
                    break;
                }
               
            }
        }
        else if(imageofObject[_ranfomObject].CompareTag("Plot"))
        {
            for (int i = 0; i < slots._slotsClothesInventory.Length; i++)
            {
                if (slots.isUsedPlotInventory[i] == false)
                {
                    slots.isUsedPlotInventory[i] = true;
                    Instantiate(imageofObject[_ranfomObject], slots._slotsPlotInventory[i].transform);
                    break;
                }
              
            }
        }
        else if (imageofObject[_ranfomObject].CompareTag("OtherObjectsInventory"))
        {
            for (int i = 0; i < slots._slotsOtherInventory.Length; i++)
            {
                if (slots.isUsedOtherInventory[i] == false)
                {
                    slots.isUsedOtherInventory[i] = true;
                    Instantiate(imageofObject[_ranfomObject], slots._slotsOtherInventory[i].transform);
                    break;
                }
               
            }
        }
    }*/
}
