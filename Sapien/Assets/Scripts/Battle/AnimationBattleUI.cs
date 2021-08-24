using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationBattleUI : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyStats;
    [SerializeField] private Transform _characterStats;
    [SerializeField] private Transform _crystallStats;


   private void Start()
   {
       
   }

    public void OpenStatsUI()
    {
        for(int i = 0; i < _enemyStats.Length; i++)
        {
            _enemyStats[i].transform.DOMoveX(1350, (0.7f + i/1.7f));
        }
        _characterStats.DOMoveX(100, 0.7f);
    }

    public void OpenHammerModStats()
    {
        _crystallStats.DOMoveX(1350, 0.7f);
        _characterStats.DOMoveX(100, 0.7f);
    }

    public void OpenInfenetlyModStats()
    {
        _characterStats.DOMoveX(100, 0.7f);
        _enemyStats[0].transform.DOMoveX(1350, 0.7f);
        
    }
}
