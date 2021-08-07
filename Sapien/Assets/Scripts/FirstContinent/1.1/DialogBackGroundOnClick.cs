using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;


public class DialogBackGroundOnClick : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private VoiceRegontion2 _voiceRegontion;
    [SerializeField] private VoicePlayble _voicePlayble;


  public void OnPointerDown(PointerEventData eventData)
    {
        if(_voicePlayble.IsTakeMistake == true)
        {
       // StartCoroutine(_voiceRegontion.CharacterSpeakCourutine());
        //StartCoroutine(_voiceRegontion.SetTask());
        //StopCoroutine(_voicePlayble.DialogColorChangeGreen());
        //StopCoroutine(_voicePlayble.DialogColorChangeWhite());
        _voicePlayble.IsTakeMistake = false;
        }
    }
}
