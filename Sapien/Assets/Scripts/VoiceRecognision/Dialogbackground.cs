using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;

public class Dialogbackground : MonoBehaviour, IPointerDownHandler
{
   [Header("Inheritance")]
   [SerializeField] private VoicePlayBack _voicePlayback;
   [SerializeField] private VoiceRegontion2 _voiceRecognision;
   [SerializeField] private UIControllerForCurScene _uiController;
   [SerializeField] private VoicePlaybleDouble _voicePlaybackDouble;
   [SerializeField] private DoubleDialogUI _doubleUIController;
 
    public void OnPointerDown(PointerEventData eventData)
    {
        
        _voiceRecognision.Counter++;
        if(_voicePlayback.IsMistake == true && _voiceRecognision.Counter != _voiceRecognision.CounterNeed)
        {
            _voicePlayback.IsMistake = false;
           
			StartCoroutine(_uiController.OnCorrect());
			_voiceRecognision.comboCount++;
			_voiceRecognision.SetComboAndBest();
			_voicePlayback.AudioCount++;
			StartCoroutine(_voicePlayback.InterlocutorSay());
            _voiceRecognision.MistakeCounter = 0;
            
        }
        else if(_voicePlayback.IsMistake == true && _voiceRecognision.Counter == _voiceRecognision.CounterNeed && _voicePlaybackDouble.IsDoublePlayingNow)
        {
            _voicePlayback.IsMistake = false;  
            
            _doubleUIController.OnCorrectDouble();
            StartCoroutine(_voicePlaybackDouble.ListenInterlocutor());
			_voiceRecognision.comboCount++;
			_voiceRecognision.SetComboAndBest();
            _voiceRecognision.MistakeCounter = 0;
            
        }
       
    }
}
