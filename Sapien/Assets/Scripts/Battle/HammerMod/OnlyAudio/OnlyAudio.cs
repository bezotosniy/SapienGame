using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnlyAudio : MonoBehaviour
{
   [Header("Answers")]
   [SerializeField] private AudioSource _correctAnswerAudio;

   [Header("Audio")]
   [SerializeField] private Text[] _audioCount;
   [SerializeField] private Button[] _playButton;


   [Header("Ticks")]
   [Space(20f)]
   [SerializeField] private Image[] _ticks;
   private int _currentIndex;
   public int _correctAnswer;

   [Header("Inheratence")]
   [Space(20f)]
   [SerializeField] private Answer _answers;
   [SerializeField] private DoneAndMissed _doneAndMissed;
   [SerializeField] private HammerBattle _hammerBattle;

   
   [Header("Other")]
   [Space(20f)]
   [SerializeField] private Button _done;
   [SerializeField] private Image[] _fill;
   [SerializeField] private int[] _randomTask;
   [SerializeField] private bool[] _isVariantPlay;
   private int _audioVariantCount;

  
   private void Start()
   {
       CreateRandom();
       _correctAnswer = Random.Range(0,3);

       _done.enabled = false;
       _done.onClick.AddListener(CheckAnswer);

        _hammerBattle._IsTimeGo = true;
       StartCoroutine(_hammerBattle.TimeGo());
   }

   private void Update()
   {
    if(_audioVariantCount < _fill.Length)
    {
        if(_isVariantPlay[_audioVariantCount])
        {
           if(_audioVariantCount != _correctAnswer)
           {
               _fill[_audioVariantCount].fillAmount += Time.deltaTime / _answers.AnswersAudio[_audioVariantCount].clip.length;
           }
           else
           {
               _fill[_audioVariantCount].fillAmount += Time.deltaTime / _correctAnswerAudio.clip.length;
           }
       }
   }
    
   }

    private void CreateRandom()
    {
        
        
        for (int i = _randomTask.Length - 1; i > 0; i--)
        {
            var r = new System.Random();
            int j = r.Next(i);
            var t = _randomTask[i];
            _randomTask[i] = _randomTask[j];
            _randomTask[j] = t;
        }
    }

   public void OnClickTick(int index)
   {
        foreach(Image t in _ticks)
        {
            t.enabled = false;
        }   
        _ticks[index].enabled = true;
        _currentIndex = index;
        _done.enabled = true;
   }


   public void OnClickPlay(int index)
   {
    if(index != _correctAnswer)
    {
        _answers.AnswersAudio[_randomTask[index]].Play();
    }
    else
    {
        _correctAnswerAudio.Play();
    }
    _audioCount[index].enabled = false;
    _playButton[index].GetComponent<Image>().enabled = false;
      
   }


   private void CheckAnswer()
   {
       _done.enabled = false;
       if(_currentIndex == _correctAnswer)
       {
           Debug.Log("Correct");
           _doneAndMissed.ScaleGood(1, 280);
           StartCoroutine(_hammerBattle.CloseType());
           _hammerBattle._IsTimeGo = false;
       }
       else
       {
           Debug.Log("Incorrect");
           _doneAndMissed.ScaleMissed(1, 280);
           StartCoroutine(_hammerBattle.ClosePanelIfMissed());
           _hammerBattle._IsTimeGo = false;
           ActivePlayButtons();
       }
   }


   public IEnumerator PlayAnswersVariant()
   {
     
        
       if(_audioVariantCount  < _fill.Length)
       {
            _isVariantPlay[_audioVariantCount] = true;
            if(_audioVariantCount != _correctAnswer)
            {
                _answers.AnswersAudio[_randomTask[_audioVariantCount]].Play();
                yield return new WaitForSeconds(_answers.AnswersAudio[_randomTask[_audioVariantCount]].clip.length);
            }
            else
            {
                _correctAnswerAudio.Play();
                yield return new WaitForSeconds(_correctAnswerAudio.clip.length);
            }
           
           _isVariantPlay[_audioVariantCount] = false;
           _fill[_audioVariantCount].enabled = false;
           /*if(_audioVariantCount < _fill.Length -1)
           {*/
                _audioVariantCount++;
           //}
          
           StartCoroutine(PlayAnswersVariant());
       }
       else
       {
           _isVariantPlay[_audioVariantCount - 1] = false;
           yield break;
       }
       yield return new WaitForSeconds(1);
      
       
        
   }


   private void ActivePlayButtons()
   {
       for(int i = 0; i < _playButton.Length; i++)
       {
           _playButton[i].GetComponent<Image>().enabled = true;
           _audioCount[i].enabled = true;
       }
   }


  
}
