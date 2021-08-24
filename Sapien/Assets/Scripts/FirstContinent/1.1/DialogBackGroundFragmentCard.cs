using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;
using UnityEngine.EventSystems;

public class DialogBackGroundFragmentCard : MonoBehaviour, IPointerDownHandler
{
   [Header("Inheritance")]
   [SerializeField] private VoicePlayBack _voicePlayback;
   [SerializeField] private VoiceRegontion2 _voiceRecognision;
   [SerializeField] private VoicePlaybleDouble _voicePlaybackDouble;
   [SerializeField] private DoubleDialogUI _doubleUIController;
   [SerializeField] private TaskWithFragmentCard _uiController;

    public void OnPointerDown(PointerEventData eventData)
    {
        _voiceRecognision.Counter++;
        _voicePlayback.IsMistake = false;
        StartCoroutine(_uiController.OnCorrect());
		_voiceRecognision.comboCount++;
		_voiceRecognision.SetComboAndBest();
		_voicePlayback.AudioCount++;
        if(_voiceRecognision.Counter == _voiceRecognision.CounterNeed)
        {
            StartCoroutine(_voicePlaybackDouble.ListenInterlocutor());
        }
        else
        {
            StartCoroutine(_voicePlayback.InterlocutorSay());
        }
		
        _voiceRecognision.MistakeCounter = 0;
        StartCoroutine(ClosePanel());

    }


    private IEnumerator ClosePanel()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
