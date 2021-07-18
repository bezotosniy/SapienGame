using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;

public class VoicePlayble : MonoBehaviour, IPointerDownHandler
{
    [Header("Background")]
    [SerializeField] private Image _background;
    [SerializeField] private Image _backgroundOnSpeak;
    [SerializeField] private Image _backgroundOnWait;

    [Header("Text")]
    [Space(10f)]
    [SerializeField] private Image _text;
    [SerializeField] private Image _textOnWait;
    [SerializeField] private Image _textOnRepeat;
    [SerializeField] private Image _textOnSpeak;

    [Header("Button")]
    [Space(10f)]
    [SerializeField] private Image _playButton;
    [SerializeField] private Image _stopButton;
    [SerializeField] private Image _stopSquarebutton;

    [Header("Voice")]
    [Space(10f)]
    [SerializeField] private AudioSource[] _audioClip;
    [SerializeField] private VoiceRecognision _voiceRecognision;
    [SerializeField] private string[] _task;
    private int index = 0;
    private bool _isPlaying;

    [Header("Cut-Scene")]
    [Space(10f)]
    [SerializeField] private PlayableDirector playableDirector;
    [SerializeField] private Transform _player;


    private void Start()
    {
        _background.sprite = _backgroundOnWait.sprite;
        _text.sprite = _textOnWait.sprite;
        _playButton.enabled = true;
    }


    private void Update()
    {
        if(_isPlaying == true)
        {
            _stopButton.GetComponent<Image>().fillAmount += Time.deltaTime / _audioClip[index].clip.length;
        }
    }


    public void OnClickPlayButton()
    {
        SetWaitBackground();
        SetStopButton();
        _isPlaying = true;
        _audioClip[index].Play();
        _voiceRecognision.StopRecordButtonOnClickHandler();
        StartCoroutine(EndOfPlayButton());
    }

    private IEnumerator EndOfPlayButton()
    {
        _stopButton.GetComponent<Image>().fillAmount = 0;
        yield return new WaitForSeconds(_audioClip[index].clip.length);
        SetPlayButton();
        SetSpeakBackground();
        _voiceRecognision.StartRecordButtonOnClickHandler();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SetWaitBackground();
        playableDirector.Play();
        OnClickPlayButton();
        

    }


    private IEnumerator Compare()
    {
        yield return new  WaitForSeconds(1f);
        
    }

    private void SetSpeakBackground()
    {
        _text.sprite = _textOnSpeak.sprite;
        _background.sprite = _backgroundOnSpeak.sprite;
    }

    private void SetWaitBackground()
    {
        _text.sprite = _textOnWait.sprite;
        _background.sprite = _backgroundOnWait.sprite;
    }

    private void SetRepeatBackground()
    {
        _text.sprite = _textOnRepeat.sprite;
        _background.sprite = _backgroundOnSpeak.sprite;
    }
    private void SetPlayButton()
    {
        _stopSquarebutton.enabled = false;
        _stopButton.enabled = false;
        _playButton.enabled = true;
    }

    private void SetStopButton()
    {
        _stopSquarebutton.enabled = true;
        _stopButton.enabled = true;
        _playButton.enabled = false;
    }

}
