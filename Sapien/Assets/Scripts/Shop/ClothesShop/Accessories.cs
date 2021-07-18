using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShopSystem;
using UnityEngine.UI;

public class Accessories : MonoBehaviour
{
    public ShopSaveScriptable shopSave;
    [SerializeField] private GameObject _mainPrefab;
    [SerializeField] private Text[] _name;
    [SerializeField] private Text[] _price;
    public int currentIndex;

    private void Start()
    {
        
        for (int i = 0; i < shopSave.itemsInfo.Length; i++)
        {
            _name[i].text = shopSave.itemsInfo[i].name;
            _price[i].text = shopSave.itemsInfo[i].price.ToString();

        }
    }

    public void ChoseAccessories(int index)
    {
        currentIndex = index;
        _mainPrefab.GetComponent<MeshFilter>().sharedMesh = shopSave.itemsInfo[index].prefab.GetComponent<MeshFilter>().sharedMesh;
        _mainPrefab.GetComponent<MeshRenderer>().sharedMaterial = shopSave.itemsInfo[index].prefab.GetComponent<MeshRenderer>().sharedMaterial;
    }

    public void BuyItem()
    {

        shopSave.itemsInfo[currentIndex].isUnlocked = true;


    }
}
