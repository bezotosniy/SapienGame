using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FrostweepGames.Plugins.GoogleCloud.StreamingSpeechRecognition;
using System;

namespace FrostweepGames.Plugins.GoogleCloud.SpeechRecognition
{
public class VoiceRegontion2 : MonoBehaviour
{
	//ALARM
	public int RecognitionSuccessful;
	
	[Header("Inheritance")]
	[SerializeField] private UIControllerForCurScene _uiController;
    [SerializeField] private VoicePlayBack _voicePlayback;
    [SerializeField] private DoubleDialogUI _doubleUiController;
	[SerializeField] private VoicePlaybleDouble _voicePlaybackDouble;
	[SerializeField] private GCStreamingSpeechRecognition _speechRecognition;
	[SerializeField] private TaskWithFragmentCard _fragmentcardTask; 


	[Header("Task(YOU CAN CHANGE)")]
	[Space(10f)]
	public string[] Task;
	public string[] DoubleTask;
	public string Sure;

	[Header("VoiceRecognision")]
	[Space(10f)]
	
	
	[SerializeField] private Image _voiceLevelImage;
	[SerializeField] private float _max; 
	[SerializeField] private float _current;
	[SerializeField] private float _maxCurrent;
	private string _resultText;
	public int MistakeCounter; 
	[SerializeField] private Dropdown _languageDropdown;

    [Header("DoubleTask")]
	[Space(10f)]
	public  int Counter;
	public int CounterNeed; 
	public int CounterNeedForFragmentCardTask;
	[SerializeField] private GameObject _fragmentCardPanel;

	[Header("Combo and Best")]
	[Space(10f)]
	[SerializeField] private Text _comboText;
	[SerializeField] private Text _bestText;
	public int comboCount;
	private int bestCount;
       

		
		private void Start()
		{
			comboCount = PlayerPrefs.GetInt("combo", comboCount);
		    bestCount = PlayerPrefs.GetInt("best", bestCount);
		    SetComboAndBest();
			_uiController.SetTask(Task[_voicePlayback.AudioCount]);
		

			
			_speechRecognition = GCStreamingSpeechRecognition.Instance;
			_speechRecognition.StreamingRecognitionStartedEvent += StreamingRecognitionStartedEventHandler;
			_speechRecognition.StreamingRecognitionFailedEvent += StreamingRecognitionFailedEventHandler;
			_speechRecognition.InterimResultDetectedEvent += InterimResultDetectedEventHandler;
			_speechRecognition.FinalResultDetectedEvent += FinalResultDetectedEventHandler;

            for (int i = 0; i < Enum.GetNames(typeof(Enumerators.LanguageCode)).Length; i++)
			{
				_languageDropdown.options.Add(new Dropdown.OptionData(((Enumerators.LanguageCode)i).Parse()));
			}
			_languageDropdown.value = _languageDropdown.options.IndexOf(_languageDropdown.options.Find(x => x.text == Enumerators.LanguageCode.en_GB.Parse()));
	        
			_speechRecognition.SetMicrophoneDevice(_speechRecognition.GetMicrophoneDevices()[PlayerPrefs.GetInt("SavedMic")]);
		}


			private void OnDestroy()
		{
			_speechRecognition.StreamingRecognitionStartedEvent -= StreamingRecognitionStartedEventHandler;
			_speechRecognition.StreamingRecognitionFailedEvent -= StreamingRecognitionFailedEventHandler;
			_speechRecognition.StreamingRecognitionEndedEvent -= StreamingRecognitionEndedEventHandler;
			_speechRecognition.InterimResultDetectedEvent -= InterimResultDetectedEventHandler;
			_speechRecognition.FinalResultDetectedEvent -= FinalResultDetectedEventHandler;
		}

	

		private void StreamingRecognitionStartedEventHandler()
		{
			
		}

		private void StreamingRecognitionFailedEventHandler(string error)
		{
		
		}

		private void StreamingRecognitionEndedEventHandler()
        {
		}





		public void StartRecordButtonOnClickHandler()
		{
		
		

			_resultText = string.Empty;
			_speechRecognition.StartStreamingRecognition((Enumerators.LanguageCode)_languageDropdown.value, null);
			Debug.Log("Speak");
			
		
			
		}

	    public async void StopRecordButtonOnClickHandler()
		{
			await _speechRecognition.StopStreamingRecognition();
			Debug.Log("Stop Record");
		}

        private void InterimResultDetectedEventHandler(string alternative)
        {
			_resultText += $"<b>Alternative:</b> {alternative}\n";
           Debug.Log(alternative);
			
		}

