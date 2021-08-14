using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;

public class VoicePlaybleDouble : MonoBehaviour
{
    [Header("Inheritance")]
    [SerializeField] private DoubleDialogUI _uiController;
    [SerializeField] private VoiceRegontion2 _voiceRecognision;
    [SerializeField] private UIControllerForCurScene _uiControllerFirst;
    [SerializeField] private VoicePlayBack _voicePlayback;

    [Header("Panels")]
    [Space(10f)]
    public GameObject _dialogPanel;
    [SerializeField] private GameObject _microphonePanel;

    [Header("Audio")]
    [Space(10f)]
    public AudioSource[] DoubleAudioTask;

    [Header("Bools")]
    [Space(10f)]
    public bool IsDoublePlayingNow;
    public bool isSure;

    [Header("Other")]
    [Space(10F)]
    public int index;

   
   
  
    private void Start()
    {
       index = 0;
    }
    
    public void OnListenInterlocutor()
    {
       _voicePlayback.AudioInterlocutor[_voicePlayback.AudioCount].Play();
       StartCoroutine(OnClickPlayButtonDouble());
       IsDoublePlayingNow = true;
    }
    
    private IEnumerator OnClickPlayButtonDouble()
    {
        yield return new WaitForSeconds(_voicePlayback.AudioInterlocutor[_voicePlayback.AudioCount].clip.length + 2);
        _uiController.OnActiveDoublePanel();
        DoubleAudioTask[index].Play();
        StartCoroutine(ListenSecondTask());
    }
    
     private IEnumerator ListenSecondTask()
    {
        yield return new WaitForSeconds(DoubleAudioTask[index].clip.length + 0.7f);
        _uiController.OnListenSecondtask();
        DoubleAudioTask[index + 1].Play();
        StartCoroutine(Speak());
    }
    
    private IEnumerator Speak()
    {
        yield return new WaitForSeconds(DoubleAudioTask[index + 1].clip.length + 0.2f);
        _voiceRecognision.StartRecordButtonOnClickHandler();
        _uiController.SpeakDouble();
    }
    
    public  void ListenToTryAgain()
    {
       _uiControllerFirst.SetTask(_voiceRecognision.Sure);
       _uiController._areYouSure.Play();
       StartCoroutine(TryAgain());  
    }
    
    public IEnumerator TryAgain()
    {
        yield return new WaitForSeconds(_uiController._areYouSure.clip.length);
        isSure = true;
        IsDoublePlayingNow = false;
        _uiController.OnPlaySure();
        _dialogPanel.SetActive(true);
        _microphonePanel.SetActive(true);
        _voiceRecognision.StartRecordButtonOnClickHandler();
    }
   
    public IEnumerator ListenInterlocutor()
   {
       yield return new WaitForSeconds(2);
       OnListenInterlocutor();
       _dialogPanel.SetActive(false);
	   _microphonePanel.SetActive(false);
       _uiController.SetColorOfItem(Color.white);
   }
}
