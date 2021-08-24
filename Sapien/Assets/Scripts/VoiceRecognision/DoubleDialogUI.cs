using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;
public class DoubleDialogUI : MonoBehaviour
{
    [Header("Double Task")]
    [Space(10f)]
    public GameObject _doublePanel;

    [Header("First Component")]
    [Space(10f)]
    [SerializeField] private Image _dialogBackgroundFirstComponent;
    [SerializeField] private Text _taskTextFirstComponent;
    [SerializeField] private Image _playButtonFirstComponent;
    [SerializeField] public Image _stopButtonFirstComponent;
    [SerializeField] private Image _stopButtonSquareFirstComponent;

    private bool _isPlayingFirstComponent;
   

    [Header("Second Component")]
    [Space(10f)]
    [SerializeField] private Image _dialogBackgroundSecondComponent;
    [SerializeField] private Text _taskTextSecondComponent;
    [SerializeField] private Image _playButtonSecondComponent;
    [SerializeField] public Image _stopButtonSecondComponent;
    [SerializeField] private Image _stopButtonSquareSecondComponent;

    private bool _isPlayingSecondComponent;


    [Header("Inheritance")]
    [Space(10f)]
    [SerializeField] private UIControllerForCurScene _uiContoroller;
    [SerializeField] private VoicePlaybleDouble _voicePlayback;

    [SerializeField] private VoiceRegontion2 _voiceRecognision;



    [Header("Other")]
    [SerializeField] private int index;
    public AudioSource _areYouSure;
    public AudioSource _sorryAudio;
    [SerializeField] private GameObject _fragmentCardPanel;



   private void Start()
   {
      SetTask(_voiceRecognision.DoubleTask[_voicePlayback.index], _voiceRecognision.DoubleTask[_voicePlayback.index + 1]);
   }
   
    private void Update()
   {
       if(_isPlayingFirstComponent)
       {
          _stopButtonFirstComponent.GetComponent<Image>().fillAmount += Time.deltaTime / _voicePlayback.DoubleAudioTask[_voicePlayback.index].clip.length;
       }

       if(_isPlayingSecondComponent)
       {
           _stopButtonSecondComponent.GetComponent<Image>().fillAmount += Time.deltaTime / _voicePlayback.DoubleAudioTask[_voicePlayback.index + 1].clip.length;
       }
   }
   
    public void OnActiveDoublePanel()
    {
       _uiContoroller.DialogItem.SetActive(false);
       _doublePanel.SetActive(true);
       DoFadeAll(0.5f);
       _playButtonFirstComponent.enabled = false;
       _stopButtonFirstComponent.enabled = true;
       _stopButtonSquareFirstComponent.enabled = true;
       _playButtonSecondComponent.enabled = false;
       _isPlayingFirstComponent = true;
       _uiContoroller.OnPlayVoiceDouble(); 
    }
    
    public void OnListenSecondtask()
    {
       DoFadeAll(0.5f);
       _stopButtonFirstComponent.enabled = false;
       _stopButtonSecondComponent.enabled = true;
       _stopButtonSquareSecondComponent.enabled = true;
       _stopButtonFirstComponent.enabled = false;
       _stopButtonSquareFirstComponent.enabled = false;
       _playButtonFirstComponent.enabled = true;
       _isPlayingFirstComponent = false;
       _isPlayingSecondComponent = true;
       _uiContoroller.OnPlayVoiceDouble();
    }
    
    public void SpeakDouble()
    {
       DoFadeAll(1);
       _stopButtonFirstComponent.GetComponent<Image>().fillAmount = 0;
       _stopButtonSecondComponent.GetComponent<Image>().fillAmount = 0;
       _isPlayingSecondComponent = false;
       _stopButtonSecondComponent.enabled = false;
       _stopButtonSquareSecondComponent.enabled = false;
       _playButtonSecondComponent.enabled = true;
       _uiContoroller.SpeakUI();
    }
    
     public void OnPlaySure()
    {
        _isPlayingFirstComponent = true;
        _stopButtonFirstComponent.enabled = true;
        _stopButtonSquareFirstComponent.enabled = true;
        _playButtonFirstComponent.enabled = false;
        _uiContoroller.OnPlayVoiceDouble();
        _sorryAudio.Play();
        StartCoroutine(OnStopSurePlay());
    }
    
    public void OnPlayFirstItem()
    {
        _voiceRecognision.StopRecordButtonOnClickHandler();
        _isPlayingFirstComponent = true;
        _stopButtonFirstComponent.enabled = true;
        _stopButtonSquareFirstComponent.enabled = true;
        _playButtonFirstComponent.enabled = false;
        _uiContoroller.OnPlayVoiceDouble();
        _voicePlayback.DoubleAudioTask[_voicePlayback.index].Play();
        StartCoroutine(OnStopFirstItemPlay());
        _playButtonSecondComponent.GetComponent<Button>().enabled = false;
    }
    
    private IEnumerator OnStopFirstItemPlay()
    {
        yield return new WaitForSeconds(_voicePlayback.DoubleAudioTask[_voicePlayback.index].clip.length);
        _isPlayingFirstComponent = false;
        _stopButtonFirstComponent.enabled = false;
        _stopButtonSquareFirstComponent.enabled = false;
        _playButtonFirstComponent.enabled = true;
        _uiContoroller.SpeakUI();
        _stopButtonFirstComponent.GetComponent<Image>().fillAmount = 0;
        _voiceRecognision.StartRecordButtonOnClickHandler();
        _playButtonSecondComponent.GetComponent<Button>().enabled = true;
    }
    
