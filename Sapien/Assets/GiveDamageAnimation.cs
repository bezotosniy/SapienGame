using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class GiveDamageAnimation : MonoBehaviour
{
       [SerializeField] private Text _text; 
       [SerializeField] private BattleController _battleController;

   public void GiveDamage(){
       _text.text = _battleController.damage.ToString();
       _text.DOFade(1, 0.6f);
       _text.transform.DOMove(new Vector3(-33.356f,8.017f,-85.517f), 0.5f);
       StartCoroutine(FadeText());
   }


   private IEnumerator FadeText(){
       yield return new WaitForSeconds(1.5f);
       _text.DOFade(0, 0.5f);
       _text.transform.DOMove(new Vector3(-33.356f,8.317f,-85.517f), 0.5f);
   }
}
