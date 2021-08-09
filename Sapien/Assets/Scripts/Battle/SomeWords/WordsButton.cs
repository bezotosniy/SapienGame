using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordsButton : MonoBehaviour
{
  [SerializeField] private GameObject _questionMark;
  [SerializeField] private SomeWords _someWords;
   public Text Word;

   public void OnClick(int count)
   {
       StartCoroutine(_someWords.OnChooseVariant(count));
       //gameObject.SetActive(false);
   }
   
}
