using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;


public class KeyBordController : MonoBehaviour
{
    private BattleController _battleController;
    [SerializeField] private VoiceregonsionForBattle _voiceRecognision;
    string textTask;

    public Transform position;
    int ID;
    string[] keyLength;

    [Space]
    [Header("Char")]
    public Transform pointLetterInst;
    public Spriteword[] LetterList;  

    [SerializeField] private Spriteword[] _letterListIfNotInfenetly;
    public Button[] buttonLetter;
    public Button space, remove, removeAll, enter;
    public GameObject[] instWord;
   
    [SerializeField] private GameObject[] _instWordNotInfenetly; 
    [SerializeField] private int _randomWord;
    [Space]
    [Header("Phrase")]
    [Range(1, 400)]
    public int OffsetFraseX;
    public string TextOneButton;
    public GameObject movingObject;
   
    public GameObject longFrase;
    public Button EnterText;
    public ColiderLongFrase[] Cl;
    public string TaskSum;
    Spriteword sp;
    public int _counter;
    [SerializeField] private  GameObject _microphonePanel;
    [SerializeField] private GameObject _DialogPanel;
    [Header("SomeWords")]
    [Space(20f)]
    [SerializeField] private GameObject[] _someWordsPrefabs;
    [SerializeField] private GameObject _someWordsParent;
    private GameObject _prefab;


    private void Start()
    {
        removeAll.onClick.AddListener(RemoveAll);
        enter.onClick.AddListener(buttonDoneLetter);
        remove.onClick.AddListener(Remove);
        EnterText.onClick.AddListener(PostTaskAnswer);
        position.gameObject.SetActive(false);
        _battleController = GetComponent<BattleController>();
        _randomWord = Random.Range(0, _someWordsPrefabs.Length);
      
    }
    public void KeyBoard()
    {
        
        InstFrase();
      
    }

    public void InstWord(int id)
    {
      
        IsButtonLetterInteractable(true);
        if(_counter != 0)
        {
            _randomWord = Random.Range(0, instWord.Length);
        }
        
        ID = id;
        enter.interactable = false;
        instWord[_randomWord] = Instantiate(LetterList[_randomWord].gameObject, transform);
        instWord[_randomWord].transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.01f);
        instWord[_randomWord].transform.DOMove(new Vector3(1039, 465, 0), 0.01f);
        sp = instWord[_randomWord].GetComponent<Spriteword>();
        if(_battleController.LerningModeId == 2)
        {
           sp.GenerateTask();
          
       }
        else if(_battleController.LerningModeId == 3)
        {
            sp.GenerateTaskHarder();
            
        }
       
        textTaskChar = "";
        sp.textTaskOneWord.text = textTaskChar;
        normalChar = false;
        int i = 0;
        int random = buttonLetter.Length - 1;
        
