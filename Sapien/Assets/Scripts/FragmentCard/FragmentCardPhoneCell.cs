using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class FragmentCardPhoneCell : MonoBehaviour
{
    public CardInfo cardInfo;
    public GameObject particle;
    public Vector3 particleRotation;

    public bool locked = true;
    private static FragmentCardPhoneCell[] cardPhoneCells = new FragmentCardPhoneCell[102];
    private static int cellsCNT = 0;
    public static event Action onLastCellLoaded;
    private void Awake()
    {
        AddCardToArray();
    }

    private void OnEnable()
    {
        OpenCard();
    }

    private void AddCardToArray()
    {
        //while (cardPhoneCells.Count <= cardInfo.cardID + 1)
        //{
        //    cardPhoneCells.Add(null);
        //}
        cardPhoneCells[cardInfo.cardID] = this;
        cellsCNT++;
        if (cellsCNT == 20)
            onLastCellLoaded?.Invoke();
    }

    public static FragmentCardPhoneCell GetCardCellByID(int id)
    {
        if (id < cardPhoneCells.Length)
            return cardPhoneCells[id];
        return null;
    }
    
    public void OpenCard()
    {
        if (!locked)
            GetComponent<Image>().sprite = cardInfo.cardSprite;
        else if (this.gameObject.GetComponent<Image>().sprite.name != "UnchosenCard")
            OpenCardFirstly();
        
    }

    public void OpenCardFirstly()
    {
        locked = false;
        StartCoroutine(MakeCardOpaque());
        StartCoroutine(BigCardAnim());
    }

    IEnumerator BigCardAnim()
    {
        Transform par = this.transform.parent.parent.parent.parent;

        GameObject card = Instantiate(new GameObject() , par);
        Image cardSprite = card.AddComponent<Image>();
        RectTransform tr = card.GetComponent<RectTransform>();
        cardSprite.sprite = cardInfo.cardSprite;
        cardSprite.preserveAspect = true;
        tr.sizeDelta = new Vector2(800,450);
        float elapsedTime = 0;
        RectTransform thisRect = this.gameObject.GetComponent<RectTransform>();
        
        GameObject instantiated = Instantiate(particle , par.position , Quaternion.Euler(-45,-90 , 90), par);
        GameObject instantiated1 = Instantiate(particle , par.position , Quaternion.Euler(-135,-90 , 90), par);
        
        instantiated.GetComponent<RectTransform>().anchoredPosition = new Vector2(-300 ,-150);
        instantiated1.GetComponent<RectTransform>().anchoredPosition = new Vector2(300 ,-150);
        instantiated.GetComponentInChildren<ParticleSystem>().Play();
        instantiated1.GetComponentInChildren<ParticleSystem>().Play();
        //instantiated.GetComponentInChildren<ParticleSystem>().loop = true;
        //instantiated1.GetComponentInChildren<ParticleSystem>().loop = true;
        foreach (Transform trans in instantiated.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = LayerMask.NameToLayer("UI");
        }
        foreach (Transform trans in instantiated1.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = LayerMask.NameToLayer("UI");
        }
        
        
        yield return new WaitForSecondsRealtime(4);
        
        Destroy(instantiated);
        Destroy(instantiated1);
        
        while (elapsedTime < 2)
        {
            tr.sizeDelta = Vector2.Lerp(tr.sizeDelta, thisRect.sizeDelta, Time.deltaTime * 4);
            tr.position = Vector3.Lerp(tr.position, thisRect.position, Time.deltaTime * 4);
            
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(card);
        FindObjectOfType<StoryManager>().ShowCardInfo(this);
        Image cardImage = gameObject.GetComponent<Image>();
        cardImage.sprite = cardInfo.cardSprite;
    }
    
    IEnumerator MakeCardOpaque()
    {
        Image cardImage = gameObject.GetComponent<Image>();
        //cardImage.sprite = cardInfo.cardSprite;
        //Color loc = cardImage.color;
        //loc.a = 0;
        //cardImage.color = loc;
        
        LayerMask mask = LayerMask.NameToLayer("UI");
        
        
        //yield return new WaitForSeconds(0.7f);

        //float speed = 1;
        //while (cardImage.color.a < 1)
        //{
        //    Color nextColor = cardImage.color;
        //    nextColor.a = Mathf.Clamp01(nextColor.a + Time.deltaTime * speed);
        //    cardImage.color = nextColor;
        //    yield return new WaitForEndOfFrame();
        //}

        yield return new WaitForSeconds(2f);
        
        
    }
}
