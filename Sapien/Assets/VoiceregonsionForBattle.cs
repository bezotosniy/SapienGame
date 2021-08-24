using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using FrostweepGames.Plugins.GoogleCloud.StreamingSpeechRecognition;

namespace FrostweepGames.Plugins.GoogleCloud.SpeechRecognition.Examples
{
public class VoiceregonsionForBattle : MonoBehaviour
{
        public GCStreamingSpeechRecognition _speechRecognition;
		[SerializeField] private BattleController _battleController;
	    [SerializeField] private UIController _uiController;
		[SerializeField] private HammerBattle _hammerBattle;
		[SerializeField] private EnemiesController _enemiesController;
		[SerializeField] private UIAnimation _uiAnimmation;
		[SerializeField] private SpeakWithCard _speakWithCard;
		[SerializeField] private SpeakWithVariant _speakWithVariant;
		[SerializeField] private DoneAndMissed _doneAndMissed;
		[SerializeField] private Dropdown _languageDropdown;
		[SerializeField] private GameObject _dialogpanel;
		[SerializeField] private GameObject _microphonePanel;

		

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
			Debug.Log("Stop");
		}
          

	
		

		private void InterimResultDetectedEventHandler(string alternative)
        {
			_resultText += $"<b>Alternative:</b> {alternative}\n";
            //Debug.Log(alternative);
			
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
			yield return new WaitForSeconds(2);
			StartRecordButtonOnClickHandler(); 
		}
	
		public void CompareTask(string Result)
		{
			StopRecordButtonOnClickHandler();
			_battleController.TimeGo = false;
			  


				if ((Result.Contains(Task) && _battleController.infinitely) || (Result.Contains(TaskNotInfently[_battleController.RandomString]) && !_battleController.infinitely))
				{
					if(_battleController.IsBombTask)
					{
                        _enemiesController.CanAttack = true;
						StartCoroutine(_uiController.OnCorrect());
						_uiAnimmation.ClosePanel();
						_battleController.IsBombTask = false;
						
					}
					else
					{
                        StartCoroutine(_uiController.OnCorrect());
					    _battleController.CrashGem(_battleController._damagePerAnswer);
					    _battleController.LerningModeId++;
				        if(!_battleController.infinitely)
				        {
                            StartCoroutine(CloseDialogBackGround());
				        }
					}
                   
				}
				else if(_speakWithCard.IsSpeakWithCard == true &&  Result.Contains(_speakWithCard.Task))
				{
                    _doneAndMissed.ScaleGood(1,280);
					StartCoroutine(_hammerBattle.CloseType());
					_hammerBattle.IsAttack = true;
					_hammerBattle._IsTimeGo = false;
				}
				else if(_speakWithCard.IsSpeakWithCard == true && !Result.Contains(_speakWithCard.Task))
				{
					_doneAndMissed.ScaleMissed(1, 280);
					_hammerBattle._type[_hammerBattle.index].SetActive(false);
                    StartCoroutine(_hammerBattle.ClosePanelIfMissed());
					_hammerBattle._IsTimeGo = false;
				}
				else if(_speakWithVariant.IsSpeakWithVariants == true && Result.Contains(_speakWithVariant._correctTask))
				{ 
					_doneAndMissed.ScaleGood(1,280);
					StartCoroutine(_hammerBattle.CloseType());
					_hammerBattle.IsAttack = true;
					_hammerBattle._IsTimeGo = false;
				}
				else if(_speakWithVariant.IsSpeakWithVariants == true && !Result.Contains(_speakWithVariant._correctTask))
				{
					_doneAndMissed.ScaleMissed(1,280);
					_hammerBattle._type[_hammerBattle.index].SetActive(false);
                    StartCoroutine(_hammerBattle.ClosePanelIfMissed());
					_hammerBattle._IsTimeGo = false;
				}
				else
				{
					if(_battleController.IsBombTask)
					{
                        _uiAnimmation.ClosePanel();
						_battleController.StartFightPanel();
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
					
					
				} 
			_speakWithVariant.IsSpeakWithVariants = false;
			_speakWithCard.IsSpeakWithCard = false;
			
		}


		private IEnumerator CloseDialogBackGround()
		{
			yield return new WaitForSeconds(3);
            _dialogpanel.SetActive(false);
			_microphonePanel.SetActive(false);
		}


		
}
}