		private void FinalResultDetectedEventHandler(string alternative)
		{
			_resultText += $"<b>Final:</b> {alternative}\n";
			

			Debug.Log(alternative);
			CompareTask(alternative);
		}
        IEnumerator Repeat()
        {
			
		    _uiController.RepeatUI();
			if(MistakeCounter == 2 && Counter == CounterNeedForFragmentCardTask)
			{
			   StopRecordButtonOnClickHandler();
			   _voicePlayback.IsMistake = true;
			   StartCoroutine(_fragmentcardTask.InCorrect());
			}
			else if(MistakeCounter == 2 && _voicePlaybackDouble.IsDoublePlayingNow == false)
			{
				StopRecordButtonOnClickHandler();
				_uiController.IncorrectUI();
			    _voicePlayback.IsMistake = true;
				

			}
			else if(MistakeCounter == 2 && _voicePlaybackDouble.IsDoublePlayingNow == true)
			{
				StopRecordButtonOnClickHandler();
				_voicePlayback.IsMistake = true;
				_doubleUiController.InCorrect();
				
			}
			else
			{
               yield return new WaitForSeconds(2);
			   
			  StartRecordButtonOnClickHandler(); 
			}
			
		}
		
		public void CompareTask(string other)
		{
			StopRecordButtonOnClickHandler();
			if(other.Contains(Task[_voicePlayback.AudioCount]))
			{
				comboCount++;
				SetComboAndBest();
				_voicePlayback.AudioCount++;
				Counter++;
				MistakeCounter = 0;

				if(Counter == CounterNeed )
				{
				 Debug.Log("CounterNeedDouble");
				 StartCoroutine(_uiController.OnCorrect());
				 StartCoroutine(_fragmentcardTask.OnCorrect());
				  StartCoroutine(_voicePlaybackDouble.ListenInterlocutor());
				}
				else if(Counter == CounterNeedForFragmentCardTask)
				{
					Debug.Log("CounterNeedFragmentCard");
					StartCoroutine(_uiController.OnCorrect());
					StartCoroutine(_fragmentcardTask.SpawnTask());
                    
				}
			    else if(Counter == CounterNeedForFragmentCardTask + 1)
				{
					Debug.Log("OnCorrect");
                    StartCoroutine(_fragmentcardTask.OnCorrect());
					StartCoroutine(_voicePlayback.InterlocutorSay());
				}
				else
				{
					Debug.Log("Else");
				   StartCoroutine(_uiController.OnCorrect());
				   StartCoroutine(_voicePlayback.InterlocutorSay());
                }
			}
			else if(other.Contains(DoubleTask[_voicePlaybackDouble.index]) && _voicePlaybackDouble.IsDoublePlayingNow == true)
			{
				
				_voicePlaybackDouble.ListenToTryAgain();
				_uiController._microphonePanel.SetActive(false);
				_doubleUiController._doublePanel.SetActive(false);
				_voicePlaybackDouble.isSure = true;
				comboCount = 0;
				SetComboAndBest();
				MistakeCounter = 0;
			}
			else if((other.Contains(DoubleTask[_voicePlaybackDouble.index + 1])) && _voicePlaybackDouble.IsDoublePlayingNow == true)
			{
                _doubleUiController.OnCorrectDouble();
				StartCoroutine(_voicePlayback.InterlocutorSay());
				_voicePlaybackDouble.IsDoublePlayingNow = false;
				comboCount++;
				SetComboAndBest();
				_voicePlayback.AudioCount++;
				
			}
			else if((!other.Contains(DoubleTask[_voicePlaybackDouble.index]) && _voicePlaybackDouble.IsDoublePlayingNow == true) || ((!other.Contains(DoubleTask[_voicePlaybackDouble.index + 1])) && _voicePlaybackDouble.IsDoublePlayingNow == true))
			{
				MistakeCounter++;
                StartCoroutine(Repeat());
				comboCount = 0;
				SetComboAndBest();
			}
			else if(_voicePlaybackDouble.isSure == true && other.Contains(Sure))
			{
				StartCoroutine(_uiController.OnCorrect());
				StartCoroutine(_voicePlaybackDouble.ListenInterlocutor());
				_voicePlaybackDouble.isSure = false;
				MistakeCounter = 0;
				comboCount++;
				SetComboAndBest();
			}
			else if(_voicePlaybackDouble.isSure == true && !other.Contains(Sure))
			{
				MistakeCounter++;
                StartCoroutine(Repeat());
				comboCount = 0;
				SetComboAndBest();
			}
			else 
			{
                MistakeCounter++;
                StartCoroutine(Repeat());
				comboCount = 0;
				SetComboAndBest();
			
			}


			
			
		}
			
			
		


		public void SetComboAndBest()
        {
        _comboText.text = comboCount.ToString();
		PlayerPrefs.SetInt("combo", comboCount);
		if(comboCount > bestCount)
		{
           bestCount = comboCount;
		}
		PlayerPrefs.SetInt("best", bestCount);
	    _bestText.text = bestCount.ToString();
        }



	
		
	}
}


