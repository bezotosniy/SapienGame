using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class DigQuest : QuestAfterStoryQuest
{
    [FormerlySerializedAs("RequireClicks")] public int requireClicks;
    [FormerlySerializedAs("ClickCoolDown")] public float clickCoolDown = 0;
    public DirtPile dirtPile;
    public Transform playerPosition;
    
    public TranslationTaskUI TaskUI; 
    
    
    private int _currentClickCnt = 0;
    private float _lastClickTime = 0;
    private Animator anim , playerAnim;
    private bool isClicked;
    private Camera mainCamera;
    private CinemachineStateDrivenCamera cameraBlender;
    private MovingPoint playerController;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OpenQuest();
            if (activated || Activate())
            {
                ChangePerspective(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChangePerspective(false);
        }
    }

    private void ChangePerspective(bool DigPerspective)
    {
        if (DigPerspective)
        {
            playerController.GoTo(playerPosition.position);
            playerController.transform.DORotate(Quaternion.LookRotation(playerPosition.forward).eulerAngles , 1);
            cameraBlender.GetComponent<Animator>().SetBool("DigQuest" , true);
        }
        else
        {
            cameraBlender.GetComponent<Animator>().SetBool("DigQuest" , false);
        }
    }

    private void Start()
    {
        mainCamera = Camera.main;
        anim = GameObject.Find("Player").GetComponentInChildren<Animator>();
        playerAnim = GameObject.FindObjectOfType<MovingPercon>().GetComponentInChildren<Animator>();
        cameraBlender = FindObjectOfType<CinemachineStateDrivenCamera>();
        playerController = FindObjectOfType<MovingPoint>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isClicked = true;
        }
    }

    public override bool Activate()
    {
        if (base.Activate())
        {
            StartCoroutine(QuestLogic());
            return true;
        }

        return false;
    }

    IEnumerator QuestLogic()
    {
        RaycastHit hit;
        while (_currentClickCnt < requireClicks)
        {
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit) &&
                Time.time - _lastClickTime >= clickCoolDown)
            {
                Debug.Log(hit.transform.gameObject + " : " + dirtPile.dirtPile1.gameObject+ " " + isClicked);
                if (isClicked)
                {
                    if (dirtPile.dirtPile1.gameObject == hit.transform.gameObject)
                        AfterClick();
                }
            }

            isClicked = false;
            yield return new WaitForFixedUpdate();
        }    
        GiveTranslationTask();
        Debug.Log($"Completed dig mission");
    }

    public void GiveTranslationTask()
    {
        TaskUI.gameObject.SetActive(true);
        TaskUI.UpdateTask(TranslateQuestions.GetRandomQuestion());
        TaskUI.OnAnswerGive += OnAnswerGiven;
    }

    public void OnAnswerGiven(bool result)
    {
        if (result)
        {
            QuestComplete();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    public override void QuestComplete()
    {
        base.QuestComplete();
        ChangePerspective(false);
        GetComponent<BoxCollider>().enabled = false;
        StartCoroutine(RespawnMission());
        //Destroy(this.gameObject);
    }

    IEnumerator RespawnMission()
    {
        float respawnTime = 10;
        yield return new WaitForSeconds(respawnTime);
        questName = questName+"_";
        availible = true;
        GetComponent<BoxCollider>().enabled = true;
        _currentClickCnt = 0;
        dirtPile.Reset();
    }

    public void AfterClick()
    {
        isClicked = false;
        _currentClickCnt++;
        anim.SetTrigger("Dig");
        dirtPile.EraseDirt(1f / requireClicks);
        Debug.Log($"{_currentClickCnt} clicked , require {requireClicks}");
        _lastClickTime = Time.time;
    }
}
