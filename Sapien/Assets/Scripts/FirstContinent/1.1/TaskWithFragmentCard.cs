using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;
using DG.Tweening;

public class TaskWithFragmentCard : MonoBehaviour
{
  [Header("Inheratence")]
  [SerializeField] private VoiceRegontion2 _voiceRecognition;
  [SerializeField] private VoicePlayBack _voicePlayback;
  [SerializeField] private UIControllerForCurScene _uiController;
  [SerializeField] private DoubleDialogUI _doubleUIContoller;  
  [Header("Prefabs")]
  [Space(20f)]
  [SerializeField] private GameObject _textPrefab;
  [SerializeField] private GameObject[] _fragmentcardPrefab;
  [SerializeField] private string[] _prefabsText;

  [Header("Logic")]
  [Space(20f)]
  [SerializeField] private List<GameObject> _prefabs = new List<GameObject>();
  [SerializeField] private GameObject _parent;
  [SerializeField] private Image _dialogBackGround;

  [Header("Fragment Card Prefab Propeties")]
  [Space(20f)]
  [SerializeField] private Sprite[] _fragmentCardImage;
  [SerializeField] private string[] _fragmentcardText;
  [SerializeField] private AudioClip[] _sounds;

  [Header("Other")]
  [Space(20f)]
  public int index = 0;
  

  private void Start()
  {
     /*InitializeTask(_prefabsText[0], 0, 0);
    InitializePrefab(_fragmentcardPrefab[0], 0, 1);
    InitializeTask(_prefabsText[1], 1, 2);
    InitializePrefab(_fragmentcardPrefab[1], 1, 3);
    InitializeTask(_prefabsText[2], 2, 4);
    gameObject.SetActive(true);
    StartCoroutine(StartSpeak());*/
  }

   
  public IEnumerator SpawnTask()
  {
    StartCoroutine(InterlocutorSay());
    yield return new WaitForSeconds(6f);
    InitializeTask(_prefabsText[0], 0, 0);
    InitializePrefab(_fragmentcardPrefab[0], 0, 1);
    InitializeTask(_prefabsText[1], 1, 2);
    InitializePrefab(_fragmentcardPrefab[1], 1, 3);
    InitializeTask(_prefabsText[2], 2, 4);
    _parent.SetActive(true);
    _uiController._microphonePanel.SetActive(true);
    _uiController.SpeakUI();
    StartCoroutine(StartSpeak());
  }

  private void InitializeTask(string Task, int index, int info)
  {
    if(Task != string.Empty)
    {
      GameObject TextPrefab = Instantiate(_textPrefab, transform.position, Quaternion.identity);
      TextPrefab.GetComponent<Text>().text = Task;
      TextPrefab.transform.SetParent(_parent.transform);
      _prefabs.Add(TextPrefab);
      if(info == 0)
      {
        float x = transform.GetComponent<RectTransform>().anchoredPosition.x - transform.GetComponent<RectTransform>().sizeDelta.x / 2;
        x += (TextPrefab.GetComponent<RectTransform>().sizeDelta.x / 2);
        Debug.Log(x);
        TextPrefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, TextPrefab.GetComponent<RectTransform>().anchoredPosition.y);
      }
      else
      {
        float x = _prefabs[_prefabs.Count - 2].GetComponent<RectTransform>().anchoredPosition.x + _prefabs[_prefabs.Count - 2].GetComponent<RectTransform>().sizeDelta.x / 1.75f;
        x += (TextPrefab.GetComponent<RectTransform>().sizeDelta.x / 2);
        Debug.Log(x);
        _prefabs[_prefabs.Count - 1].GetComponent<RectTransform>().anchoredPosition = new Vector2(x, TextPrefab.GetComponent<RectTransform>().anchoredPosition.y);
      }
      
    }
  }

  private void InitializePrefab(GameObject Fragmentcard, int index, int info)
  {
    if(Fragmentcard != null)
    {
      GameObject FragmentCard = Instantiate(Fragmentcard, transform.position, Quaternion.identity);
      FragmentCardVoiceRecognition fragmentCardVoiceRecognition = FragmentCard.GetComponent<FragmentCardVoiceRecognition>();
      fragmentCardVoiceRecognition._fragmentcardImage.sprite = _fragmentCardImage[index];
      fragmentCardVoiceRecognition._backSideText.text = _fragmentcardText[index];
      fragmentCardVoiceRecognition._audioSorce.clip = _sounds[index];
      FragmentCard.transform.SetParent(_parent.transform);
      _prefabs.Add(FragmentCard);
  
      if(info == 0)
      {
        float x = transform.GetComponent<RectTransform>().anchoredPosition.x - transform.GetComponent<RectTransform>().sizeDelta.x / 2;
        x += (Fragmentcard.GetComponent<RectTransform>().sizeDelta.x / 2);
        Debug.Log(x);
        Fragmentcard.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, Fragmentcard.GetComponent<RectTransform>().anchoredPosition.y);
      }
      else
      {
        float x = _prefabs[_prefabs.Count - 2].GetComponent<RectTransform>().anchoredPosition.x + _prefabs[_prefabs.Count - 2].GetComponent<RectTransform>().sizeDelta.x / 1.75f;
        x += (Fragmentcard.GetComponent<RectTransform>().sizeDelta.x / 2);
        Debug.Log(x);
       _prefabs[_prefabs.Count - 1].GetComponent<RectTransform>().anchoredPosition = new Vector2(x, Fragmentcard.GetComponent<RectTransform>().anchoredPosition.y);
      }
    }
  }

 private IEnumerator StartSpeak()
  {
    yield return new WaitForSeconds(1);
    _voiceRecognition.StartRecordButtonOnClickHandler();
    _uiController.SpeakUI();
  }

