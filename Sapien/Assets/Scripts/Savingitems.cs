using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savingitems : MonoBehaviour
{
    [SerializeField] private GameObject _clothesPanel;
    private void Start()
    {
        for (int i = 0; i < _clothesPanel.transform.childCount; i++)
        {
            _clothesPanel.transform.GetChild(i).GetComponent<InventoryItemClothes>().LoadInfo();
        }
    }
}
     