        for (; i < buttonLetter.Length; i++)
        {
            if (Random.Range(i, buttonLetter.Length) == random && !normalChar)
            {
                normalChar = true;
                buttonLetter[i].GetComponentInChildren<Text>().text = sp.TextTask[numberLetter].ToString();
            }
            else
            {
                System.Random rand = new System.Random();
                char ch = (char)rand.Next(0x0061, 0x007A);
                if (ch == 'z')
                    ch = (char)((int)'a'+ i);
                else
                    ch = (char)((int)ch + i);
                buttonLetter[i].GetComponentInChildren<Text>().text = ch.ToString();
            }
        }
        numberLetter++;
    }
    public void InstWordNotInfenetly(int id)
    {
        IsMainButtonsEnabled(true);
        IsButtonLetterInteractable(true);
       
            _randomWord = Random.Range(0, _instWordNotInfenetly.Length);
        
        ID = id;
        enter.interactable = false;
        _instWordNotInfenetly[_randomWord] = Instantiate(_letterListIfNotInfenetly[_randomWord].gameObject, transform);
        _microphonePanel.SetActive(true);
        _DialogPanel.SetActive(true);
        _instWordNotInfenetly[_randomWord].transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.01f);
        _instWordNotInfenetly[_randomWord].transform.DOMove(new Vector3(1039, 465, 0), 0.01f);
        sp = _instWordNotInfenetly[_randomWord].GetComponent<Spriteword>();
        
        sp.GenerateTask();
       
        textTaskChar = "";
        sp.textTaskOneWord.text = textTaskChar;
        normalChar = false;
        int i = 0;
        int random = buttonLetter.Length - 1;
        
        for (; i < buttonLetter.Length; i++)
        {
            if (Random.Range(i, buttonLetter.Length) == random && !normalChar)
            {
                normalChar = true;
                buttonLetter[i].GetComponentInChildren<Text>().text = sp.TextTask[numberLetter].ToString();
            }
            else
            {
                System.Random rand = new System.Random();
                char ch = (char)rand.Next(0x0061, 0x007A);
                if (ch == 'z')
                    ch = (char)((int)'a'+ i);
                else
                    ch = (char)((int)ch + i);
                buttonLetter[i].GetComponentInChildren<Text>().text = ch.ToString();
            }
        }
        numberLetter++;
    }
    int numberLetter = 0;
    bool normalChar;
    string textTaskChar;
    public void LetterInButton(string text)
    {if (text == "space")
            text = " ";
        textTaskChar= sp.textTaskOneWord.text + text;
        sp.textTaskOneWord.text = textTaskChar;
       
        if (numberLetter < sp.TextTask.Length)
        {
            Debug.Log(numberLetter);
            normalChar = false;
            for (int ind = buttonLetter.Length - 1; ind > 0; ind--)
            {
                var r = new System.Random();
                int j = r.Next(ind);
                var t = buttonLetter[ind];
                buttonLetter[ind] = buttonLetter[j];
                buttonLetter[j] = t;
            }
            for (int i = 0; i < buttonLetter.Length; i++)
            {
                Debug.Log("text2");
                if (i == 0)
                {
                    Debug.Log("text3");
                    normalChar = true;
                    buttonLetter[i].GetComponentInChildren<Text>().text = sp.TextTask[numberLetter].ToString();
                }
                else
                {
                    System.Random rand = new System.Random();
                  
                    char ch = (char)rand.Next(0x0061, 0x007A);
                    if (ch == 'z')
                        ch = (char)((int)'a' + i);
                    else
                        ch = (char)((int)ch + i);
                    buttonLetter[i].GetComponentInChildren<Text>().text = ch.ToString();
                }       
            }
            
            
            numberLetter++;
        }
        else
        {
            enter.interactable = true;
            IsButtonLetterInteractable(false);
        }
    }
    void buttonDoneLetter()
    {
        IsMainButtonsEnabled(false);
        StartCoroutine(closeLetter());
    }
    void Remove()
    {
        
        numberLetter-=2;
        if(numberLetter < 0)
        {
            numberLetter = 0;
        }
        LetterInButton("");
        sp.textTaskOneWord.text = sp.textTaskOneWord.text.Remove(sp.textTaskOneWord.text.Length - 1);
        
        IsButtonLetterInteractable(true);
        
    }
    void RemoveAll()
    {
        numberLetter =0;
        LetterInButton("");
        sp.textTaskOneWord.text ="";
        IsButtonLetterInteractable(true);
    }
    IEnumerator closeLetter()
    {
        yield return new WaitForSeconds(1);
        numberLetter = 0;

        
          StartCoroutine(DestroyInstWord(3));
        if (sp.textTaskOneWord.text == sp.TextTask)
        {
            _battleController.TimeGo = false;
            _battleController.CrashGem(25);
            _battleController.LerningModeId++;
            _counter = 0;
            
        }   
       
        else if(sp.textTaskOneWord.text != sp.TextTask)
        {
            _battleController.TimeGo = false;
            if(_battleController.infinitely)
            {
                
                StartCoroutine(_battleController.Repeat());
            }
            else
            {
                
                StartCoroutine(_battleController.ClosePanel());
                
            }
           
                  
        }
       
        
    }


    public IEnumerator DestroyInstWord(float destroyTime)
    {
        yield return new WaitForSeconds(destroyTime);
        if(_battleController.infinitely)
        {
            Destroy(instWord[_randomWord]);
        }
        else
        {
            Destroy(_instWordNotInfenetly[_randomWord]);
        }
        
    }
  
    void InstFrase()
    {
        _randomWord = Random.Range(0, _someWordsPrefabs.Length);
        _prefab = Instantiate(_someWordsPrefabs[_randomWord], new Vector2(20, -180), Quaternion.identity) as GameObject;
        _prefab.transform.SetParent(_someWordsParent.transform, false);
    }

    


    void MoveObject()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = GetComponent<RectTransform>().position.z;
        movingObject.transform.position = pos;
    }
   
    public void PostTaskAnswer()
    {
        TaskSum = "";
        for(int i = 0; i<Cl.Length;i++)
        {
            if (Cl[i].Answer != null)
            {
                TaskSum += " ";
                TaskSum += Cl[i].Answer;
            }
        }
     

        if(System.String.Compare(TaskSum, textTask, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.CompareOptions.IgnoreCase | System.Globalization.CompareOptions.IgnoreSymbols) == 0)
        {
            //_battleController.CrashGem(25);
        }
        else
        {
           StartCoroutine(_battleController.ClosePanel());
        }
       StartCoroutine(DestroyFrase());
    }


    private IEnumerator DestroyFrase()
    {
        yield return new WaitForSeconds(3);
            var obj = FindObjectsOfType<ButtonFrase>();
        foreach (ButtonFrase i in obj)
        {
            Destroy(i.gameObject);
        }
        position.gameObject.SetActive(false);
    }
    public bool canPressed;
    private void Update()
    {
        if (Input.GetMouseButton(0) && canPressed)
        {
            MoveObject();
        }
    }


    private void IsButtonLetterInteractable(bool IsInteractable)
    {
        for(int i = 0; i < buttonLetter.Length; i++)
        {
            buttonLetter[i].enabled = IsInteractable;
        }
    }


    private void IsMainButtonsEnabled(bool IsEnabled)
    {
        enter.GetComponent<Image>().enabled = IsEnabled;
        remove.enabled = IsEnabled;
        removeAll.GetComponent<Image>().enabled = IsEnabled;
    }

  
}
