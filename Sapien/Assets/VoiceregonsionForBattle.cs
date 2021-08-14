using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace FrostweepGames.Plugins.GoogleCloud.SpeechRecognition
{
public class VoiceregonsionForBattle : MonoBehaviour
{
        public GCSpeechRecognition _speechRecognition;
		[SerializeField] private BattleController _battleController;
	    [SerializeField] private UIController _uiController;
		[SerializeField] private HammerBattle _hammerBattle;

		[SerializeField] private UIAnimation _uiAnimmation;
		[SerializeField] private SpeakWithCard _speakWithCard;
		[SerializeField] private SpeakWithVariant _speakWithVariant;
		[SerializeField] private DoneAndMissed _doneAndMissed;

		

		private string _resultText;
		public string Task;
		public string[] TaskNotInfently;
		
		//public Text responce;
		public Image _voiceLevelImage;
		public float max, current;

		
	    
		




		public void changeText(string Task)
		{
			
		
		}
		private void Start()
		{
			
		    
            Task = _battleController.VoiceTask;
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
					max = (float)_speechRecognition.configs[_speechRecognition.currentConfigIndex].voiceDetectionThreshold;
					current = _speechRecognition.GetLastFrame() / max;

					if (current >= 1f)
					{
						_voiceLevelImage.fillAmount = Mathf.Lerp(_voiceLevelImage.fillAmount, Mathf.Clamp(current / 2f, 0, 1f), 30 * Time.deltaTime);
					}
					else
					{
						_voiceLevelImage.fillAmount = Mathf.Lerp(_voiceLevelImage.fillAmount, Mathf.Clamp(current / 2f, 0, 0.5f), 30 * Time.deltaTime);
					}

					_voiceLevelImage.color = current >= 1f ? Color.green : Color.red;
				}
			}
			else
			{
				_voiceLevelImage.fillAmount = 0f;
			}

			/*if(_resultText != null)
			{
				SravnTask(_resultText);
			}*/
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
		public IEnumerator StopRecordAuthomatic()
		{
			
			while (true)
			{
				yield return new WaitForSeconds(1f);
				if (current < 0.11f)
				{
					yield return new WaitForSeconds(1f);
					if (current < 0.11f)
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
			yield return new WaitForSeconds(2);
			StartRecordButtonOnClickHandler(); 
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
			_battleController.TimeGo = false;
			  //Debug.Log(other);


				if ((other.Contains(Task) && _battleController.infinitely) || (other.Contains(TaskNotInfently[_battleController.RandomString]) && !_battleController.infinitely))
				{
					
                    StartCoroutine(_uiController.OnCorrect());
					_battleController.CrashGem(25);
					_battleController.LerningModeId++;
				}
				else if(_speakWithCard.IsSpeakWithCard == true &&  other.Contains(_speakWithCard.Task))
				{
                    _doneAndMissed.ScaleGood(1,280);
					StartCoroutine(_hammerBattle.CloseType());
					_hammerBattle.IsAttack = true;
					_hammerBattle._IsTimeGo = false;
				}
				else if(_speakWithCard.IsSpeakWithCard == true && !other.Contains(_speakWithCard.Task))
				{
					_doneAndMissed.ScaleMissed(1, 280);
					_hammerBattle._type[_hammerBattle.index].SetActive(false);
                    StartCoroutine(_hammerBattle.ClosePanelIfMissed());
					_hammerBattle._IsTimeGo = false;
				}
				else if(_speakWithVariant.IsSpeakWithVariants == true && other.Contains(_speakWithVariant._correctTask))
				{ 
					_doneAndMissed.ScaleGood(1,280);
					StartCoroutine(_hammerBattle.CloseType());
					_hammerBattle.IsAttack = true;
					_hammerBattle._IsTimeGo = false;
				}
				else if(_speakWithVariant.IsSpeakWithVariants == true && !other.Contains(_speakWithVariant._correctTask))
				{
					_doneAndMissed.ScaleMissed(1,280);
					_hammerBattle._type[_hammerBattle.index].SetActive(false);
                    StartCoroutine(_hammerBattle.ClosePanelIfMissed());
					_hammerBattle._IsTimeGo = false;
				}
				else
				{
					
					_uiController.IncorrectUI();
					_battleController.TimeGo = false;
					if(_battleController.infinitely)
					{
                        StartCoroutine(_battleController.Repeat());
					}
					else
					{
						
						StartCoroutine(_battleController.ClosePanel());
					}
					
				} 
			_speakWithVariant.IsSpeakWithVariants = false;
			_speakWithCard.IsSpeakWithCard = false;
			
		}


		
}
}