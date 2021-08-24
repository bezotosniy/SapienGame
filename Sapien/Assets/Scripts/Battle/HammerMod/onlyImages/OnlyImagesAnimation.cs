using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OnlyImagesAnimation : MonoBehaviour
{
    [SerializeField] private Transform[] _dialogsItem;
   [SerializeField] private Transform _questionMark;
   [SerializeField] private AudioSource[] _audio;
   [SerializeField] private Sound _questionMarkSound;

     [Header("Dialog")]
   [SerializeField] private string[] _replicasText;
   [SerializeField] private GameObject[] _replica;
   private int _randomAvatar;


   [Header("RepeatPanel")]
   [Space(20f)]
   [SerializeField] private Text _repeatCount;
   [SerializeField] private GameObject _repeatButton;

   [SerializeField] private GameObject _repeatPanel;

   [Header("Inheratence")]
   [Space(20f)]
   [SerializeField] private Answer _answers;
   
   private void Start()
   {
      StartCoroutine(OpenDialog());
      _randomAvatar = Random.Range(1, _answers.Avatars.Length);
      CreateDialog(_replicasText);
   }


   private IEnumerator OpenDialog()
   {
       for(int i = 0; i < _dialogsItem.Length; i++)
       {
            yield return new WaitForSeconds(1);
           
           _dialogsItem[i].DOScale(new Vector3(1,1,1), 0.4f);
           if(i % 2 == 0)
           {
                _dialogsItem[i].DOMoveX(880, 0.6f);
                yield return new WaitForSeconds(0.7f);
                if(i < _dialogsItem.Length - 1)
                {
                    _audio[i].Play();
                }
                
           }
           else
           {
               _dialogsItem[i].DOMoveX(1100, 0.6f);
                yield return new WaitForSeconds(0.7f);
                if(i < _dialogsItem.Length - 1)
                {
                    _audio[i].Play();
                }
           }
       }
       yield return new WaitForSeconds(0.4f);
       _repeatPanel.SetActive(true);
       StartCoroutine(QuestionMark());
       _questionMarkSound.PlaySound();
    }

    private IEnumerator QuestionMark()
    {
        yield return new WaitForSeconds(0.4f);
        _questionMark.DORotate(new Vector3(0, 0, 45), 0.3f);
        StartCoroutine(QuestionMarkSecond());
    }

    private IEnumerator QuestionMarkSecond()
    {
        yield return new WaitForSeconds(0.4f);
        _questionMark.DORotate(new Vector3(0, 0, -45), 0.3f);
        StartCoroutine(QuestionMark());
    }

    public void OnClickRepeatButton()
    {
        _repeatPanel.SetActive(false);
        _repeatButton.SetActive(false);
        _repeatCount.enabled = false;
        StartCoroutine(RepeatDialog());
    }


    private IEnumerator RepeatDialog()
    {
        yield return new WaitForSeconds(0.4f);
        for(int i = 0; i < _audio.Length; i++)
        {
            _audio[i].Play();
            yield return new WaitForSeconds(_audio[i].clip.length + 0.3f);
        }
    }

      private void CreateDialog(string[] dialog)
    {
          for(int i = 0; i < _replica.Length; i++)
        {
            if(i < _replica.Length - 1)
            {
                _replica[i].GetComponent<Replica>().Words.text = dialog[i];
            }
            
           
            
               if(i % 2 != 0)
               {
                   _replica[i].GetComponent<Replica>().Avatar.sprite =  _answers.Avatars[_randomAvatar];
               }
               else
               {
                   _replica[i].GetComponent<Replica>().Avatar.sprite = _answers.Avatars[_randomAvatar - 1];
               }
            
          

           
        }
    }
}
