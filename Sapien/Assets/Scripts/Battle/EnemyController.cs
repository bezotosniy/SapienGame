using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using DG.Tweening;
public class EnemyController : MonoBehaviour
{
    public int HPbat = 90;
    public Text enemyText;
    public Slider hpSlider;
    [SerializeField] private BattleController inv;
    public Animator EnemyAnim;
    public Transform PointShoot;
    Vector3 StartPosEnemy;
    [Range(0.001f, 1)]
    public float speedMouse;
    
     public bool IsAttack;

     [SerializeField] private ParticleSystem _blood;
     [SerializeField] private Camera _camera;
       private Ray RayMouse;
    [SerializeField] private MovingBattlePeron _movingPerson;
    [SerializeField] private EnemiesController _enemiesController;
    public Text _damageText;
    public bool IsDied;
    public Canvas _canvas;
     void Start()
    {
        
        StartPosEnemy = transform.position;
        hpSlider.maxValue = HPbat;
        hpSlider.value = HPbat;
        enemyText.text = hpSlider.value.ToString() + "/" + hpSlider.maxValue.ToString();

    }


    public void OnBombClick()
    {
      hpSlider.value = 0;
      HPbat = 0;
      enemyText.text = hpSlider.value.ToString() + "/" + hpSlider.maxValue.ToString();                      
    }



  
    // Update is called once per frame
    void Update()
    {
        HPbat = (int)hpSlider.value;
        enemyText.text = hpSlider.value.ToString() + "/" + hpSlider.maxValue.ToString();

        if(HPbat <= 0)
        {
            IsDied = true;
            EnemyAnim.SetTrigger("die");
            _canvas.enabled = false;
        }

    }

  
    

      


   
}
