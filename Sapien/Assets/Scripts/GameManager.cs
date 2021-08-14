using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PhoneManager _phoneManager;


    public bool IsPhoneOpened
    {
        get
        {
            int cnt = 0;
            foreach(Transform child in _phoneManager.transform){
                if(child.gameObject.activeSelf)
                    cnt++;
            }

            return cnt > 1;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
            _phoneManager = GameObject.FindObjectOfType<PhoneManager>();
            SceneManager.sceneLoaded += (arg0, mode) => _phoneManager = GameObject.FindObjectOfType<PhoneManager>();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

}
