using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HammerBattle : MonoBehaviour
{
    [Header("Dialog")]
    [Space(20f)]
    [SerializeField] private GameObject[] _replica;
    [SerializeField] private Sprite[] _avatars;
    [SerializeField] private string[] _words;
    [SerializeField] private int _randomAvatar;


    [Header("Answers")]
    [Space(20f)]
    [SerializeField] private AudioSource[] _answersAudio;
    [SerializeField] private AudioSource _correctAnswerAudio;
    [SerializeField] private Image[] _tick;
    [SerializeField] private Image[] _images;
    [SerializeField] private Image[] _playButtons;
    [SerializeField] private Sprite[] _answersimage;
    [SerializeField] private Sprite _correctImage;
    [SerializeField] private GameObject[] _box;
    [SerializeField] private int _correctAnswer;
    [SerializeField] private Button _done;
    private int _currentIndex;
    [SerializeField] private int _randomType;

    [Header("Battle")]
    [Space(20f)]
    [SerializeField] private Camera _camera;
    private Ray _ray;
    private bool _isAttack;
    [SerializeField] private GameObject StartParticle; 
    [SerializeField] private GameObject _background;
    [SerializeField] private GameObject _uron; 
    [SerializeField] private GameObject _destroyBullet;
    [SerializeField] private Transform _instantiateParticle;
    [SerializeField] private  Transform _enemy;
    [SerializeField] private Animator _animator;
    private int index;


    [Header("Inheratence")]
    [SerializeField] private BattleController _battleController;
    [SerializeField] private UIAnimation _animation;
    [SerializeField] private MovingBattlePeron _person;

    [Header("Time")]
    [Space(20f)]
    [SerializeField] private Text _timeText;
    private bool _IsTimeGo;


    [Header("Crystall")]
    [Space(20f)]
    [SerializeField] private int _damage;
    [SerializeField] private int _crystallHP;
    




    
    

    

    private void Start()
    {
        
        _done.onClick.AddListener(CheckAnswer);
        _done.GetComponent<Image>().enabled = false;
        _randomAvatar = Random.Range(1, _avatars.Length);

        for(int i = 0; i < _images.Length; i++)
        {
            _images[i].sprite = _answersimage[i];
        }

        for(int i = 0; i < _replica.Length; i++)
        {
            _replica[i].GetComponent<Replica>().Words.text = _words[i];

            if(i % 2 != 0)
            {
                _replica[i].GetComponent<Replica>().Avatar.sprite = _avatars[_randomAvatar];
            }
            else
            {
                _replica[i].GetComponent<Replica>().Avatar.sprite = _avatars[_randomAvatar - 1];
            }
        }
        
        StartCoroutine(DialogPlay());
    }

     void FixedUpdate()
    {
       
        if (Input.GetButtonDown("Fire1") && _isAttack == true)
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

    public IEnumerator DialogPlay()
    {
        
        _correctAnswer = Random.Range(0,3);
        _images[_correctAnswer].sprite = _correctImage;
        _randomType = Random.Range(0,3);
        ChoseType();
        if(index == 0)
        {
        yield return new WaitForSeconds(2.5f);
        TransformReplicas(880, 0);
        yield return new WaitForSeconds(1.6f);
        TransformReplicas(1060, 1);
        yield return new WaitForSeconds(1.6f);
        TransformReplicas(880, 2);
        yield return new WaitForSeconds(1.6f);
        TransformReplicas(1060, 3);
        yield return new WaitForSeconds(1.6f);
        TransformReplicas(880, 4);
        yield return new WaitForSeconds(0.5f);
        }
        StartCoroutine(TimeGo());
        index++;
        
    }


    public void OnClickTick(int index)
    {
        foreach(Image t in _tick)
        {
            t.enabled = false;
        }   
        _tick[index].enabled = true;
        _done.GetComponent<Image>().enabled = true;
        _currentIndex = index;
    }

    public void PlayAnswer(int index)
    {
        if(index != _correctAnswer)
        {
            _answersAudio[index].Play();
        }
        else
        {
            _correctAnswerAudio.Play();
        }
      
    }

    private void TransformReplicas(int position, int index)
    {
        _replica[index].transform.DOMoveX(position, 0.5f);
        _replica[index].transform.DOScale(new Vector3(1,1,1), 0.3f);
    }


    private void FirstType()
    {
        for(int  i = 0; i < _box.Length; i++)
        {
            _box[i].SetActive(true);
            _images[i].enabled = false;
            _playButtons[i].enabled = true;
        }
    }

    private void SecondType()
    {
          for(int  i = 0; i < _box.Length; i++)
        {
            _box[i].SetActive(true);
            _images[i].enabled = true;
            _playButtons[i].enabled = true;
        }
    }

       private void ThirdType()
    {
          for(int  i = 0; i < _box.Length; i++)
        {
            _box[i].SetActive(true);
            _images[i].enabled = true;
            _playButtons[i].enabled = false;
        }
    }


    private void CheckAnswer()
    {
            if(_currentIndex == _correctAnswer)
            {
                _isAttack = true;
                _animation.ClosePanel();
                DeleteTick();
                for(int i = 0; i < _images.Length; i++)
                {
                    _images[i].sprite = _answersimage[i];
                }
                _IsTimeGo = false;
            }
            else
            {
                _IsTimeGo = false;
                _animation.ClosePanel();
                StartCoroutine(_battleController.WaitForOpenPanel());
                DeleteTick();
                for(int i = 0; i < _images.Length; i++)
                {
                    _images[i].sprite = _answersimage[i];
                }
            }
        
       
    }

    private void ChoseType()
    {
        if(_randomType == 0)
        {
            FirstType();
        }
        else if(_randomType == 1)
        {
            SecondType();
        }
        else
        {
            ThirdType();
        }
    }


   private void DeleteTick()
   {
       for(int i = 0; i < _tick.Length; i++)
       {
           _tick[i].enabled = false;
       }
   }

 IEnumerator MoveBullet(GameObject bullet, Vector3 point)
        {
            while (true)
            {
            Vector3 quat = Vector3.RotateTowards( bullet.transform.forward, point - bullet.transform.position, 120 *Time.deltaTime,0.0f);
            bullet.transform.rotation = Quaternion.LookRotation(quat);
                bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, point,4.74f * Time.deltaTime);
                if (Vector3.Distance(bullet.transform.position, point) < 0.01f)
                {
                    _crystallHP -= _damage;
                    Instantiate(_destroyBullet,_enemy);
                    Destroy(bullet);
                    StartCoroutine(_battleController.WaitForOpenPanel());
                    yield break;
                    
                }
                yield return new WaitForFixedUpdate();
                
            }
        }
         public IEnumerator ClickOnEnemy(Vector3 enemyPoint)
        {

            GameObject bk = Instantiate(_background, transform);
            _animator.SetTrigger("StartFight");

            yield return new WaitForSeconds(1);
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
                   _animation.ClosePanel();
                   StartCoroutine(_battleController.WaitForOpenPanel());
                   DeleteTick();
                   for(int i = 0; i < _images.Length; i++)
                   {
                       _images[i].sprite = _answersimage[i];
                   }
                }
                yield break;
            }
            
        }
        

    }
       

}


