using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;

public class UIControllerForCurScene : MonoBehaviour
{
    [Header("Microphone Background")]
    [SerializeField] private Image _background;
    [SerializeField] private Image _backgroundOnSpeak;
    [SerializeField] private Image _backgroundOnWait;


    [Header("Text")]
    [Space(10f)]
    [SerializeField] private Image _mainText;
    [SerializeField] private Image _textOnSpeak;
    [SerializeField] private Image _textOnWait;
    [SerializeField] private Image _textOnRepeat;
    [SerializeField] private Image _textOnTap;


    [Header("Microphone Image")]
    [Space(10f)]
    
    [SerializeField] private Image _imageofMicrophone;
    [SerializeField] private Image _imageOfmouse;


    [Header("Dialog Item UI")]
    [Space(10f)]
    public GameObject DialogItem;
    [SerializeField] private Image _dialogBackground;
    [SerializeField] private Color _dialogCorrect;
    [SerializeField] private Color _dialogInCorrect;
    [SerializeField] private Color _dialogDefaultColor;
    [SerializeField] private Text _taskText;


    [Header("Play Button")]
    [Space(10f)]
    [SerializeField] private Image _playButton;
    [SerializeField] public Image _stopButton;
    [SerializeField] private Image _stopButtonSquare;

    private bool _isPlaying;


    [Header("Combo and Best ")]
    [Space(10f)]
    [SerializeField] private GameObject _comboPanel;
    [SerializeField] private GameObject _bestPanel;


    [Header("Inheritance")]
    [Space(10f)]
    [SerializeField] private VoicePlayBack _voicePlayback;
    [SerializeField] private VoiceRegontion2 _voiceRecognision;


    [Header("Other UI")]
    [SerializeField] private GameObject _repeatPanel;
    [SerializeField] private GameObject _dialogPanel;
    public GameObject _microphonePanel;


    [Header("Courutine")]
    [Space(10f)]
    public int index = 0;


    private void Update()
    {
        if(_isPlaying)
        {
            _stopButton.GetComponent<Image>().fillAmount += Time.deltaTime / _voicePlayback.AudioTask[(int)_voicePlayback.AudioCount].clip.length;
        }
    }
    
    public void InterLocutorSaid()
    {
       _microphonePanel.SetActive(false);
       _dialogPanel.SetActive(false);
       _mainText.sprite = _textOnWait.sprite;
       _background.sprite = _backgroundOnWait.sprite;
       _imageofMicrophone.enabled = true;
       _imageOfmouse.enabled = false;
       _dialogBackground.DOColor(Color.white, 0.01f);
       _playButton.enabled = false;
       _stopButtonSquare.enabled = true;
       _stopButton.enabled = true;
       _comboPanel.SetActive(false);
       _bestPanel.SetActive(false);
       DoFadeAll(0.5f);
    } 
    
    public void OnPlayVoice()
    {
       _microphonePanel.SetActive(true);
       _dialogPanel.SetActive(true);
       _mainText.sprite = _textOnWait.sprite;
       _background.sprite = _backgroundOnWait.sprite;
       _imageofMicrophone.enabled = true;
       _imageOfmouse.enabled = false;
       _dialogBackground.DOColor(Color.white, 0.01f);
       _playButton.enabled = false;
       _stopButtonSquare.enabled = true;
       _stopButton.enabled = true;
       _isPlaying = true;
       DoFadeAll(0.5f);
    }
    
     public void OnPlayVoiceDouble()
    {
      _microphonePanel.SetActive(true);
       _mainText.sprite = _textOnWait.sprite;
       _background.sprite = _backgroundOnWait.sprite;
       _imageofMicrophone.enabled = true;
       _imageOfmouse.enabled = false;
       _dialogBackground.DOColor(Color.white, 0.01f);
       _playButton.enabled = false;
       _stopButtonSquare.enabled = true;
       _stopButton.enabled = true;
       _isPlaying = true;
       DoFadeAll(0.5f);
    }
    
