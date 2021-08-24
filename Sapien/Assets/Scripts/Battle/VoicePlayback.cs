using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;
public class VoicePlayback : MonoBehaviour
{

  
    public AudioSource speaker;
    public GameObject speak, wait, PlayBar, PauseBar;

    public bool isPaused = false;
    public bool isRecording = false;
    public bool RecordResult = false;


    public Image speakbg;
    public Image waitbg;
    public Image bg;

    public VoiceRegontion2 voiceRecognision;
    //public Animator TaxiAnim;

    //public Text result;

    void Start()
    {
        //StartCoroutine(HelloVoice());
        
    }

    void Update()
    {
        if (speaker.isPlaying)
        {
            PlayBar.SetActive(false);
            bg.sprite = waitbg.sprite;
            
            speak.SetActive(false);
            wait.SetActive(true);
            Debug.Log(speaker.clip.length);
        }
        else if (!speaker.isPlaying)
        {
           
            isPaused = false;
            isRecording = true;
            PauseBar.GetComponent<Image>().fillAmount = 0f;
            PlayBar.SetActive(true);
            PauseBar.SetActive(false);
            bg.sprite = speakbg.sprite; 
            speak.SetActive(true);
            wait.SetActive(false);
        }
        
      
    }

  

    public void HelloPlayback()
    {
       
        if (!speaker.isPlaying)
        {
           //OnSpeak();
            speaker.Play();
            isPaused = false;
        }
        else if (speaker.isPlaying)
        {
            
            speaker.Pause();
            isPaused = true;
           
        }
    }

    private IEnumerator StartVoicerecognition()
    {
        yield return new WaitForSeconds(1);
        //voiceRecognision.StartRecordButtonOnClickHandler();
    }



    private void OnSpeak()
    {
        PauseBar.SetActive(true);
        PauseBar.GetComponent<Image>().fillAmount += Time.deltaTime / speaker.clip.length;
       
        speak.SetActive(false);
        wait.SetActive(true);
        Debug.Log(speaker.clip.length);
    }
}
