using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CartoonFX;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition.Examples;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class BattleController : MonoBehaviour
{
   KeyBordController keyBoard;

   [Header("Sounds")]
   public Sound[] _sounds;

    [Header("Person")]
    //public int damage;
    public MovingBattlePeron person;
    public Animator animPerson;
    public Slider hpPersonSlider;
    public Text TextPersonHP;
    public int HP_Person;
    int Start_hp_Person;
    [Space]
    //public string[] TaskString;
    private Animator CanvasAnim;
    [Space]
    [Header("Gems")]
    public Animator gems;
    public GameObject[] gem;
    public GameObject ParticleCrashGem;
    public Transform tranfGem;
    public GameObject ParticleUronNumber;
    public Button ClickOnGem;
    [Space]
    [Header("Enemy")]
    public EnemyController EnemyControll;

    public int indexGem = 0;
    public int CurrentUron;

    [Space]
    [Header("Task")]
    public VoiceregonsionForBattle voiceRec;
    public GameObject OneWord,OneFraze;
    public Text TimeText;
    public bool TimeGo;
    string Task;
    public string VoiceTask;
    public int[] RandomTask;
    [SerializeField] private AudioSource _dictorAudio;
    public AudioSource[] _dictorNotInfentlySaid;
    [Space(10f)]
    private Camera Cam;
    private Ray RayMouse;
    [Space]
    [Header("Static ui")]
    public Button Bomb;
    public Slider bombSlider;
    public Text RESULT_TEXT;
    public Button GoHome;
    [SerializeField] private AudioSource _mainMusik;
    private float _volumeofMusik;
    [SerializeField] private bool _isPlayMusik;
    [Space]
    [Header("Survive Modes")]
    public bool infinitely;
    public int LerningModeId;

    [Header("Bomb")]
    [SerializeField] private Image[] _fills;
    public int _damagePerAnswer;

    [SerializeField] private GameObject _bombUI;
    [SerializeField] private Button _infentlyInstrument;
    [SerializeField] private Button _firstButton;
    [SerializeField] private Button _secondButton;
    [SerializeField] private Button _hammerButton;

    [SerializeField] private UIAnimation _animation;
    [SerializeField] private VoicePlaybackForBattle _voicePlayback;
    [SerializeField] private VoiceregonsionForBattle _voiceRecognision;
    [SerializeField] private UIController _uiController;
     int allTime = 45;
     [SerializeField] private int _firstTaskTime;
     [SerializeField] private int _secondTaskTime;
     [SerializeField] private int _thirdTaskTime;
     [SerializeField] private int _fourthTaskTime;

     public int RandomString;
     [SerializeField] private Text _taskText;
     [SerializeField] private GameObject _pharsePanel;
    
     [SerializeField] private Image _firstButtonImage;

     [SerializeField] private int _random;
     [SerializeField] private EnemyController _enemyContoller;
    
     [SerializeField] private GameObject _diamond;
     [SerializeField] private GameObject[] _enemies;
     [SerializeField] private GameObject _hammerPanel;
     [SerializeField] private HammerBattle _hammerBattle;
     public bool IsHammerTask;
     [SerializeField] private EnemiesController _enemiesController;
     [SerializeField] private Animation _openPanelHammerMod;
     
     [SerializeField] private int _damage;
    [SerializeField] private int _minDamage;
    [SerializeField] private int _maxDamage;
    [SerializeField] private Transform _player;
    [SerializeField] private Slider[] _enemyHP;
    [SerializeField] private Canvas[] _enemyCanvas;
    [SerializeField] private List<EnemyController> _enemyController = new List<EnemyController>();  
    public int _countAttack;  
    [SerializeField] private GameObject _someWordsPanel;
    [SerializeField] private bool _isHammerMod;
    [SerializeField] private DoneAndMissed _doneAndMissed;
    [SerializeField] private int _damageBomb;
    [SerializeField] private AnimationBattleUI _animationStats;
    [SerializeField] private SavingBomb _saving;
    public bool IsBomb;
    private bool _doneSomeWordsButtonEnabled;
    public bool IsBombTask = false; 



    private void Start()
    {
        for(int i = 0; i < _fills.Length; i++)
        {
            if(_saving._saving.IsBombsFillOpened[i])
            {
                _fills[i].enabled = true;
            }
        }
       
       if(!infinitely)
       {
            CreateRandom();
            gem[3].SetActive(false);
       }
            
       
        InfentlyMode();
        _mainMusik.Play();
       
        _infentlyInstrument.onClick.AddListener(ClickOnGems);
        _secondButton.onClick.AddListener(ClickOnGems);
        _hammerButton.onClick.AddListener(OnHammerButtonCLick);
        Bomb.onClick.AddListener(OnClickBombButton);
        Bomb.interactable = false;
        Start_hp_Person = HP_Person;
        HP_Person_Controller(0);
        //InitialiseFrase();
        Cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        keyBoard = GetComponent<KeyBordController>();
       _volumeofMusik = _mainMusik.GetComponent<AudioSource>().volume;
       // CanvasAnim = GetComponent<Animator>();
        StartCoroutine(StartFight());
        _firstButtonImage = _firstButton.GetComponent<Image>();

        if(_isHammerMod)
        {
            for(int  i = 0; i < _enemies.Length; i++)
            {
                _enemies[i].SetActive(false);
            }

        }
        
    
    }

    private void Awake()
    {
        _saving.Load();
    }

     private void CreateRandom()
    {
        
        
        for (int i = RandomTask.Length - 1; i > 0; i--)
        {
            var r = new System.Random();
            int j = r.Next(i);
            var t = RandomTask[i];
            RandomTask[i] = RandomTask[j];
            RandomTask[j] = t;
        }
    }

    private void Update()
    {
        /*if(LerningModeId == 4)
        {
            _firstButton.transform.DOMoveY(80, 0.5f);
            _firstButtonImage.enabled = true;
            
        }*/
    }
    
     /*void FixedUpdate()
    {
       
        if (Input.GetButtonDown("Fire1") && IsAttack == true)
        {
            if (Cam!= null)
            {
                RaycastHit hit;
                var mousePos = Input.mousePosition;
                RayMouse = Cam.ScreenPointToRay(mousePos);

                if (Physics.Raycast(RayMouse.origin, RayMouse.direction, out hit, 40))
                {
                    if (hit.collider.tag == "Enemy")
                    {   
                        StartCoroutine(ClickOnEnemy(hit.collider.gameObject.transform.position));
                        damage = Random.Range(_minDamage, _maxDamage);
                        bombSlider.value = 0;
                        Bomb.interactable = false;
                        _enemyContoller.IsAttack = false;
                    }
                }
            }
            else { Debug.Log("No camera"); }
        }
    }*/
    /*private void InitialiseFrase()
    {
        PlayerPrefs.SetString("phrase" + 0, "Hello");
        PlayerPrefs.SetString("phrase" + 1, "Hello there");
        PlayerPrefs.SetString("phrase" + 2, "Nice to meet you");
        PlayerPrefs.SetString("phrase" + 3, "Good morning");
   
        int i = 0;
       
        while(PlayerPrefs.GetString("phrase" + i) != "")
        {
           TaskString[i] = PlayerPrefs.GetString("phrase" + i);
            PlayerPrefs.SetInt("phraseLength", i);
            i++;
        }
               
    }*/
      private IEnumerator StartFight()
    {
        Debug.Log("StartFight");
        yield return new WaitForSeconds(13f);
       StartFightPanel();
       yield return new WaitForSeconds(1.25f);
       if(!infinitely && !_isHammerMod)
       {
            _animationStats.OpenStatsUI();
       }
       else if(_isHammerMod)
       {
            _hammerButton.transform.DOScale(new Vector3(0.56f, 0.56f, 0.56f), 0.5f);
            _infentlyInstrument.GetComponent<Image>().enabled = false;
            _secondButton.GetComponent<Image>().enabled = false;
       }
       else if(infinitely)
       {
           _animationStats.OpenInfenetlyModStats();
       }
       
    }

      public void StartFightPanel()
      {
         
          Debug.Log("Yes");
          
          CurrentUron = 0;
          _someWordsPanel.SetActive(false);
          _enemiesController.ChangeStatus();
          _countAttack = 0;


        if(IsHammerTask)
        {
            _infentlyInstrument.transform.DOScale(new Vector3(0,0,0), 0.01f);
             _secondButton.transform.DOScale(new Vector3(0, 0, 0), 0.01f);
        }
        if(infinitely)
        {
            _infentlyInstrument.transform.DOScale(new Vector3(0.56f, 0.56f, 0.56f), 0.5f);
        }
        else
        {
            _firstButtonImage.GetComponent<Image>().enabled = true;
            _secondButton.transform.DOScale(new Vector3(0.56f, 0.56f, 0.56f), 0.5f);
            _secondButton.GetComponent<Image>().enabled  = true;
        }
      }

      public void ClickOnGems()
    {
       
        indexGem = 0;
        LerningModeId = 0;
        if (indexGem == 0)
        {
            for(int i = 0; i < gem.Length - 1; i++)
               {
                   gem[i].GetComponent<MeshRenderer>().material.DOColor(Color.magenta, 0.01f);
                   gem[i].SetActive(true);
                   gem[i].transform.DOScale(new Vector3(1,1,1), 0.5f);
               }
            _sounds[1].PlaySound();
            gems.SetTrigger("Pick");
            StartCoroutine(WaitToStart());
        }
        _infentlyInstrument.interactable = false;
        _secondButton.interactable = false;
    }

     public IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(2.5f);
        StartCoroutine(WaitForChangeScale());
        if(infinitely)
        {
            _infentlyInstrument.transform.DOScale(new Vector3(0,0,0), 0.5f);
        }
        else
        {
            
            _secondButton.transform.DOScale(new Vector3(0, 0, 0), 0.5f);
            _firstButton.GetComponent<Image>().enabled = false;
        }
       
        
    }

     private IEnumerator WaitForChangeScale()
    {
        yield return new WaitForSeconds(1.5f);
       
       if(infinitely)
       {
          if (indexGem <= 3)
        {
             Debug.Log("Scale");
            _sounds[2].PlaySound();
            float i = gem[indexGem].transform.localScale.x;
            for (float q = i; q < i * 2; q += .1f)
            {
                yield return new WaitForFixedUpdate();
                gem[indexGem].transform.localScale = new Vector3(q, q, q);
               
            }
            
            RandomString = Random.Range(0,_voiceRecognision.TaskNotInfently.Length);
            _taskText.text = voiceRec.Task;
            
            yield return new WaitForSeconds(2);
            _mainMusik.Stop();
           _animation.OpenPanel();
            TimeGo = true;
            Debug.Log("NewTask");
            _sounds[0].PlaySound();
            StartCoroutine(InstantTaskLearning());
        }
       }
       else if(!infinitely)
       {
           if (indexGem <= 2)
        {
            Debug.Log("Scale");
            _sounds[2].PlaySound();
            float i = gem[indexGem].transform.localScale.x;
            for (float q = i; q < i * 2; q += .1f)
            {
                 yield return new WaitForFixedUpdate();
                gem[indexGem].transform.localScale = new Vector3(q, q, q);
               
            }
            RandomString = Random.Range(0,_voiceRecognision.TaskNotInfently.Length);
            _taskText.text = voiceRec.TaskNotInfently[RandomString];
            voiceRec.gameObject.SetActive(true);
            yield return new WaitForSeconds(2);
            _mainMusik.Stop();
            _animation.OpenPanel();
            TimeGo = true;
            Debug.Log("NewTask");
            _sounds[0].PlaySound();
            StartCoroutine(InstantTaskNotInfently());
            
        }
        else 
        {
            _enemyContoller.IsAttack = true;
            BombDamagePlus(_damageBomb);
        }
         
    }
   
    }
        /*if (indexGem <= 3)
        {
            float i = gem[indexGem].transform.localScale.x;
            for (float q = i; q < i * 2; q += .1f)
            {
                 yield return new WaitForFixedUpdate();
                gem[indexGem].transform.localScale = new Vector3(q, q, q);
               
            }
            RandomString = Random.Range(0,_voiceRecognision.TaskNotInfently.Length);
            _taskText.text = voiceRec.TaskNotInfently[RandomString];
            voiceRec.gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
            _mainMusik.Stop();
            
            TimeGo = true;
            Debug.Log("NewTask");
            if (infinitely) 
            {
              _animation.OpenPanel();
               StartCoroutine(InstantTaskLearning());
            }
            else if(!infinitely && indexGem <= 2)
            {
               _animation.OpenPanel();
                StartCoroutine(InstantTaskNotInfently());
                if(indexGem == 2)
                {
                    _enemyContoller.IsAttack = true;
                    for(int a = 0; i < gem.Length; i++)
                    {
                        gem[a].SetActive(false);
                    }
                }
            }
        }
        else 
        {
            _enemyContoller.IsAttack = true;
        }*/
        
    


  
        
      IEnumerator InstantTaskLearning()
    {
        indexGem++;
        StartCoroutine(Time());
        if (LerningModeId < 4f)
        {
            
            yield return new WaitForSeconds(1.5f);
            switch (LerningModeId)
            {
                case 0:
                    StartCoroutine(ListenToDictor());
                    break;
                case 1:
                    _uiController.InterLocutorSaid();
                    StartCoroutine(Speak());
                    break;
                case 2:
                    _uiController.ChooseLettersUI();
                    OneWord.SetActive(true); keyBoard.InstWord(0);
                    break;
                case 3:
                    _uiController.ChooseLettersUI();
                    OneWord.SetActive(true); keyBoard.InstWord(0);
                    break;

            }
            
        }
       
    }

    private IEnumerator InstantTaskNotInfently()
    {
        indexGem++;
        _random = Random.Range(0,2);
        StartCoroutine(Time());
        if(LerningModeId <= 2f)
        {
            yield return new WaitForSeconds(1.5f);
            switch(LerningModeId)
            {
                case 0:
                
                if(_random == 0)
                {
                    _uiController.InterLocutorSaid();
                    StartCoroutine(ListenToDictorNotinfently());
                }
                else
                {
                    _uiController.InterLocutorSaid();
                    StartCoroutine(SpeakIfNotInfently());
                }
                
                break;
                case 1:
                keyBoard.InstWordNotInfenetly(0);
                _uiController.ChooseLettersUI();
                _uiController.ChooseLettersUI();
                OneWord.SetActive(true); 
                
                break;
                case 2:
                 OneWord.SetActive(false);
                 keyBoard.KeyBoard();
                _uiController.DoPharses();
                _pharsePanel.SetActive(true); 
              
           
                break;
            }
        }
        else 
        {
                Debug.Log("End");
        }
    }


    public IEnumerator Repeat()
    {
        
        yield return new WaitForSeconds(2.5f);
        _sounds[4].PlaySound();
        _animation.ClosePanel();
        _doneAndMissed.ScaleMissed(0, 250);
        yield return new WaitForSeconds(2f);
        _animation.OpenPanel();
        StartCoroutine(Time());
        
        yield return new WaitForSeconds(1.5f);
        
       switch (LerningModeId)
            {
                case 0:
                if(infinitely)
                    
                    StartCoroutine(ListenToDictor());
                    break;
                case 1:
                if(infinitely)
                    
                    _uiController.InterLocutorSaid();
                    StartCoroutine(Speak());
               
                    break;
                case 2:
                    _uiController.ChooseLettersUI();
                    OneWord.SetActive(true); keyBoard.InstWord(0);
                   
                    break;
                case 3:
                    _uiController.ChooseLettersUI();
                    OneWord.SetActive(true); keyBoard.InstWord(0);
                    
                    break;

            }
       
    }


    public IEnumerator ClosePanel()
    {
       LerningModeId++;
       
        yield return new WaitForSeconds(2);
        _sounds[4].PlaySound();
       _animation.ClosePanel();
        _uiController._dialogPanel.SetActive(false);
        _uiController._microphonePanel.SetActive(false);
        StartCoroutine(WaitForChange());
        
        _doneAndMissed.ScaleMissed(0, 250);
        
    }

    private IEnumerator WaitForChange(){
        yield return new WaitForSeconds(3);
        _sounds[5].PlaySound();
        gem[indexGem - 1].transform.DOScale(new Vector3(1,1,1), 0.5f);
        gem[indexGem - 1].GetComponent<MeshRenderer>().material.DOColor(Color.black, 0.7f);
        StartCoroutine(WaitForChangeScale());
    }

    private IEnumerator ListenToDictor()
    {
        _voicePlayback.OnClickPlayButton();
        yield return new WaitForSeconds(1);
    }

    private IEnumerator Speak()
    {
       yield return new WaitForSeconds(1);
       _uiController.SpeakUI();
       voiceRec.StartRecordButtonOnClickHandler();
    }

    private IEnumerator SpeakIfNotInfently(){
        yield return new WaitForSeconds(1);
        _uiController.SpeakUI();

        voiceRec.StartRecordButtonOnClickHandler();
    }

    private IEnumerator ListenToDictorNotinfently()
    {
     
        _voicePlayback.OnClickPlayButton();
        yield return new WaitForSeconds(1);
    }

      public void CrashGem(int Plus)
    {
        
        CurrentUron += Plus;
        StartCoroutine(CrashGemCoroutine());
        
    }

    public void BombDamagePlus(int Plus)
    {
        
        
            for(int i = 0; i < _fills.Length; i++)
            {
                if(CurrentUron == _damagePerAnswer * 3)
                {
                    if(!_fills[i].enabled)
                    {
                       _fills[i].enabled = true;
                       _saving._saving.IsBombsFillOpened[i] = true;
                       if(i != _fills.Length - 1)
                       {
                            _fills[i + 1].enabled = true;
                            _saving._saving.IsBombsFillOpened[i + 1] = true; 
                       }
                       break;
                       
                    }
                }
                else if(CurrentUron == _damagePerAnswer * 2)
                {
                    if(!_fills[i].enabled)
                    {
                       _fills[i].enabled = true;
                       _saving._saving.IsBombsFillOpened[i] = true;
                    }
                    break;
                }
               
               
            }
            if(_fills[_fills.Length - 1].enabled)
            {
                Bomb.transform.DOScale(new Vector3(0.55f, 0.55f, 0.6f), 0.6f);
                Bomb.interactable = true;
                

            }
            _saving.Save();

        
       
       
           
        
    }

    public void BombDamageForhammerMod()
    {
        for(int i = 0; i < _fills.Length; i++)
        {
            if(!_fills[i].enabled)
            {
               _fills[i].enabled = true;
               _saving._saving.IsBombsFillOpened[i] = true;
            }
            _saving.Save();
        }
    }

    IEnumerator CrashGemCoroutine()
    {
        
        Debug.Log("CrashGem");
        yield return new WaitForSeconds(3);
        _doneAndMissed.ScaleGood(0, 250);
        _sounds[4].PlaySound();
       _animation.ClosePanel();
        TimeGo = false;
        yield return new WaitForSeconds(0.5f);
        OneWord.SetActive(false); OneFraze.SetActive(false); //voiceRec.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        tranfGem.position = gem[indexGem - 1].transform.position;
        gem[indexGem - 1].SetActive(false);
        Instantiate(ParticleCrashGem, tranfGem.transform);
        _sounds[6].PlaySound();

        StartCoroutine(WaitForChangeScale());

    }

    public IEnumerator Time()
    {
        
        switch(LerningModeId)
        {
            case 0:
            allTime = _firstTaskTime;
            break;

            case 1:
            allTime = _secondTaskTime;
            break;

            case 2:
            allTime = _thirdTaskTime;
            break;

            case 3: 
            allTime = _fourthTaskTime;
            break;
        }
        TimeGo = true;
        while (true) {
            yield return new WaitForSeconds(1f);
            
            TimeText.text = allTime.ToString();
            allTime--;
            if(allTime < 2)
            {
                voiceRec.StopRecordButtonOnClickHandler();
            }
            if(allTime < 3)
            {
                keyBoard.enter.interactable = false; 
                if(FindObjectOfType<SomeWords>() != null)
                {
                    _doneSomeWordsButtonEnabled = GetComponent<SomeWords>()._done.interactable = false;
                
                }
            
            }
            if(allTime < 0||!TimeGo)
            {
               
                if (allTime <=0)
                {
                    _sounds[7].PlaySound();
                    if(infinitely)
                    {
                        keyBoard.enter.interactable = false;
                        StartCoroutine(Repeat());
                        if(LerningModeId == 2)
                        {
                            StartCoroutine(keyBoard.DestroyInstWord(2.2f));
                            
                        }
                    }
                    else
                    {
                        keyBoard.enter.interactable = false;
                        StartCoroutine(ClosePanel());
                        TimeGo = false;
                        if(LerningModeId == 2)
                        {
                            StartCoroutine(keyBoard.DestroyInstWord(2.2f));
                        }
                       
                        
                    }
                    
                    
                }
                yield break;
            }
            
        }
        

    }

      public void HP_Person_Controller(int damage)
    {
        hpPersonSlider.maxValue = Start_hp_Person;
        HP_Person = Mathf.Clamp(HP_Person - damage, 0, Start_hp_Person);
        TextPersonHP.text = HP_Person.ToString() + "/" + Start_hp_Person;
        hpPersonSlider.value = HP_Person;
        if (HP_Person <= 0)
        {
            StartCoroutine(FinishGame());
        }
    }


    private void OnClickBombButton()
    {
        Bomb.transform.DOScale(new Vector3(0, 0, 0), 0.6f);
        LerningModeId = Random.Range(0,3);
        StartCoroutine(InstantTaskNotInfently());
        IsBomb = true;
        IsBombTask = true;
        LerningModeId = Random.Range(0, 2);
        RandomString = Random.Range(0,_voiceRecognision.TaskNotInfently.Length);
        _taskText.text = voiceRec.TaskNotInfently[RandomString];
        voiceRec.gameObject.SetActive(true);
        _mainMusik.Stop();
        _animation.OpenPanel();
        TimeGo = true;
        Debug.Log("NewTask");
        _sounds[0].PlaySound();
        StartCoroutine(InstantTaskNotInfently());
    }

     public void EnemyDie()
    {
        
        
       
        _enemiesController.LastHited.hpSlider.value -= CurrentUron;
      
        if(_enemyController.Count == 1)
        {
            Debug.Log("One");
            OneEnemy();
        }
        else if(_enemyController.Count== 2)
        {
            Debug.Log("Two");
            TwoEnemy();
        }
        else if(_enemyController.Count == 3)
        {
            Debug.Log("Three");
            ThreeEnemy();
        }
        else
        {
            Debug.Log("Error");
        }
            
            
            
        
       

    }

    private void OneEnemy()
    {
       if(_enemyController[0].hpSlider.value <= 0 && _enemyController.Count == 1)
        {
            _enemyController.RemoveAt(0);
            StartCoroutine(FinishGame()); 
            _enemyCanvas[0].enabled = false;
            _enemiesController.RemoveEnemy(0);
            StartFightPanel();
             if(_enemiesController._enemies[_enemiesController.RandomEnemy].GetComponent<EnemyController>().hpSlider.value >= 0)
            {
            Debug.Log("Start Attack");
            _damage = Random.Range(_minDamage, _maxDamage);
            StartCoroutine(_enemiesController.EnemyGiveUron(_damage));
            }
                   
        }
          else if(_enemiesController._enemies[_enemiesController.RandomEnemy].GetComponent<EnemyController>().hpSlider.value <= 0)
        {
            StartFightPanel();
        }
        else if(_enemyController.Count > 1)
        {
            _damage = Random.Range(_minDamage, _maxDamage);
            StartCoroutine(_enemiesController.EnemyGiveUron(_damage));
        }
        else
        {
            StartCoroutine(FinishGame());
        }
    }


    private void TwoEnemy()
    {
         if(_enemyController[0].hpSlider.value <= 0 && _enemyController.Count == 1)
        {
            _enemyController.RemoveAt(0);
            StartCoroutine(FinishGame()); 
            _enemyCanvas[0].enabled = false;
            _enemiesController.RemoveEnemy(0);
            StartFightPanel();
             if(_enemiesController._enemies[_enemiesController.RandomEnemy].GetComponent<EnemyController>().hpSlider.value >= 0)
            {
            Debug.Log("Start Attack");
            _damage = Random.Range(_minDamage, _maxDamage);
            StartCoroutine(_enemiesController.EnemyGiveUron(_damage));
            }
                   
        }
        else if(_enemyController[1].hpSlider.value <= 0 && _enemyController.Count == 2)
        {
           _enemyController.RemoveAt(1);
            StartCoroutine(FinishGame()); 
            _enemyCanvas[1].enabled = false;
            _enemiesController.RemoveEnemy(1);
            StartFightPanel();
             if(_enemiesController._enemies[_enemiesController.RandomEnemy].GetComponent<EnemyController>().hpSlider.value >= 0)
            {
            Debug.Log("Start Attack");
            _damage = Random.Range(_minDamage, _maxDamage);
            StartCoroutine(_enemiesController.EnemyGiveUron(_damage));
            }
        }
        else if(_enemiesController._enemies[_enemiesController.RandomEnemy].GetComponent<EnemyController>().hpSlider.value <= 0)
        {
            StartFightPanel();
        }
        else if(_enemyController.Count > 1)
        {
            _damage = Random.Range(_minDamage, _maxDamage);
            StartCoroutine(_enemiesController.EnemyGiveUron(_damage));
        }
        else
        {
            StartCoroutine(FinishGame());
        }
    }

    private void ThreeEnemy()
    {
     
      
           

        if(_enemyController[0].hpSlider.value <= 0 && _enemyController.Count == 1)
        {
            _enemyController.RemoveAt(0);
            StartCoroutine(FinishGame()); 
            _enemyCanvas[0].enabled = false;
            _enemiesController.RemoveEnemy(0);
            StartFightPanel();
            if(_enemiesController._enemies[_enemiesController.RandomEnemy].GetComponent<EnemyController>().hpSlider.value >= 0)
            {
            Debug.Log("Start Attack");
            _damage = Random.Range(_minDamage, _maxDamage);
            StartCoroutine(_enemiesController.EnemyGiveUron(_damage));
            }
                   
        }
        else if(_enemyController[1].hpSlider.value <= 0 && _enemyController.Count == 2)
        {
           _enemyController.RemoveAt(1);
            StartCoroutine(FinishGame()); 
            _enemyCanvas[1].enabled = false;
            _enemiesController.RemoveEnemy(1);
            StartFightPanel();
             if(_enemiesController._enemies[_enemiesController.RandomEnemy].GetComponent<EnemyController>().hpSlider.value >= 0)
            {
                   Debug.Log("Start Attack");
            _damage = Random.Range(_minDamage, _maxDamage);
            StartCoroutine(_enemiesController.EnemyGiveUron(_damage));
            }
        }
        else if(_enemyController[2].hpSlider.value <= 0 && _enemyController.Count == 3)
        {
            _enemyController.RemoveAt(2);
            StartCoroutine(FinishGame()); 
            _enemyCanvas[2].enabled = false;
            _enemiesController.RemoveEnemy(2);
            StartFightPanel();
            if(_enemiesController._enemies[_enemiesController.RandomEnemy].GetComponent<EnemyController>().hpSlider.value >= 0)
            {
                   Debug.Log("Start Attack");
            _damage = Random.Range(_minDamage, _maxDamage);
            StartCoroutine(_enemiesController.EnemyGiveUron(_damage));
            }
        }
        else if(_enemiesController._enemies[_enemiesController.RandomEnemy].GetComponent<EnemyController>().hpSlider.value <= 0)
        {
            StartFightPanel();
        }
        else if(_enemyController.Count > 0)
        {
            Debug.Log("Start Attack");
            _damage = Random.Range(_minDamage, _maxDamage);
            StartCoroutine(_enemiesController.EnemyGiveUron(_damage));
        }
        else
        {
            StartCoroutine(FinishGame());
        }
      
        
    }


    

     public IEnumerator FinishGame()
    {
         for(int i = 0; i < gem.Length; i++)
       {
           gem[i].SetActive(false);
       }
        
       
      
        _firstButton.GetComponent<Image>().enabled = true;
        _firstButton.transform.DOMoveY(-70,0.5f);

      if(!infinitely)
      {
         if (HP_Person <= 0)
        {
            Cam.GetComponent<Animator>().SetTrigger("end");
            animPerson.SetTrigger("Lose");
             
            for(int i = 0; i < gem.Length; i++)
            {
                gem[i].SetActive(false);
            }
            _sounds[10].PlaySound();
            yield return new WaitForSeconds(4);
            _player.DOMoveY(3.8f, 0.4f);
            GoHome.gameObject.SetActive(true);
            GoHome.onClick.AddListener(GoHomeVoid);
            RESULT_TEXT.gameObject.SetActive(true);
           _sounds[11].PlaySound();
            RESULT_TEXT.text = "LOSE";
           StartCoroutine(GoHomeAvoidAutomatic());
            
        }
         else if(_enemyHP[0].value <= 0 && _enemyHP[1].value <= 0 && _enemyHP[2].value <= 0)
        {
            animPerson.SetTrigger("Win");
            Cam.GetComponent<Animator>().SetTrigger("end");
            _sounds[8].PlaySound();
           for(int i = 0; i < gem.Length; i++)
            {
                gem[i].SetActive(false);
            }
            yield return new WaitForSeconds(4);
            GoHome.gameObject.SetActive(true);
            GoHome.onClick.AddListener(GoHomeVoid);
            RESULT_TEXT.gameObject.SetActive(true);
            RESULT_TEXT.text = "WIN";
            _sounds[9].PlaySound();
           for(int i = 0; i < _enemyHP.Length; i++)
           {
               if(_enemyHP[i].value <= 0)
               {
                   _enemyCanvas[i].enabled = false;
               }
           }
            

        }

        StartCoroutine(GoHomeAvoidAutomatic());
       
      }
      else
    {
        animPerson.SetTrigger("Win");
        Cam.GetComponent<Animator>().SetTrigger("end");
        _sounds[8].PlaySound();
        for(int i = 0; i < gem.Length; i++)
        {
            gem[i].SetActive(false);
        }
        yield return new WaitForSeconds(4);
        GoHome.gameObject.SetActive(true);
        GoHome.onClick.AddListener(GoHomeVoid);
        RESULT_TEXT.gameObject.SetActive(true);
        RESULT_TEXT.text = "WIN";
        _sounds[9].PlaySound();
        for(int i = 0; i < _enemyHP.Length; i++)
        {
            if(_enemyHP[i].value <= 0)
            {
                _enemyCanvas[i].enabled = false;
            }
        }
        StartCoroutine(GoHomeAvoidAutomatic());
    }
        
       
    }

    

    public void GoHomeVoid()
    {
        StartCoroutine(LoadYourAsyncScene());
    }
    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(PlayerPrefs.GetInt("LastScene"));
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private IEnumerator GoHomeAvoidAutomatic()
    {
        yield return new WaitForSeconds(7);
        StartCoroutine(LoadYourAsyncScene());
    }


   private void OnHammerButtonCLick()
   {
       IsHammerTask = true;
       for(int i = 0; i < gem.Length; i++)
       {
           gem[i].SetActive(false);
       }
       for(int  i = 0; i< _enemies.Length; i++)
       {
            _enemies[i].SetActive(false);
       }
       _diamond.transform.DOMove(new Vector3(-33.35f, 5.447f, -85.975f), 1.5f);
       StartCoroutine(WaitForOpenHammerPanel());
       
   }


    public IEnumerator WaitForOpenHammerPanel(){
        yield return new WaitForSeconds(3);
        if(_hammerBattle.index <= _hammerBattle._randomTask.Length)
        {
            _uiController._microphonePanel.SetActive(false);
            _uiController._dialogPanel.SetActive(false);
            _hammerPanel.SetActive(true);
            _openPanelHammerMod.Play();
            _hammerBattle._sounds[1].PlaySound();
            _hammerButton.transform.DOMoveY(-70, 0.5f);
            _secondButton.transform.DOMoveY(-70, 0.5f);
            StartCoroutine(_hammerBattle.OpenType());
        }
        
        
       
        
    }
    private void InfentlyMode()
    {
       if(infinitely == true)
        {
            _bombUI.SetActive(false);
            _infentlyInstrument.GetComponent<Image>().enabled = true;
            ClickOnGem.GetComponent<Image>().enabled = false;
            _firstButton.GetComponent<Image>().enabled = false;
            _secondButton.GetComponent<Image>().enabled = false;
            for(int i = 1; i < _enemies.Length; i++)
            {
                _enemies[i].SetActive(false);
            }
            _enemyCanvas[0].enabled = false;
        }
        else
        {
            _bombUI.SetActive(true);
            _infentlyInstrument.GetComponent<Image>().enabled = false;
             ClickOnGem.GetComponent<Image>().enabled = false;
            _firstButton.GetComponent<Image>().enabled = false;
            _secondButton.GetComponent<Image>().enabled = true;
        }
    }



    

}
