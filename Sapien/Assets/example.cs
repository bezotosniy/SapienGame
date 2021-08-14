using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class example : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private AudioSource _audio;
   
    void Start()
    {
        _button.onClick.AddListener(OnClick);
    }

   
    void OnClick()
    {
        _audio.Play();
    }
}
