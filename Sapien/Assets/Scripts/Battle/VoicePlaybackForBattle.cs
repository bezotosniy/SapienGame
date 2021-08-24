using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition.Examples;

public class VoicePlaybackForBattle : MonoBehaviour
{
    [Header("Inheritance")]
  [SerializeField] private UIController _uiController;
  [SerializeField] private VoiceregonsionForBattle _voiceRegontision;
  [SerializeField] private BattleController _battleController;
  

  [Header("Audio(You can Change)")]
  [Space(10f)]
  public AudioSource[] AudioTask;
  public AudioSource[] AudioInterlocutor;

  [Header("Other")]
  [Space(10f)]
  public int AudioCount;
 



  
    
    public void OnClickPlayButton()
    {
        _voiceRegontision.StopRecordButtonOnClickHandler();
       _uiController.OnPlayVoice();
       
       if(_battleController.infinitely)
       {
           AudioTask[_battleController.RandomString].Play();
       }
       else
       {
           _battleController._dictorNotInfentlySaid[_battleController.RandomString].Play();
       }
    
       StartCoroutine(StartRecord());
    } 
    
     public IEnumerator InterlocutorSay()
    {
        yield return new WaitForSeconds(4);
        AudioInterlocutor[AudioCount].Play();
        _uiController.InterLocutorSaid();
        StartCoroutine(OnClickButton());
        _uiController.SetTask(_voiceRegontision.Task);
       
        _uiController._microphonePanel.SetActive(false);
    }
    
    private IEnumerator OnClickButton()
    {
        yield return new WaitForSeconds(AudioInterlocutor[AudioCount].clip.length + 1);
        OnClickPlayButton();
    }
    
    private IEnumerator StartRecord()
    {
       yield return new WaitForSeconds(AudioTask[_battleController.RandomString].clip.length);
       _voiceRegontision.StartRecordButtonOnClickHandler();
       _uiController.SpeakUI();
    }
}
