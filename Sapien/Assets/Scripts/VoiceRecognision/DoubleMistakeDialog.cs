using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;

public class DoubleMistakeDialog : MonoBehaviour, IPointerDownHandler
{
   [Header("Inheritance")]
   [SerializeField] private VoicePlayBack _voicePlayback;
   [SerializeField] private VoiceRegontion2 _voiceRecognision;
   [SerializeField] private UIController _uiController;
   [SerializeField] private VoicePlaybleDouble _voicePlaybackDouble;
   [SerializeField] private DoubleDialogUI _doubleUIController;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(_voicePlayback.IsMistake == true && _voiceRecognision.Counter == _voiceRecognision.CounterNeed)
        {
           
           _voicePlaybackDouble.ListenToTryAgain();
		   _uiController._microphonePanel.SetActive(false);
		   _doubleUIController._doublePanel.SetActive(false);
		   _voicePlaybackDouble.isSure = true;
		   _voiceRecognision.comboCount = 0;
		   //_voiceRecognision.SetComboAndBest();
		   _voiceRecognision.MistakeCounter = 0;
           _voicePlayback.IsMistake = false;
        }
    }
}
