using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnlyImages : MonoBehaviour
{
 

   
   [SerializeField] private Image[] _images;
   [SerializeField] private Image _correctImage;


   [Header("Ticks")]
   [Space(20f)]
   [SerializeField] private Image[] _boxes;
   private int _currentIndex;
   private int _correctAnswer;

   [Header("Inheratence")]
   [Space(20f)]
   [SerializeField] private Answer _answers;
   [SerializeField] private DoneAndMissed _doneAndMissed;
    [SerializeField] private HammerBattle _hammerBattle;

   
   [Header("Other")]
   [Space(20f)]
   [SerializeField] private Button _done;

  
   private void Start()
   {
        _hammerBattle._IsTimeGo = true;
        StartCoroutine(_hammerBattle.TimeGo());

       _correctAnswer = Random.Range(0,3);

       for(int i = 0; i < _images.Length; i++)
       {
           _images[i].sprite = _answers.AnswersImage[i].sprite;
       }
       _images[_correctAnswer].sprite = _correctImage.sprite;

       _done.enabled = false;
       _done.onClick.AddListener(CheckAnswer);


   }

   public void OnClickTick(int index)
   {
        foreach(Image t in _boxes)
        {
            t.enabled = false;
        }   
        _boxes[index].enabled = true;
        _currentIndex = index;
        _done.enabled = true;
   }



   private void CheckAnswer()
   {
       _done.enabled = false;
       if(_currentIndex == _correctAnswer)
       {
           Debug.Log("Correct");
           _doneAndMissed.ScaleGood(1, 280);
           StartCoroutine(_hammerBattle.CloseType());
           _hammerBattle.IsAttack = true;
       }
       else
       {
           Debug.Log("Incorrect");
           _doneAndMissed.ScaleMissed(1, 280);
           StartCoroutine(_hammerBattle.ClosePanelIfMissed());
            //gameObject.SetActive(false);
       }
   }
}
