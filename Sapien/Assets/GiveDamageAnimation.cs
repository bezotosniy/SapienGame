using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class GiveDamageAnimation : MonoBehaviour
{
       //[SerializeField] private Text _text; 
       [SerializeField] private BattleController _battleController;

   public void GiveDamage(Text text)
   {
       text.text = _battleController.CurrentUron.ToString();
       text.DOFade(1, 0.6f);
       text.transform.DOMove(new Vector3(-33.356f,8.017f,-85.517f), 0.5f);
       StartCoroutine(FadeText(text));
   }


   private IEnumerator FadeText(Text text){
       yield return new WaitForSeconds(1.5f);
       text.DOFade(0, 0.5f);
       text.transform.DOMove(new Vector3(-33.356f,8.317f,-85.517f), 0.5f);
   }
}
