using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using DG.Tweening;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;

public class VoicePlayble : MonoBehaviour, IPointerDownHandler
{
    [Header("Background")]
    [SerializeField] private Image _background;
    [SerializeField] private Image _backgroundOnSpeak;
    [SerializeField] private Image _backgroundOnWait;

    [Header("DialogItems")]
    [Space(10f)]
    [SerializeField] private Image _dialogBackGround;



   

    [Header("Text")]
    [Space(10f)]
    [SerializeField] private Image _text;
    [SerializeField] private Image _textOnWait;
    [SerializeField] private Image _textOnRepeat;
    [SerializeField] private Image _textOnSpeak;
    [SerializeField] private Image _textOnClick;



    [Header("Button")]
    [Space(10f)]
    [SerializeField] private Image _playButton;
    [SerializeField] private Image _stopButton;
    [SerializeField] private Image _stopSquarebutton;


    [Header("Microphone")]
    [Space(10f)]
    [SerializeField] private Image _microphoneImage;
    [SerializeField] private Image _mouseImage;


    [Header("Combo and Best")]
    [Space(10f)]
    [SerializeField] private GameObject _comboAndBestPanel;
    
   
   
   [Header("Voice")]
    [Space(10f)]
    [SerializeField] private AudioSource[] _audioClip;
    public AudioSource[] _characterSpeak;
    [SerializeField] private VoiceRegontion2 _voiceRecognision;
    public string[] Task;

    public int MistakeCount;

    public Text TaskText;
    public int index = 0;
    private bool _isPlaying;

    public bool IsRecording;


    [Header("Cut-Scene")]
    [Space(10f)]
    [SerializeField] private PlayableDirector playableDirector;
    [SerializeField] private Transform _player;


    [Header("Mistake")]
    [Space(10f)]
    public bool IsTakeMistake;


    private void Start()
    {
        OnClickPlayButton();
        _comboAndBestPanel.SetActive(false);
        IsTakeMistake = false;
        _microphoneImage.enabled = true;
        _background.sprite = _backgroundOnWait.sprite;
        _text.sprite = _textOnWait.sprite;
        _playButton.enabled = true;
        IsRecording = false;
    }


    private void Update()
    {
       if(IsRecording == true)
       {
            _microphoneImage.DOFade(1, 0.01f);
            _comboAndBestPanel.SetActive(true);
       }
       else
       {
             _microphoneImage.DOFade(0.5f, 0.01f);
            _comboAndBestPanel.SetActive(false);
       }
         
        if(_isPlaying == true)
        {
            _stopButton.GetComponent<Image>().fillAmount += Time.deltaTime / _audioClip[index].clip.length;
        }
    }


    public void OnClickPlayButton()
    {
        DoFadeItems(0.5f);
        SetWaitBackground();
        SetStopButton();
        _isPlaying = true;
        _audioClip[index].Play();
        //_voiceRecognision.StopRecordButtonOnClickHandler();
        StartCoroutine(EndOfPlayButton());
    }

    private IEnumerator EndOfPlayButton()
    {
        DoFadeItems(1);
        _stopButton.GetComponent<Image>().fillAmount = 0;
        yield return new WaitForSeconds(_audioClip[index].clip.length);
        IsRecording = true;
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

   private void SetSpeakBackground()
    {
     
       _text.sprite = _textOnSpeak.sprite;
       _background.sprite = _backgroundOnSpeak.sprite;
       _microphoneImage.enabled = true;
       _mouseImage.enabled = false;
    }

    public void SetWaitBackground()
    {
        
        _microphoneImage.enabled = true;
        _mouseImage.enabled = false;
        _text.sprite = _textOnWait.sprite;
        _background.sprite = _backgroundOnWait.sprite;
        

    }

    public void SetRepeatBackground()
    {
        _microphoneImage.enabled = true;
        _mouseImage.enabled = false;
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


    private void SetTapBackground()
    {
     _text.sprite = _textOnClick.sprite;
     _mouseImage.enabled = true;
     _microphoneImage.enabled = false;
    }


  public void CharacterSpeak()
  {
     
    _characterSpeak[index].Play();
    
  }
    


    public void CheckMistake()
    {
         if(MistakeCount == 2)
        {
          IsRecording = false;
          IsTakeMistake = true;
          StartCoroutine(DialogColorChangeRed());
          SetTapBackground();
         // _voiceRecognision.StopRecordButtonOnClickHandler();
        }
    }
 

   public IEnumerator DialogColorChangeRed()
   {
       yield return new WaitForSeconds(1f);
       _dialogBackGround.DOColor(Color.red, 0.5f);
       _mouseImage.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f);
       StartCoroutine(DialogColorChangeWhite());
       

   }
 

   public IEnumerator DialogColorChangeWhite()
    {
       yield return new WaitForSeconds(1f); 
       _dialogBackGround.DOColor(Color.white, 0.5f);
       _mouseImage.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.5f);
       StartCoroutine(DialogColorChangeRed());
    }
    


    public void DoFadeItems(float value)
    {
        _microphoneImage.DOFade(value, 0.01f);
        _dialogBackGround.DOFade(value,0.01f);
        _playButton.DOFade(value, 0.01f);
        TaskText.DOFade(value, 0.01f);
        
    }
}
