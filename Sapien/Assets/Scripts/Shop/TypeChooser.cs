using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TypeChooser : MonoBehaviour
{
    [SerializeField] private GameObject[] _types;
    [SerializeField] private GameObject[] _background;
    
   
    
    


    public void ChoseType(int index)
    {
        foreach(GameObject type in _types)
        {
            type.SetActive(false);
        }
        _types[index].SetActive(true);
       

        foreach(GameObject backgrounds in _background)
        {
            backgrounds.SetActive(false);
        }
        _background[index].SetActive(true);
        _background[index].transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f);
    }


  



}