     public void SpeakUI()
    {
       _mainText.sprite = _textOnSpeak.sprite;
       _background.sprite = _backgroundOnSpeak.sprite;
       _dialogBackground.DOColor(Color.white, 0.01f);
       _stopButton.enabled = false;
       _stopButtonSquare.enabled = false;
       _playButton.enabled = true;
       _isPlaying = false;
        _comboPanel.SetActive(true);
       _bestPanel.SetActive(true);
        DoFadeAll(1);
       _stopButton.GetComponent<Image>().fillAmount = 0;
       StopAllCoroutines();
    }
    
     public IEnumerator OnCorrect()
    {
        yield return new WaitForSeconds(1);
        _mainText.sprite = _textOnSpeak.sprite;
       _background.sprite = _backgroundOnSpeak.sprite;
       _dialogBackground.DOColor(Color.green, 1f);
       _stopButton.enabled = false;
       _stopButtonSquare.enabled = false;
       _playButton.enabled = true;
       DoFadeAll(1);
       StartCoroutine(ChangeCorrectColor());
    }
    
    public void RepeatUI()
    {
       _mainText.sprite = _textOnRepeat.sprite;
       _background.sprite = _backgroundOnSpeak.sprite;
       _dialogBackground.DOColor(Color.white, 0.01f);
       _stopButton.enabled = false;
       _stopButtonSquare.enabled = false;
       _playButton.enabled = true;
       _repeatPanel.SetActive(true);
       DoFadeAll(1);
       StartCoroutine(FadepanelClose());
    }
    
    public void IncorrectUI()
    {
        _voiceRecognision.StopRecordButtonOnClickHandler();
        _mainText.sprite = _textOnTap.sprite;
        _background.sprite = _backgroundOnSpeak.sprite;
        _imageOfmouse.enabled = true;
        _imageofMicrophone.enabled = false;
        _stopButton.enabled = false;
        _stopButtonSquare.enabled = false;
        _playButton.enabled = true;
        DoFadeAll(1);
        StartCoroutine(ChangeIncorectColor());
        
    }
    
    public void DoFadeAll(float value)
    {
        _mainText.DOFade(value, 0.01f);
        _background.DOFade(value, 0.01f);
        _imageofMicrophone.DOFade(value, 0.01f);
        _playButton.DOFade(value, 0.01f);
        _stopButton.DOFade(value, 0.01f);
        _taskText.DOFade(value, 0.01f);
        _stopButtonSquare.DOFade(value, 0.01f);
    }
    
    private IEnumerator FadepanelClose()
    {
        yield return new WaitForSeconds(2f);
        _repeatPanel.SetActive(false);
    }
    
    public void SetTask(string task)
    {
        _taskText.text = task;
    }
    
    public IEnumerator ChangeCorrectColor()
    {
       yield return new WaitForSeconds(0.3f);
       _dialogBackground.DOColor(Color.green, 0.3f);
       _dialogBackground.DOFade(0.5f, 0.3f);
         if(index == 3)
        {
            index = 0;
            yield break;
        }
       StartCoroutine(ChangeCorrectSecond());
    }
    
    public IEnumerator ChangeCorrectSecond()
    {
        yield return new WaitForSeconds(0.3f);
        _dialogBackground.DOColor(Color.white, 0.3f);
        _dialogBackground.DOFade(1f, 0.3f);
        index++;
        StartCoroutine(ChangeCorrectColor());
    }
    
    public IEnumerator ChangeIncorectColor(){
        yield return new WaitForSeconds(0.7f);
        _dialogBackground.DOColor(Color.red, 0.6f);
        _dialogBackground.DOFade(0.5f, 0.6f);
        if(_voicePlayback.IsMistake == false)
        {
           
            yield break;
            
        }
       StartCoroutine(ChangeInCorrectSecond());
    }
    
    public IEnumerator ChangeInCorrectSecond()
    {
        yield return new WaitForSeconds(0.7f);
        _dialogBackground.DOColor(Color.white, 0.6f);
        _dialogBackground.DOFade(1, 0.6f);
        index++;
        StartCoroutine(ChangeIncorectColor());
    }
}
