using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InventoryButtons : MonoBehaviour
{
    [SerializeField] private GameObject[] _inventoryPanels;
    [SerializeField] private GameObject[] _selectBackround;

    [SerializeField] private Transform[] _dressingcells;
    [SerializeField] private Transform _dressingPanel;
    



    public void SelectType(int index)
    {
        foreach(GameObject type in _inventoryPanels)
        {
            type.SetActive(false);
        }
        _inventoryPanels[index].SetActive(true);
        if(index == 2)
        {
            for(int i = 0; i < _dressingcells.Length; i++)
            {
                _dressingcells[i].DOMoveX(1395, 0.7f);
                _dressingPanel.DOMove(new Vector3(0.49f,2.86f, -5.9f), 0.7f);
            }
            
        }
        else
        {
            for (int i = 0; i < _dressingcells.Length; i++)
            {
                _dressingcells[i].DOMoveX(2100, 0.7f);
                _dressingPanel.DOMove(new Vector3(7.23919f, 3.91f, -7.714f), 0.7f);
            }
        }

        foreach(GameObject background in _selectBackround)
        {
            background.SetActive(false);
        }
        _selectBackround[index].SetActive(true);
        _selectBackround[index].transform.DOScale(new Vector3(1.25f, 1.25f, 1.25f), 0.05f);



    }
}
