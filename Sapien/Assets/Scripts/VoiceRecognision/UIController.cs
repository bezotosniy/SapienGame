using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;

public class UIController : MonoBehaviour
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
    [SerializeField] private Image _textOnChoose;


    [Header("Microphone Image")]
    [Space(10f)]
    
    [SerializeField] private Image _imageofMicrophone;
    [SerializeField] private Image _keyboardImage;
   

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


    


    [Header("Inheritance")]
    [Space(10f)]
    [SerializeField] private VoicePlaybackForBattle _voicePlayback;
    [SerializeField] private VoiceregonsionForBattle _voiceRecognision;
    [SerializeField] private BattleController _battleController;
    


    [Header("Other UI")]
    //[SerializeField] private GameObject _repeatPanel;
    public GameObject _dialogPanel;
    public GameObject _microphonePanel;
    [SerializeField] private GameObject _scrollbar;


    [Header("Courutine")]
    [Space(10f)]
    public int index = 0;


    private void Update()
    {
        if(_isPlaying && _battleController.infinitely)
        {
            _stopButton.GetComponent<Image>().fillAmount += Time.deltaTime / _voicePlayback.AudioTask[0].clip.length;
        }
        else if(_isPlaying && !_battleController.infinitely)
        {
            _stopButton.GetComponent<Image>().fillAmount += Time.deltaTime / _battleController._dictorNotInfentlySaid[_battleController.RandomString].clip.length;
        }
    }
    
    public void InterLocutorSaid()
    {
        _scrollbar.SetActive(true);
       _microphonePanel.SetActive(true);
       _dialogPanel.SetActive(true);
       _mainText.sprite = _textOnWait.sprite;
       _background.sprite = _backgroundOnWait.sprite;
       _imageofMicrophone.enabled = true;
       _keyboardImage.enabled = false;
       _dialogBackground.DOColor(Color.white, 0.01f);
       _playButton.enabled = true;
       _stopButtonSquare.enabled = false;
       _stopButton.enabled = false;
       _taskText.enabled = true;
       DoFadeAll(0.5f);
    } 
    
    public void OnPlayVoice()
    {
       _microphonePanel.SetActive(true);
       _dialogPanel.SetActive(true);
       _mainText.sprite = _textOnWait.sprite;
       _background.sprite = _backgroundOnWait.sprite;
       _imageofMicrophone.enabled = true;
       _keyboardImage.enabled = false;
       _dialogBackground.DOColor(Color.white, 0.01f);
       _playButton.enabled = false;
       _stopButtonSquare.enabled = true;
       _stopButton.enabled = true;
       _isPlaying = true;
       _taskText.enabled = true;
       DoFadeAll(0.5f);
    }
    
     public void OnPlayVoiceDouble()
    {
      _microphonePanel.SetActive(true);
       _mainText.sprite = _textOnWait.sprite;
       _background.sprite = _backgroundOnWait.sprite;
       _imageofMicrophone.enabled = true;
       
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
      
        DoFadeAll(1);
       _stopButton.GetComponent<Image>().fillAmount = 0;
       StopAllCoroutines();
    }
    
     public IEnumerator OnCorrect()
    {
        yield return new WaitForSeconds(0.3f);
        _scrollbar.SetActive(true);
        _taskText.enabled = true;
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
      // _repeatPanel.SetActive(true);
       DoFadeAll(1);
       StartCoroutine(FadepanelClose());
    }
    
    public void IncorrectUI()
    {
        _scrollbar.SetActive(true);
        _taskText.enabled = true;
        _voiceRecognision.StopRecordButtonOnClickHandler();
       // _mainText.sprite = _textOnTap.sprite;
        _background.sprite = _backgroundOnSpeak.sprite;
        _stopButton.enabled = false;
        _stopButtonSquare.enabled = false;
        _playButton.enabled = true;
        DoFadeAll(1);
        StartCoroutine(ChangeIncorectColor());
        
    }

    public void ChooseLettersUI()
    {
        _scrollbar.SetActive(false);
        _taskText.enabled = false;
       _mainText.sprite = _textOnChoose.sprite;
       _background.sprite = _backgroundOnSpeak.sprite;
       _dialogBackground.DOColor(Color.white, 0.01f);
       _stopButton.enabled = false;
       _stopButtonSquare.enabled = false;
       _playButton.enabled = false;
       _isPlaying = false;
       _imageofMicrophone.enabled = false;
       _keyboardImage.enabled = true;
        DoFadeAll(1);
       _stopButton.GetComponent<Image>().fillAmount = 0;
       StopAllCoroutines();
    }


    public void DoPharses()
    {
        _microphonePanel.SetActive(false);
        _dialogPanel.SetActive(false);
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
        //_repeatPanel.SetActive(false);
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
          index++;
       StartCoroutine(ChangeCorrectSecond());
    }
    
    public IEnumerator ChangeCorrectSecond()
    {
        yield return new WaitForSeconds(0.3f);
        _dialogBackground.DOColor(Color.white, 0.3f);
        _dialogBackground.DOFade(1f, 0.3f);
        if(index == 3)
        {
            index = 0;
            yield break;
        }
        StartCoroutine(ChangeCorrectColor());
    }
    
    public IEnumerator ChangeIncorectColor(){
        yield return new WaitForSeconds(0.7f);
        _dialogBackground.DOColor(Color.red, 0.6f);
        _dialogBackground.DOFade(0.5f, 0.6f);
        index++;
       StartCoroutine(ChangeInCorrectSecond());
    }
    
    public IEnumerator ChangeInCorrectSecond()
    {
        yield return new WaitForSeconds(0.7f);
        _dialogBackground.DOColor(Color.white, 0.6f);
        _dialogBackground.DOFade(1, 0.6f);
       
           if(index == 1)
        {
           
            yield break;
            
        }
        StartCoroutine(ChangeIncorectColor());
    }
}
