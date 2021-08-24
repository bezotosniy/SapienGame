using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition.Examples;

public class SpeakWithCard : MonoBehaviour
{
    [Header("Inheratence")]
    [SerializeField] private VoiceregonsionForBattle _voiceregonsion;
    [SerializeField] private HammerBattle _hammerBattle;
    

    [Header("UI")]
    [Space(20f)]
    [SerializeField] private Image _textImage;
    [SerializeField] private Sprite _speak;
    [SerializeField] private Sprite _wait;
    [SerializeField] private Image _background;
    [SerializeField] private Sprite _speakbackground;
    [SerializeField] private Sprite _waitBackground;
    [SerializeField] private Image _fragmentCardImage;
    [SerializeField] private Image _fragmentCard;

    [Header("Voice")]
    [Space(20f)]
    public bool IsSpeakWithCard;
    public string Task;

    private void Start()
    {
      _hammerBattle._IsTimeGo = true;
      StartCoroutine(_hammerBattle.TimeGo());

      IsSpeakWithCard = true;
      _textImage.sprite = _wait;
      _background.sprite = _waitBackground;
      _fragmentCardImage.sprite = _fragmentCard.sprite;
      StartCoroutine(StartRecord());
    }

    private IEnumerator StartRecord()
    {
        yield return new WaitForSeconds(5);
        _textImage.sprite = _speak;
        _background.sprite = _speakbackground;
        _voiceregonsion.StartRecordButtonOnClickHandler();
    }


    public void ChangeTextImage(bool isSpeak)
    {
        if(isSpeak)
        {
             _voiceregonsion.StartRecordButtonOnClickHandler();
            _textImage.sprite = _speak;
            _background.sprite = _speakbackground;
            IsSpeakWithCard = true;
        }
        else
        {
            _voiceregonsion.StopRecordButtonOnClickHandler();
            _textImage.sprite = _wait;
            _background.sprite = _waitBackground;
            IsSpeakWithCard = false;
        }
    }



}
