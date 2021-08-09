using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SomeWords : MonoBehaviour
{
    [Header("Fields")]
    [SerializeField] private Transform[] fields;
    [SerializeField] private Text[] _textInField;
    [SerializeField] private GameObject[] _imageInField;

    [Header("Positions")]
    [Space(20f)]
    [SerializeField] private Vector3 _positionOfVariant;
    [SerializeField] private Vector3 _positionOfButtons;

    [Header("Inheritance")]
    private BattleController _battleController;
    private DoneAndMissed _doneAndMissed;

   


    [Header("Other")]
    [Space(20f)]
    [SerializeField] private List<int> _checkerAnswerConter = new List<int>();
    [SerializeField] private Transform _variantPanel;
    [SerializeField] private Image _questionMark;
    private  int _index;
    private int _questionCounter;
    public int _answerCounter;
   
    [SerializeField] private GameObject[] _variants;
    [SerializeField] private string[] _tasks;
    [SerializeField] private int[] _randomTask;
    [SerializeField] private Button _done;


    private void Start()
    {
        _battleController = FindObjectOfType<BattleController>();
        _doneAndMissed = FindObjectOfType<DoneAndMissed>();
        _done.onClick.AddListener(OnClickDoneButton);
        CreateRandom();
        for(int i = 0; i < _variants.Length; i++)
        {
            _variants[i].GetComponent<WordsButton>().Word.text = _tasks[_randomTask[i]];
        }
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


    public IEnumerator OnChooseVariant(int count)
    {
        yield return new WaitForSeconds(0.3f);
        _variantPanel.DOScale(new Vector3(0, 0, 0), 0.6f);
        ScalingVariants(0, false);
        yield return new WaitForSeconds(0.7f);
       
        _imageInField[_index].SetActive(true);
        _textInField[_index].text = _variants[count].GetComponent<WordsButton>().Word.text;
        _checkerAnswerConter.Add(_randomTask[count]);
        _questionCounter++;
           if(_index < fields.Length - 1)
        {
            _index++;
            _questionMark.transform.position = fields[_index].position;
            _variantPanel.position = fields[_index].position - _positionOfVariant;
            _variantPanel.DOScale(new Vector3(1, 1, 1), 0.6f);
            ScalingVariants(1, true);
            _variants[count].SetActive(false);
        }
      
       if(_questionCounter == fields.Length)
       {
           _questionMark.enabled = false;
       }
      
    }

    public void OnClickDoneButton()
    {
       for(int i = 0; i <  _checkerAnswerConter.Count - 1; i++)
       {
        if(_checkerAnswerConter[i] < _checkerAnswerConter[i + 1])
        {
            _answerCounter++;
            Debug.Log("Yes");
        }
        else
        {
            Debug.Log("No"); 
        }
       }
      
        
        
        CheckAnswer();  
       
    }

    private void CheckAnswer()
    {
        if(_answerCounter == _checkerAnswerConter.Count - 1)
        {
            Debug.Log("Correct");
            _doneAndMissed.ScaleGood(1, 460);
            StartCoroutine(_doneAndMissed.ChangeScale());
            StartCoroutine(StartCrashGem());
        }
        else
        {
            Debug.Log("Incorrect");
            _doneAndMissed.ScaleMissed(1, 460);
            StartCoroutine(_doneAndMissed.ChangeScaleMissed());
            StartCoroutine(StartClosePanel());
        }
    }

    private IEnumerator StartCrashGem()
    {
        yield return new WaitForSeconds(3);
        _doneAndMissed.ScaleGood(0, 360);
        _battleController.CrashGem(25);
    }

    private IEnumerator StartClosePanel()
    {
        yield return new WaitForSeconds(3);
        _doneAndMissed.ScaleMissed(0, 360);
        StartCoroutine(_battleController.ClosePanel());
    }

   



    private void ScalingVariants(int scale, bool isChangePosition)
    {
        for(int i = 0; i < _variants.Length; i++)
        {
            if(isChangePosition)
            {
                _variants[i].transform.DOScale(new Vector3(scale, scale, scale), 0.6f);
                _variants[i].transform.position += _positionOfButtons;
            }
            else
            {
                _variants[i].transform.DOScale(new Vector3(scale, scale, scale), 0.6f);
            }
            
        }
    }

}
