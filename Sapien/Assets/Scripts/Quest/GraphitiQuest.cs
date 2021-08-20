using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class GraphitiQuest : QuestAfterStoryQuest
{
    public bool nearGraphiti;
    public float timeToFullErase;
    public Transform playerPosition;

    private float elapsedTime;
    
    private Vector3 lastPos;
    private Animator playerAnim , cameraAnim;
    private MovingPoint playerController;
    private CanvasGroup CanvasGroup;
    private bool pointerInImage;
    private CinemachineStateDrivenCamera cameraController;
    
    private void Start()
    {
        playerController = FindObjectOfType<MovingPoint>();
        playerAnim = GameObject.FindObjectOfType<MovingPercon>().GetComponentInChildren<Animator>();
        cameraController = FindObjectOfType<CinemachineStateDrivenCamera>();
        CanvasGroup = GetComponentInChildren<CanvasGroup>();
        cameraAnim = cameraController.gameObject.GetComponent<Animator>();
    }

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

    private void Update()
    {
        if (activated && nearGraphiti)
        {
            if (pointerInImage)
            {
                UpdateImage((Input.mousePosition - lastPos).magnitude);
                lastPos = Input.mousePosition;
            }

            if (CanvasGroup.alpha == 0)
            {
                QuestComplete();
            }
        }
    }

    public override void QuestComplete()
    {
        base.QuestComplete();
        ChangePerspective(false);
        Destroy(this.gameObject);
    }

    private void ChangePerspective(bool GraphitiPerspective)
    {
        if (GraphitiPerspective)
        {
            playerController.GoTo(playerPosition.position);
            playerController.transform.DORotate(Quaternion.LookRotation(CanvasGroup.transform.forward).eulerAngles , 1);
            nearGraphiti = true;
            playerAnim.SetBool("Cleaning", true);
            cameraAnim.SetBool("GraphitiQuest" , true);
        }
        else
        {
            nearGraphiti = false;
            playerAnim.SetBool("Cleaning" , false);
            cameraAnim.SetBool("GraphitiQuest" , false);
        }
    }

    private void UpdateImage(float path)
    {
        if (path > 0)
        {
            Debug.Log($"Move");
            elapsedTime = Mathf.Clamp(elapsedTime + Time.deltaTime , 0 , timeToFullErase);
            CanvasGroup.alpha = 1 - (elapsedTime / timeToFullErase);
        }
    }
    
    public void OnPointerEnter()
    {
        pointerInImage = true;
        Debug.Log($"Pointer enter");
    }
    
    public void OnPointerExit()
    {
        pointerInImage = false;
        Debug.Log($"Pointer exit");
    }
}
