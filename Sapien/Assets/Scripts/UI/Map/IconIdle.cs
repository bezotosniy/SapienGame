using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class IconIdle : MonoBehaviour
{
    public float topOffset, botOffset;
    private float defaultY;
    
    public void StartIdle(QuestType type)
    {
        defaultY = this.GetComponent<RectTransform>().anchoredPosition.y;
        if (type == QuestType.StoryQuest)
        {
            StartCoroutine(Rotator());
        }
        StartCoroutine(Idle());
    }

    IEnumerator Idle()
    {
        RectTransform rect = GetComponent<RectTransform>();
        float elapsedTime = 0, duration = 2, speed = (topOffset) / duration;
        while (true)
        {
            elapsedTime = 0;
            while (elapsedTime < duration / 2)
            {
                rect.anchoredPosition += new Vector2(0, speed * Time.deltaTime);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            while (elapsedTime < duration)
            {
                rect.anchoredPosition -= new Vector2(0, speed * Time.deltaTime);

                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }

    IEnumerator Rotator()
    {
        RectTransform rect = GetComponent<RectTransform>();
        float elapsedTime = 0, duration = 1, angle = 15;
        rect.DORotate(new Vector3(0 , 0 , -angle) ,duration/4);
        yield return new WaitForSeconds(duration / 4);
        while (true)
        {
            rect.DORotate(new Vector3(0 , 0 , angle) ,duration/2);
            yield return new WaitForSeconds(duration / 2);
            
            rect.DORotate(new Vector3(0 , 0 , -angle) ,duration/2);
            yield return new WaitForSeconds(duration / 2);
            
            
        }
    }

}
