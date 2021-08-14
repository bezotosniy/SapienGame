using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
	[SerializeField] private GCSpeechRecognition _speechRecognition;

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

    [Header("DoubleTask")]
	[Space(10f)]
	public  int Counter;
	public int CounterNeed; 

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
		

			_speechRecognition = GCSpeechRecognition.Instance;
			_speechRecognition.RecognizeSuccessEvent += RecognizeSuccessEventHandler;
			_speechRecognition.LongRunningRecognizeSuccessEvent += LongRunningRecognizeSuccessEventHandler;
			_speechRecognition.FinishedRecordEvent += FinishedRecordEventHandler;
			_speechRecognition.StartedRecordEvent += StartedRecordEventHandler;
			_speechRecognition.RecordFailedEvent += RecordFailedEventHandler;

			_speechRecognition.BeginTalkigEvent += BeginTalkigEventHandler;
			_speechRecognition.EndTalkigEvent += EndTalkigEventHandler;

			
			


			
			


			_speechRecognition.SetMicrophoneDevice(_speechRecognition.GetMicrophoneDevices()[PlayerPrefs.GetInt("SavedMic")]);


		}

		private void Update()
		{
			if (_speechRecognition.IsRecording)
			{
				if (_speechRecognition.GetMaxFrame() > 0)
				{
					_max = (float)_speechRecognition.configs[_speechRecognition.currentConfigIndex].voiceDetectionThreshold;
					_current = _speechRecognition.GetLastFrame() / _max;

					if (_current >= 1f)
					{
						_voiceLevelImage.fillAmount = Mathf.Lerp(_voiceLevelImage.fillAmount, Mathf.Clamp(_current / 2f, 0, 1f), 30 * Time.deltaTime);
					}
					else
					{
						_voiceLevelImage.fillAmount = Mathf.Lerp(_voiceLevelImage.fillAmount, Mathf.Clamp(_current / 2f, 0, 0.5f), 30 * Time.deltaTime);
					}

					_voiceLevelImage.color = _current >= 1f ? Color.green : Color.red;
				}
			}
			else
			{
				_voiceLevelImage.fillAmount = 0f;
			}
		}



		public void StartRecordButtonOnClickHandler()
		{
		
		

			_resultText = string.Empty;
			StartCoroutine(StopRecordAuthomatic());
			_speechRecognition.StartRecord(false);
			Debug.Log("Speak");
			
		
			
		}

		public void StopRecordButtonOnClickHandler()
		{
			
			
			
			StartCoroutine(StopRecordAuthomatic());
			_speechRecognition.StopRecord();

		}


		public void StopRecord()
		{
			_speechRecognition.StopRecord();
		}
		public IEnumerator StopRecordAuthomatic()
		{
			while (true)
			{
				yield return new WaitForSeconds(1f);
				if (_current < _maxCurrent)
				{
					yield return new WaitForSeconds(1f);
					if (_current < _maxCurrent)
					{
						
					
						
						_speechRecognition.StopRecord();
						Debug.Log("StopRecord");

						
						
						yield break;
					}
				}
			}
		}

		private void StartedRecordEventHandler()
		{

		}

		private void RecordFailedEventHandler()
		{
			
			
		}

		private void BeginTalkigEventHandler()
		{

		}

		private void EndTalkigEventHandler(AudioClip clip, float[] raw)
		{
			FinishedRecordEventHandler(clip, raw);
		}

		private void FinishedRecordEventHandler(AudioClip clip, float[] raw)
		{

			if (clip == null)
				return;
			string Phrases = "phrase, phrase2, phrase3";
			RecognitionConfig config = RecognitionConfig.GetDefault();
			config.languageCode = (Enumerators.LanguageCode.en_US.Parse());
			config.speechContexts = new SpeechContext[]
			{
				new SpeechContext()
				{
					phrases = Phrases.Replace(" ", string.Empty).Split(',')
				}
			};
			config.audioChannelCount = clip.channels;
			// configure other parameters of the config if need

			GeneralRecognitionRequest recognitionRequest = new GeneralRecognitionRequest()
			{
				audio = new RecognitionAudioContent()
				{
					content = raw.ToBase64()
				},
				//audio = new RecognitionAudioUri() // for Google Cloud Storage object
				//{
				//	uri = "gs://bucketName/object_name"
				//},
				config = config
			};

			_speechRecognition.Recognize(recognitionRequest);

		}


		private void RecognizeSuccessEventHandler(RecognitionResponse recognitionResponse)
		{

			InsertRecognitionResponseInfo(recognitionResponse);
		}

		private void LongRunningRecognizeSuccessEventHandler(Operation operation)
		{
			if (operation.error != null || !string.IsNullOrEmpty(operation.error.message))
				return;



			if (operation != null && operation.response != null && operation.response.results.Length > 0)
			{

				_resultText += "\n" + operation.response.results[0].alternatives[0].transcript;

				string other = "\nDetected alternatives:\n";

				foreach (var result in operation.response.results)
				{
					foreach (var alternative in result.alternatives)
					{
						if (operation.response.results[0].alternatives[0] != alternative)
						{
							other += alternative.transcript + ", ";
						}
					}
				}

				_resultText += other;
			}
			else
			{
				_resultText = "Long Running Recognize Success. Words not detected.";
			}
		}
		IEnumerator Repeat()
        {
			
		    _uiController.RepeatUI();
			if(MistakeCounter == 2 && _voicePlaybackDouble.IsDoublePlayingNow == false)
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
		public void InsertRecognitionResponseInfo(RecognitionResponse recognitionResponse)
		{
			if (recognitionResponse == null || recognitionResponse.results.Length == 0)
			{
				StartCoroutine(Repeat());
				return;
			}

			_resultText += "\n" + recognitionResponse.results[0].alternatives[0].transcript;

			var words = recognitionResponse.results[0].alternatives[0].words;

			if (words != null)
			{
				string times = string.Empty;

				foreach (var item in recognitionResponse.results[0].alternatives[0].words)
				{
					times += "<color=green>" + item.word + "</color> -  start: " + item.startTime + "; end: " + item.endTime + "\n";
				}

				_resultText += "\n" + times;
			}

			string other = "\nDetected alternatives: ";

			foreach (var result in recognitionResponse.results)
			{
				foreach (var alternative in result.alternatives)
				{
					if (recognitionResponse.results[0].alternatives[0] != alternative)
					{
						other += alternative.transcript + ", ";
					}
				}
			}

			_resultText += other;
			
			SravnTask(_resultText + other);
			//_resultText.text += other;
		}
		public void SravnTask(string other)
		{
			if(other.Contains(Task[_voicePlayback.AudioCount]))
			{
				comboCount++;
				
				SetComboAndBest();
				_voicePlayback.AudioCount++;
				Counter++;
				MistakeCounter = 0;
				if(Counter == CounterNeed )
				{
				 StartCoroutine(_uiController.OnCorrect());
				  StartCoroutine(_voicePlaybackDouble.ListenInterlocutor());
				}
				else
				{
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


