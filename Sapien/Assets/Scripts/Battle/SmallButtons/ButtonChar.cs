using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonChar : MonoBehaviour, IPointerClickHandler
{
    Text text;
    KeyBordController kb;
    void Start()
    {
        kb = FindObjectOfType<KeyBordController>();
        text = GetComponentInChildren<Text>();
    }
    public void OnClick()
    {
        kb.LetterInButton(text.text);
    }
 
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = Color.green;
        StartCoroutine(ChangeColor());
    }

    private IEnumerator ChangeColor()
    {
        yield return new WaitForSeconds(0.15f);
         gameObject.GetComponent<Image>().color = Color.white;
    }
}
