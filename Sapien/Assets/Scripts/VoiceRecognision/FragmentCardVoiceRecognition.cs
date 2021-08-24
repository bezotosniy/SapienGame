using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;
using DG.Tweening;


public class FragmentCardVoiceRecognition : MonoBehaviour
{
    public Image _fragmentcardImage;
    public Text _backSideText;
    public AudioSource _audioSorce;
    private VoiceRegontion2 _voiceRecognition;
    private UIControllerForCurScene _uiController;
    [SerializeField] private Transform _backsidePanel;
    [SerializeField] private GameObject _panelForClick;
    [SerializeField] private Image _closeImage;
    private Vector3 _startPosition;
    private bool _isBackPanelClicked = true;
    private  bool _isPanelClicked = true;
    public int _clickCount;
 

    private void Start()
    {
        _voiceRecognition = FindObjectOfType<VoiceRegontion2>();
        _uiController = FindObjectOfType<UIControllerForCurScene>();
        _startPosition = transform.position;
    }

    public void MoveCardToTheCenter()
    {
        
        if(_isPanelClicked && _clickCount < 2)
        {
            _clickCount++;
            _voiceRecognition.StopRecordButtonOnClickHandler();
            _uiController.OnPlayVoice();
            var seq = DOTween.Sequence();
            seq.Append(transform.DOMove(new Vector3(1000, 600, 0), 1f));
            seq.Append(transform.DOScale(new Vector3(5, 5, 5), 0.5f));
            _panelForClick.SetActive(false);
            _closeImage.enabled = true;
            _isPanelClicked = false;
        }
       
    }

    public void OpenBackPanel()
    {
        if(_isBackPanelClicked)
        {
            _voiceRecognition.comboCount = 0;
            _voiceRecognition.SetComboAndBest();
            _backsidePanel.DOScale(new Vector3(1, 1, 1), 0.6f);
            StartCoroutine(CloseAll());
            _isBackPanelClicked = false;
            _closeImage.enabled = false;
        }
      
    }

    public void OnClickCloseButton()
    {
        OnAnimation(1, 0);
        _panelForClick.SetActive(true);
        _closeImage.enabled = false;
        StartCoroutine(StartRecord());
        StartCoroutine(EnabledClick());
    }

    private IEnumerator EnabledClick()
    {
        yield return new WaitForSeconds(0.5f);
        _isBackPanelClicked = true;
        _isPanelClicked = true;
    }

    private IEnumerator StartRecord()
    {
        yield return new WaitForSeconds(2.5f);
        _voiceRecognition.StartRecordButtonOnClickHandler();
        _uiController.SpeakUI();
    }


    private IEnumerator CloseAll()
    {
        yield return new WaitForSeconds(1);
        _audioSorce.Play();
        yield return new WaitForSeconds(_audioSorce.clip.length + 3f);
        OnAnimation(1, 0);
        _panelForClick.SetActive(true);
        _closeImage.enabled = false;
        yield return new WaitForSeconds(2.5f);
        StartCoroutine(EnabledClick());
        _voiceRecognition.StartRecordButtonOnClickHandler();
        _uiController.SpeakUI();
        
    }


    private void OnAnimation(int CardScale, int BackSideScale)
    {
        var seq = DOTween.Sequence();
        seq.Append(transform.DOScale(new Vector3(CardScale, CardScale, CardScale), 0.5f));
        seq.Join(_backsidePanel.DOScale(new Vector3(BackSideScale, BackSideScale, BackSideScale), 0.6f));
        seq.Append(transform.DOMove(_startPosition, 1f));
    }

}
