using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;

public class VoicePlayBack : MonoBehaviour
{
  [Header("Inheritance")]
  [SerializeField] private UIControllerForCurScene _uiController;
  [SerializeField] private VoiceRegontion2 _voiceRegontision;
  [SerializeField] private DoubleDialogUI _doubleUiController;
  [SerializeField] private VoicePlaybleDouble _doubleVoicePlayble;

  [Header("Audio(You can Change)")]
  [Space(10f)]
  public AudioSource[] AudioTask;
  public AudioSource[] AudioInterlocutor;

  [Header("Other")]
  [Space(10f)]
  public int AudioCount;
  public bool IsMistake;



    private void Start()
    {
        OnClickPlayButton();
    }
    
    public void OnClickPlayButton()
    {
        _voiceRegontision.StopRecordButtonOnClickHandler();
       _uiController.OnPlayVoice();
       if(_doubleVoicePlayble.isSure == true)
       {
           _doubleUiController._sorryAudio.Play();
       }
       else
        {
            AudioTask[AudioCount].Play();
        }
      
       StartCoroutine(StartRecord());
    } 
    
     public IEnumerator InterlocutorSay()
    {
        yield return new WaitForSeconds(3);
        AudioInterlocutor[AudioCount].Play();
        _uiController.InterLocutorSaid();
        StartCoroutine(OnClickButton());
        _uiController.SetTask(_voiceRegontision.Task[AudioCount]);
        _doubleUiController._doublePanel.SetActive(false);
        _uiController._microphonePanel.SetActive(false);
    }
    
    private IEnumerator OnClickButton()
    {
        yield return new WaitForSeconds(AudioInterlocutor[AudioCount].clip.length + 1);
        OnClickPlayButton();
    }
    
    private IEnumerator StartRecord()
    {
       yield return new WaitForSeconds(AudioTask[AudioCount].clip.length);
       _voiceRegontision.StartRecordButtonOnClickHandler();
       _uiController.SpeakUI();
    }
}
