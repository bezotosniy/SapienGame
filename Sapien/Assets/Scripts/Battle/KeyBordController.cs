
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class KeyBordController : MonoBehaviour
{
    Inventory inv;
    string textTask;

    public Transform position;
    int ID;
    string[] keyLength;

    [Space]
    [Header("Char")]
    public Transform pointLetterInst;
    public Spriteword[] LetterList;  
    public Button[] buttonLetter;
    public Button space, remove, removeAll, enter;
    GameObject instWord;
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
    private void Start()
    {
        removeAll.onClick.AddListener(RemoveAll);
        enter.onClick.AddListener(buttonDoneLetter);
        remove.onClick.AddListener(Remove);
        EnterText.onClick.AddListener(PostTaskAnswer);
        position.gameObject.SetActive(false);
        inv = GetComponent<Inventory>();
    }
    public void KeyBoard(string Task)
    {
        textTask = Task;
        InstFrase();
      
    }

    public void InstWord(int id)
        
    {
        ID = id;
        enter.interactable = false;
        instWord = Instantiate(LetterList[ID].gameObject, transform);
        sp = instWord.GetComponent<Spriteword>();
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
            
        }
    }
    void buttonDoneLetter()
    {
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
    }
    void RemoveAll()
    {
        numberLetter =0;
        LetterInButton("");
        sp.textTaskOneWord.text ="";
    }
    IEnumerator closeLetter()
    {
        yield return new WaitForSeconds(1);
        numberLetter = 0;
        Destroy(instWord);
        if (sp.textTaskOneWord.text == sp.TextTask)
            inv.CrashGem((int)inv.damage);
        else
            inv.CrashGem(0);
    }
  
    void InstFrase()
    {
        int[] rand;
        
        Debug.Log("One");
        position.gameObject.SetActive(true);
        GameObject obj;
        keyLength = textTask.Split(' ');
        var r = new System.Random();
        for (int i = keyLength.Length - 1; i > 0; i--)
        {
            int j = r.Next(i);
            var t = keyLength[i];
            keyLength[i] = keyLength[j];
            keyLength[j] = t;
        }
        for (int i = 0; i < keyLength.Length; i++)
        {
            Debug.Log(keyLength[i]);
            Vector3 pos = new Vector3(position.position.x + i * OffsetFraseX, position.position.y, position.position.z);
            obj = Instantiate(longFrase, pos, Quaternion.identity);
            obj.transform.SetParent(position);
            obj.GetComponentInChildren<Text>().text = keyLength[i];
        }

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
            inv.CrashGem((int)inv.damage);
        }
        else
        {
            inv.CrashGem(0);
        }
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
}
