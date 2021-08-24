using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition.Examples;

public class SpeakWithVariant : MonoBehaviour
{
    [Header("Tasks")]
    [SerializeField] private string[] _task;
    [SerializeField] private Text[] _taskText;
    public string _correctTask;
    public bool IsSpeakWithVariants;
    private int _correctAnswer;


    [Header("Inheratence")]
    [Space(20f)]
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



    private void Start()
    {
        _hammerBattle._IsTimeGo = true;
        StartCoroutine(_hammerBattle.TimeGo());

        IsSpeakWithVariants = true;
        _correctAnswer = Random.Range(0, 3);

        for(int i = 0; i < _taskText.Length; i++)
        {
            _taskText[i].text = _task[i];
        }
        _taskText[_correctAnswer].text = _correctTask;


       IsSpeakWithVariants = true;
      _textImage.sprite = _wait;
      _background.sprite = _waitBackground;
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
            IsSpeakWithVariants = true;
        }
        else
        {
            _voiceregonsion.StopRecordButtonOnClickHandler();
            _textImage.sprite = _wait;
            _background.sprite = _waitBackground;
            
        }
    }

}