private IEnumerator InterlocutorSay()
  {
    yield return new WaitForSeconds(4);
    _voicePlayback.AudioInterlocutor[_voicePlayback.AudioCount].Play();
    _uiController.InterLocutorSaid();
    _doubleUIContoller._doublePanel.SetActive(false);
    _uiController._microphonePanel.SetActive(false);
  }

    public IEnumerator OnCorrect()
    {
      yield return new WaitForSeconds(1);
      StartCoroutine(ChangeCorrectColor());
      CloseImage();
    }


public IEnumerator ChangeCorrectColor()
    {
     
       yield return new WaitForSeconds(0.3f);
        _dialogBackGround.DOColor(Color.green, 0.3f);
        _dialogBackGround.DOFade(0.5f, 0.3f);
         if(index == 3)
        {
          index = 0;
          yield break;
        }
       StartCoroutine(ChangeCorrectSecond());
    }
public IEnumerator ChangeCorrectSecond()
    {
      
        yield return new WaitForSeconds(0.3f);
        _dialogBackGround.DOColor(Color.white, 0.3f);
        _dialogBackGround.DOFade(1f, 0.3f);
        index++;
        StartCoroutine(ChangeCorrectColor());
    }



public IEnumerator InCorrect()
{
  yield return new WaitForSeconds(0.5f);
  StartCoroutine(ChangeIncorectColor());
  CloseImage();
}


public IEnumerator ChangeIncorectColor()
{
  yield return new WaitForSeconds(0.7f);
  _dialogBackGround.DOColor(Color.red, 0.6f);
  _dialogBackGround.DOFade(0.5f, 0.6f);
  if(_voicePlayback.IsMistake == false)
  {
           
     yield break;
            
  }
  StartCoroutine(ChangeInCorrectSecond());
}
    
public IEnumerator ChangeInCorrectSecond()
{
  yield return new WaitForSeconds(0.7f);
  _dialogBackGround.DOColor(Color.white, 0.6f);
 _dialogBackGround.DOFade(1, 0.6f);
  index++;
  StartCoroutine(ChangeIncorectColor());
  }

private void CloseImage()
{
  
  for(int i = 0; i < _prefabs.Count; i++)
  {
    if( _prefabs[i].GetComponent<FragmentCardVoiceRecognition>() != null)
    {
      _prefabs[i].transform.DOScale(new Vector3(0, 0, 0), 0.5f);
    }
  }
}
}