    private IEnumerator OnStopSurePlay()
    {
        yield return new WaitForSeconds(_sorryAudio.clip.length);
        _isPlayingFirstComponent = false;
        _stopButtonFirstComponent.enabled = false;
        _stopButtonSquareFirstComponent.enabled = false;
        _playButtonFirstComponent.enabled = true;
        _uiContoroller.SpeakUI();
        _stopButtonFirstComponent.GetComponent<Image>().fillAmount = 0;
    }
    
    public void OnPlaySecondItem()
    {
        _voiceRecognision.StopRecordButtonOnClickHandler();
        _isPlayingSecondComponent = true;
        _stopButtonSecondComponent.enabled = true;
        _stopButtonSquareSecondComponent.enabled = true;
        _playButtonSecondComponent.enabled = false;
        _uiContoroller.OnPlayVoiceDouble();
        _voicePlayback.DoubleAudioTask[_voicePlayback.index + 1].Play();
        StartCoroutine(OnStopSecondItemPlay());
        _playButtonFirstComponent.GetComponent<Button>().enabled = false;
    }
    
    private IEnumerator OnStopSecondItemPlay()
    {
        yield return new WaitForSeconds(_voicePlayback.DoubleAudioTask[_voicePlayback.index + 1].clip.length);
        _isPlayingSecondComponent = false;
        _stopButtonSecondComponent.enabled = false;
        _stopButtonSquareSecondComponent.enabled = false;
        _playButtonSecondComponent.enabled = true;
        _uiContoroller.SpeakUI();
        _stopButtonSecondComponent.GetComponent<Image>().fillAmount = 0;
        _voiceRecognision.StartRecordButtonOnClickHandler();
        _playButtonFirstComponent.GetComponent<Button>().enabled = true;
    }
    
    private void DoFadeAll(float value)
    {
     _uiContoroller.DoFadeAll(value);
     _dialogBackgroundFirstComponent.DOFade(value, 0.01f);
     _taskTextFirstComponent.DOFade(value, 0.01f);
     _dialogBackgroundSecondComponent.DOFade(value,0.01f);
     _taskTextSecondComponent.DOFade(value, 0.01f);
     _stopButtonFirstComponent.DOFade(value, 0.01f);
     _stopButtonSquareFirstComponent.DOFade(value, 0.01f);
     _playButtonFirstComponent.DOFade(value, 0.01f);
     _stopButtonSecondComponent.DOFade(value, 0.01f);
     _stopButtonSquareSecondComponent.DOFade(value, 0.01f);
     _playButtonSecondComponent.DOFade(value, 0.01f);
    }
    
    public void SetTask(string FirstTask, string SecondTask)
    {
     _taskTextFirstComponent.text = FirstTask;
     _taskTextSecondComponent.text = SecondTask;
    }
    
    public void OnCorrectDouble()
    {
       StartCoroutine(ChangeCorrectColor());
    }
    
    public IEnumerator ChangeCorrectColor()
    {
       yield return new WaitForSeconds(0.3f);
       _dialogBackgroundSecondComponent.DOColor(Color.green, 0.3f);
        if(index == 2)
        {
            _fragmentCardPanel.SetActive(false);
            index = 0;
            yield break;
        }
       StartCoroutine(ChangeCorrectSecond());
    }
    
    public IEnumerator ChangeCorrectSecond()
    {
        yield return new WaitForSeconds(0.3f);
        _dialogBackgroundSecondComponent.DOColor(Color.white, 0.3f);
        _dialogBackgroundFirstComponent.DOFade(1f, 0.3f);
        _dialogBackgroundSecondComponent.DOFade(1f, 0.3f);
        index++;
        StartCoroutine(ChangeCorrectColor());
    }
    
     public void InCorrect()
    {
        StartCoroutine(ChangeIncorectColor());
    }
    
    public IEnumerator ChangeIncorectColor()
    {
        yield return new WaitForSeconds(0.7f);
        _dialogBackgroundSecondComponent.DOColor(Color.red, 0.6f);
        _dialogBackgroundFirstComponent.DOColor(Color.red, 0.6f);
        _dialogBackgroundFirstComponent.DOFade(0.5f, 0.6f);
        _dialogBackgroundSecondComponent.DOFade(0.5f, 0.6f);
        if(index == 3)
        {
            index = 0;
            yield break;
            
        }
       StartCoroutine(ChangeInCorrectSecond());
    }
    
    public IEnumerator ChangeInCorrectSecond()
    {
         yield return new WaitForSeconds(0.7f);
        _dialogBackgroundSecondComponent.DOColor(Color.white, 0.6f);
        _dialogBackgroundFirstComponent.DOColor(Color.white, 0.6f);
        _dialogBackgroundFirstComponent.DOFade(1f, 0.6f);
        _dialogBackgroundSecondComponent.DOFade(1f, 0.6f);
        index++;
        StartCoroutine(ChangeIncorectColor());
    }
    
    public void SetColorOfItem(Color color)
    {
        _dialogBackgroundFirstComponent.color = color;
        _dialogBackgroundSecondComponent.color = color;
    }





   

    
}
