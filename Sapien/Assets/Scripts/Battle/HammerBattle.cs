using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition.Examples;

public class HammerBattle : MonoBehaviour
{
   
    
    

    [Header("Battle")]
    [Space(20f)]
    [SerializeField] private Camera _camera;
    private Ray _ray;
    public bool IsAttack;
    [SerializeField] private GameObject StartParticle; 
    [SerializeField] private GameObject _background;
    [SerializeField] private GameObject _uron; 
    [SerializeField] private GameObject _destroyBullet;
    [SerializeField] private Transform _instantiateParticle;
    [SerializeField] private  Transform _enemy;
    [SerializeField] private Animator _animator;
    public int index;


    [Header("Inheratence")]
    [SerializeField] private BattleController _battleController;
    [SerializeField] private UIAnimation _animation;
    [SerializeField] private MovingBattlePeron _person;
    [SerializeField] private AnimationBattleUI _animationStats;
    

    [Header("Time")]
    [Space(20f)]
    [SerializeField] private Text _timeText;
    public bool _IsTimeGo;


    [Header("Crystall")]
    [Space(20f)]
    [SerializeField] private int _damage;
    [SerializeField] private int _crystallHP;
    [SerializeField] private Text _hp;
    [SerializeField] private Slider _slider;
    [SerializeField] private int _hpControl;
    [SerializeField] private GameObject _hpPanel;
    [SerializeField] private GameObject _enemyPanel;
    [SerializeField] private SpeakWithCard _speakWithCard;
    [SerializeField] private SpeakWithVariant _speakWithVariant; 
    [SerializeField] private VoiceregonsionForBattle _voiceRecognision;

    public int[] _randomTask;

    [Header("Type Pabnels")]
    public GameObject[] _type;
    [SerializeField] private UIAnimation _uiAnimation;
    [SerializeField] private DoneAndMissed _doneAndMissed;
    [SerializeField] private Animation _openPanel;
    public Sound[] _sounds;
    [SerializeField] private Sound _whenBulletMoveSound;
    [SerializeField] private Sound  _damageEnemy;
    [SerializeField] private Sound  _explotionBullet;

    [Header("Finish Game")]
    [SerializeField] private Animator _cameraAnimator;
    [SerializeField] private Transform _player;
 




    
    

    

    private void Start()
    {
        _hp.text = _crystallHP + "/" + _hpControl;
        _enemyPanel.SetActive(false);
        _hpPanel.SetActive(true);
        index = 0;
        CreateRandom();
        _animationStats.OpenHammerModStats();
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

     void FixedUpdate()
    {
       
        if (Input.GetButtonDown("Fire1") && IsAttack == true)
        {
            
                RaycastHit hit;
                var mousePos = Input.mousePosition;
                _ray = _camera.ScreenPointToRay(mousePos);

                if (Physics.Raycast(_ray.origin, _ray.direction, out hit, 40))
                {
                    if (hit.collider.tag == "Enemy")
                    {   
                        StartCoroutine(ClickOnEnemy(hit.collider.gameObject.transform.position));
                    }
                }
            
            
        }
    }

    public IEnumerator ClosePanelIfMissed()
    {
        yield return new WaitForSeconds(2f);
        _sounds[0].PlaySound();
        _animation.ClosePanel();
        yield return new WaitForSeconds(2);
        _doneAndMissed.ScaleMissed(0,260);
        _openPanel.Play();
        _sounds[1].PlaySound();
        StartCoroutine(StartRecord());
       
    }

    private IEnumerator StartRecord()
    {
        if(_randomTask[index] == 3 || _randomTask[index] == 4)
        {
            _speakWithCard.IsSpeakWithCard = true; 
            _speakWithVariant.IsSpeakWithVariants = true;
        }
        _speakWithCard.ChangeTextImage(false);
        _speakWithVariant.ChangeTextImage(false);
        yield return new WaitForSeconds(3f);
        if(_speakWithCard.IsSpeakWithCard == true || _speakWithVariant.IsSpeakWithVariants == true)
        {
            _speakWithCard.ChangeTextImage(true);
            _speakWithVariant.ChangeTextImage(true);
            _voiceRecognision.StartRecordButtonOnClickHandler();
        }
    }

    public IEnumerator OpenType()
    {
        yield return new WaitForSeconds(0.5f);
        if(index > 0)
        {
            _type[_randomTask[index  - 1]].SetActive(false);
        }
        _type[_randomTask[index]].SetActive(true);
    }

    public IEnumerator CloseType()
    {
        yield return new WaitForSeconds(2.5f);
        _sounds[0].PlaySound();
        _animation.ClosePanel();
        yield return new WaitForSeconds(3);
        _doneAndMissed.ScaleGood(0, 260);
       
    }

    
    
 IEnumerator MoveBullet(GameObject bullet, Vector3 point)
        {
            _battleController.BombDamageForhammerMod();
            _whenBulletMoveSound.PlaySound();
            while (true)
            {
            Vector3 quat = Vector3.RotateTowards( bullet.transform.forward, point - bullet.transform.position, 120 *Time.deltaTime,0.0f);
            bullet.transform.rotation = Quaternion.LookRotation(quat);
                bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, point,4.74f * Time.deltaTime);
                if (Vector3.Distance(bullet.transform.position, point) < 0.01f)
                {
                    _damageEnemy.PlaySound();
                    _explotionBullet.PlaySound();
                    _crystallHP -= _damage;
                    _hp.text = _crystallHP + "/" + _hpControl;
                    _slider.value -= _damage;
                    Instantiate(_destroyBullet,_enemy);
                    Destroy(bullet);
                    if(_crystallHP <= 0)
                    {
                        StartCoroutine(FinishGame());
                      
                    }
                    else
                    {
                        StartCoroutine(_battleController.WaitForOpenHammerPanel());
                        
                    }
                    yield break;
                    
                    
                }
                
                yield return new WaitForFixedUpdate();
                
            }
        }
         public IEnumerator ClickOnEnemy(Vector3 enemyPoint)
        {
            index++;
            GameObject bk = Instantiate(_background, transform);
            
            
            yield return new WaitForSeconds(1.5f);
            _animator.SetTrigger("IsAttack");
            GameObject bullet = Instantiate(_uron, _instantiateParticle);
         

            StartCoroutine(MoveBullet(bullet, enemyPoint));
            yield return new WaitForSeconds(2);
            Destroy(bk);
        }
  
  public IEnumerator TimeGo()
    {
        int allTime = 60;
        _IsTimeGo = true;
        while (true) {
            yield return new WaitForSeconds(1f);
            
            _timeText.text = allTime.ToString();
            allTime--;
            if(allTime < 0||!_IsTimeGo)
            {
                if (allTime < 0)
                {
                    _IsTimeGo = false;
                    _sounds[0].PlaySound();
                    _animation.ClosePanel();
                    _type[index].SetActive(false);
                    StartCoroutine(OpenType());
                   
                }
                yield break;
            }
            
        }
        

    }



    private IEnumerator FinishGame()
    {
       
            Debug.Log("Win");
            _animator.SetTrigger("Win");
            _cameraAnimator.SetTrigger("end");
            _battleController._sounds[8].PlaySound();
            yield return new WaitForSeconds(4);
            _battleController.GoHome.gameObject.SetActive(true);
            _battleController.GoHome.onClick.AddListener(_battleController.GoHomeVoid);
            _battleController.RESULT_TEXT.gameObject.SetActive(true);
            _battleController._sounds[9].PlaySound();
            _battleController.RESULT_TEXT.text = "WIN";
        
        
    }
  





   

   
  
    
}


