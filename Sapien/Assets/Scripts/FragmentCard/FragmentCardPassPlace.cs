using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class FragmentCardPassPlace : MonoBehaviour
{
    public CardInfo targetCard;
    public GameObject completeParticle , bossParticle , playerSpellParticle , warningPanel;
    
    private Ray ray;
    private bool isClicked;
    private Animator anim;
    private Coroutine CR_Passing;
    Transform leftHand, rightHand;

    private void Start()
    {
        anim = GameObject.Find("Player").GetComponentInChildren<Animator>();
        leftHand = GameObject.Find("Player").transform.FindDeepChild("Hand_L");
        rightHand = GameObject.Find("Player").transform.FindDeepChild("Hand_R");
        
        //anim.SetTrigger("Dance");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            isClicked = true;
    }

    private void FixedUpdate()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!GameManager.Instance.IsPhoneOpened && Physics.Raycast(ray, out hit))
        {
            if ((hit.transform.IsChildOf(this.gameObject.transform) || hit.transform == this.transform) && isClicked)
            {
                isClicked = false;
                Debug.Log("MouseOverPowerPlace");

                if (targetCard == FragmentCard.instance.cardInfo && Mathf.Approximately(FragmentCard.instance.GetEnergyNormalized() ,1f))
                {
                    if (CR_Passing == null)
                        StartCoroutine(PassCard());
                }
                else
                {
                    StartCoroutine(PassCardFailed());
                }
            }
        }
        isClicked = false;
    }

    IEnumerator PassCard()
    {
        if ((targetCard.cardID + 1) % 5 == 0)
        {
            CR_Passing = StartCoroutine(PassBossCard());
            yield break;
        }
        anim.SetTrigger("Dance"); // 3s
        GameObject part = Instantiate(completeParticle, this.transform);
        
        yield return new WaitForSecondsRealtime(2f);
        
        Vector3 castPosition = Vector3.Lerp(leftHand.position, rightHand.position, 0.5f);
        GameObject playerSpell = Instantiate(playerSpellParticle, castPosition
            ,Quaternion.LookRotation((this.transform.position - castPosition).normalized),leftHand);
        playerSpell.name = "SPELL PARTICLE";

        float elapsedTime = 0;

        while (elapsedTime < 4)
        {
            castPosition = Vector3.Lerp(leftHand.position, rightHand.position, 0.5f);
            playerSpell.transform.rotation = Quaternion.LookRotation((this.transform.position - castPosition).normalized);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }


        //yield return new WaitForSecondsRealtime(5f);
        Destroy(playerSpell);
        Destroy(part);
        FragmentCard.instance.OnCardGiveOff();
        CR_Passing = null;
    }

    IEnumerator PassBossCard()
    {
        Debug.Log("Boss fight");
        anim.SetTrigger("Dance"); // 3s
        GameObject part = Instantiate(completeParticle, this.transform);
        GameObject bossPart = Instantiate(bossParticle, this.transform);

        
        yield return new WaitForSecondsRealtime(2f);
        Vector3 castPosition = Vector3.Lerp(leftHand.position, rightHand.position, 0.5f);
        
        GameObject playerSpell = Instantiate(playerSpellParticle, castPosition
            ,Quaternion.LookRotation((this.transform.position - castPosition).normalized),leftHand);
        playerSpell.name = "SPELL PARTICLE";
        
        float elapsedTime = 0;

        while (elapsedTime < 4)
        {
            castPosition = Vector3.Lerp(leftHand.position, rightHand.position, 0.5f);
            playerSpell.transform.rotation = Quaternion.LookRotation((this.transform.position - castPosition).normalized);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Destroy(part);
        Destroy(bossPart);
        Destroy(playerSpell);
        FragmentCard.instance.OnCardGiveOff();
        CR_Passing = null;
    }

    IEnumerator PassCardFailed()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        GameObject warning = Instantiate(warningPanel , canvas.gameObject.transform);
        warning.GetComponent<Animator>().SetTrigger("ShowWarning");

        yield return new WaitForSecondsRealtime(5);
        
        Destroy(warning);
    }
}
