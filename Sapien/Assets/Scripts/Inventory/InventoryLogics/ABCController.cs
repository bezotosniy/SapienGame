using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABCController : MonoBehaviour
{
    [SerializeField] private  GameObject[] _alphabet;
    [SerializeField] private Saving _saving;
    private int _randomNumber;

    private void Start()
    {
        for(int i = 0; i< _saving.repository.isOpened.Length; i++)
        {
            if(_saving.repository.isOpened[i] == true)
            {
                _alphabet[i].SetActive(true);
            }
        }

    }


    private void Awake()
    {
        _saving.Load();
    }



    public void OpenLetter()
    {
        _randomNumber = Random.Range(0, 26);
        if (_alphabet[_randomNumber].activeSelf == false)
        {
            _alphabet[_randomNumber].SetActive(true);
            _saving.repository.isOpened[_randomNumber] = true;
            _saving.Save();
           
            
        }
        else if(_alphabet[_randomNumber].activeSelf == true)
        {
            OpenLetter();
        }
        else
        {
            Debug.Log("You get all letters!");
        }
    }
}
