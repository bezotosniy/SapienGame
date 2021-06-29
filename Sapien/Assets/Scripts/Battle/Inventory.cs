using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CartoonFX;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    KeyBordController keyBoard;

    [Header("Person")]
    public float damage;
    public MovingBattlePeron person;
    public Animator animPerson;
    public Slider hpPersonSlider;
    public Text TextPersonHP;
    public int HP_Person;
    int Start_hp_Person;
    [Space]
    public string[] TaskString;
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

    int indexGem = 0;
    int CurrentUron;

    [Space]
    [Header("Task")]
    public VoiceRecognision voiceRec;
    public GameObject OneWord,OneFraze;
    public Text TimeText;
    bool TimeGo;
    string Task;
    [Space]
    private Camera Cam;
    private Ray RayMouse;
    [Space]
    [Header("Static ui")]
    public Button Bomb;
    public Slider bombSlider;
    public Text RESULT_TEXT;
    public Button GoHome;
    [Space]
    [Header("Survive Modes")]
    public bool infinitely;
    int LerningModeId;
    void Start()
    {
        Bomb.onClick.AddListener(bomb);
        bombSlider.value = 0;
        Bomb.interactable = false;
        Start_hp_Person = HP_Person;
        HP_Person_Controller(0);
        ClickOnGem.onClick.AddListener(ClickOnGems);
        InitialiseFrase();
        Cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        keyBoard = GetComponent<KeyBordController>();
       
        CanvasAnim = GetComponent<Animator>();
        StartCoroutine(StartFight());
        CreateRandom();

    }
    void CreateRandom()
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
    void Update()
    {
       
        if (Input.GetButtonDown("Fire1"))
        {
            if (Cam != null)
            {
                RaycastHit hit;
                var mousePos = Input.mousePosition;
                RayMouse = Cam.ScreenPointToRay(mousePos);

                if (Physics.Raycast(RayMouse.origin, RayMouse.direction, out hit, 40))
                {
                    if (hit.collider.tag == "Enemy")
                    {   
                        StartCoroutine(person.ClickOnEnemy(hit.collider.gameObject.transform.position));
                    }
                }
            }
            else { Debug.Log("No camera"); }
        }
    }
        
    void InitialiseFrase()
    {
        PlayerPrefs.SetString("phrase" + 0, "How are you?");
        PlayerPrefs.SetString("phrase" + 1, "Hello there");
        PlayerPrefs.SetString("phrase" + 2, "Nice to meet you");
        PlayerPrefs.SetString("phrase" + 3, "Good morning");
   
        int i = 0;
        string[] a;
        while(PlayerPrefs.GetString("phrase" + i) != "")
        {
           TaskString[i] = PlayerPrefs.GetString("phrase" + i);
            PlayerPrefs.SetInt("phraseLength", i);
            i++;
        }
               
    }
  
    public IEnumerator StartFight()
    {
        yield return new WaitForSeconds(17.5f);
        CanvasAnim.SetTrigger("StartFight");
        EnemyControll.MouseBar.gameObject.SetActive(true);
    }
    public void ClickOnGems()
    {
        if (indexGem == 0)
        {
            gems.SetTrigger("Pick");
            StartCoroutine(WaitToStart());
        }
        ClickOnGem.interactable = false;
    }
    IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(4);
        StartCoroutine(WaitForChangeScale());
        
    }
    IEnumerator WaitForChangeScale()
    {
        Debug.Log("Scale");
        if (indexGem <= 3)
        {
            float i = gem[indexGem].transform.localScale.x;
            for (float q = i; q < i * 2; q += .1f)
            {
                gem[indexGem].transform.localScale = new Vector3(q, q, q);
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForSeconds(1);
            CanvasAnim.SetTrigger("NewTask");
            TimeGo = true;
            Debug.Log("NewTask");
            if (!infinitely) StartCoroutine(InstantTask()); else StartCoroutine(InstantTaskLearning());
            StartCoroutine(Time());
            indexGem++;
        }
    }
    public IEnumerator Time()
    { int allTime =45;
        TimeGo = true;
        while (true) {
            yield return new WaitForSeconds(1f);
            
            TimeText.text = allTime.ToString();
            allTime--;
            if(allTime == 0||!TimeGo)
            {
                if (allTime == 0)
                {
                    if (num - 1 == 0) keyBoard.PostTaskAnswer();
                    else if (num - 1 == 2) voiceRec.SravnTask("");
                }
                yield break;
            }
            
        }
        

    }
    public int[] RandomTask;
    int num = 0;
    int saveString;
    int ID; //id Letter phrase
    void ChooseFrase()
    { if (RandomTask[num] != 0)
        {
            int Lenth = PlayerPrefs.GetInt("phraseLength");
            if (Lenth > 5)
                Task = PlayerPrefs.GetString("phrase" + Random.Range(Lenth - 5, Lenth));
            else
                Task = PlayerPrefs.GetString("phrase" + Random.Range(0, Lenth));
        }
        else
        {
            if (keyBoard.LetterList.Length > 1)
            {
                ID = Random.Range(0, keyBoard.LetterList.Length);
            }
            else ID = 0;
        }
    }
    void CurrentlyFrase(int number)
    {
        
            Task = PlayerPrefs.GetString("phrase" + number);
            
        
            if (keyBoard.LetterList.Length > 1)
            {
                ID = Random.Range(0, keyBoard.LetterList.Length);
            }
            else ID = 0;
        
    }
    IEnumerator InstantTask()
    {
        Debug.Log("newTask");
        ChooseFrase();
        yield return new WaitForSeconds(1.5f);
        if (num < RandomTask.Length)
        {
            if (RandomTask[num] == 0) { OneWord.SetActive(true); keyBoard.InstWord(ID); }
            else if (RandomTask[num] == 1) { keyBoard.KeyBoard(Task); }
            else { voiceRec.gameObject.SetActive(true); voiceRec.changeText(Task); }
            num++;
        }
        else
        {
            voiceRec.gameObject.SetActive(true); voiceRec.changeText(Task);
        }
    }
    IEnumerator InstantTaskLearning()
    {
        if (LerningModeId < 4f)
        {
            CurrentlyFrase(0);
            yield return new WaitForSeconds(1.5f);
            switch (LerningModeId)
            {
                case 0:
                    voiceRec.gameObject.SetActive(true); voiceRec.changeText(Task);
                    break;
                case 1:
                    voiceRec.gameObject.SetActive(true); voiceRec.changeText(Task);
                    break;
                case 2:
                    OneWord.SetActive(true); keyBoard.InstWord(ID);
                    break;
                case 3:
                    OneWord.SetActive(true); keyBoard.InstWord(ID);
                    break;

            }
            LerningModeId++;
        }
        
    }
    public void EnemyDie()
    {
        Instantiate(ParticleUronNumber, EnemyControll.transform.position + new Vector3(1,1,1),Quaternion.identity);
        ParticleUronNumber.GetComponent<CFXR_ParticleText_Runtime>().text = CurrentUron.ToString();
        EnemyControll.hpSlider.value -= CurrentUron;
        if (EnemyControll.hpSlider.value <= 0)
        {
            EnemyControll.hpSlider.value = 0;
            EnemyControll.EnemyAnim.SetTrigger("die");
            StartCoroutine(FinishGame()); 
        }
        else
        {
            StartCoroutine(EnemyControll.EnemyGiveUron((int)damage));
        }

    }
    public void HP_Person_Controller(int damage)
    {
        hpPersonSlider.maxValue = Start_hp_Person;
        HP_Person -= damage;
        TextPersonHP.text = HP_Person.ToString() + "/" + Start_hp_Person;
        hpPersonSlider.value = HP_Person;
        if (HP_Person <= 0)
            StartCoroutine(FinishGame());

    }
    
    public void CrashGem(int Plus)
    {
        if (infinitely)
        {
            if (Plus != 0)
            {
                CurrentUron += Plus;
                bombSlider.value += 0.3f; if (bombSlider.value >= 1) Bomb.interactable = true;
                StartCoroutine(CrashGemCoroutine());
                
            }
            else { StartCoroutine(Repeat()); }
        }
        else
        {
           
                CurrentUron += Plus;
                bombSlider.value += 0.3f; if (bombSlider.value >= 1) Bomb.interactable = true;
                StartCoroutine(CrashGemCoroutine());
                
           
        }
    }
    IEnumerator Repeat()
    {
      
        TimeGo = false;
        yield return new WaitForSeconds(1);
        CanvasAnim.SetTrigger("NewTask");
        yield return new WaitForSeconds(2f);
        CanvasAnim.SetTrigger("NewTask");
        StartCoroutine(Time());
        
        yield return new WaitForSeconds(1.5f);
        if (num < RandomTask.Length)
        {
            if (RandomTask[num-1] == 0) { OneWord.SetActive(true); keyBoard.InstWord(ID); }
            else if (RandomTask[num-1] == 1) { keyBoard.KeyBoard(Task); }
            else { voiceRec.gameObject.SetActive(true); voiceRec.changeText(Task); }
          
        }
        else
        {
            voiceRec.gameObject.SetActive(true); voiceRec.changeText(Task);
        }


    }
    IEnumerator CrashGemCoroutine()
    {
        Debug.Log("CrashGem");
        yield return new WaitForSeconds(2);
        CanvasAnim.SetTrigger("NewTask");
        TimeGo = false;
        
        yield return new WaitForSeconds(0.5f);
    OneWord.SetActive(false); OneFraze.SetActive(false); voiceRec.gameObject.SetActive(false);
    yield return new WaitForSeconds(1f);
    tranfGem.position = gem[indexGem - 1].transform.position;
        gem[indexGem - 1].SetActive(false);
        Instantiate(ParticleCrashGem, tranfGem.transform);

        StartCoroutine(WaitForChangeScale());

    }
    public void bomb()
    {
        if(bombSlider.value >= 1)
        {
            damage = 999;
            bombSlider.value = 0;
            Bomb.interactable = false;
        }
    }

    IEnumerator FinishGame()
    {
        Cam.GetComponent<Animator>().SetTrigger("end");
        yield return new WaitForSeconds(7f);
        RESULT_TEXT.gameObject.SetActive(true);
        GoHome.gameObject.SetActive(true);
        GoHome.onClick.AddListener(GoHomeVoid);

        if (HP_Person <= 0)
        {
            RESULT_TEXT.text = "LOSE";
            animPerson.SetTrigger("lose");
        }
        else if (EnemyControll.hpSlider.value <= 0)
        {
            RESULT_TEXT.text = "WIN";
            animPerson.SetTrigger("win");
        }
    }
    void GoHomeVoid()
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
}

