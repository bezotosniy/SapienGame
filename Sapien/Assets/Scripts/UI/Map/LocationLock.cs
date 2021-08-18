using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LocationLock : MonoBehaviour
{
    [SerializeField] private Image[] lockers;
    public AnimationCurve speedByTimeCoef;
    
    public GameObject particles;
    public float particleScale;

    [HideInInspector]public RectTransform locationTransform;
    
    
    private Vector2[] directions;
    
    private void Start()
    {
        
    }

    public void SpawnParticle()
    {
        GameObject particle = Instantiate(particles, this.transform);
        foreach (Transform trans in particle.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = LayerMask.NameToLayer("ParticlesUI");
        }
        particle.transform.localScale = Vector3.one * particleScale;

        Destroy(particle , 50);
    }
    
    public void Unlock()
    {
        directions = GetDirections();
        for(int i = 0; i < lockers.Length; ++i)
        {
            StartCoroutine(MoveLocker(lockers[i] , directions[i] , Random.Range(500 , 900)));
        }
    }

    IEnumerator MoveLocker(Image locker , Vector2 direction , float speed)
    {
        float elapsedTime = 0, duration = 10;
        //locker.rectTransform.DOMove();
        Image cloud = Instantiate(locker.gameObject, locker.transform.parent).GetComponent<Image>();
        locker.gameObject.SetActive(false);
        Debug.Log(direction);
        locker.gameObject.name = "Moving locker " + direction;
        while (elapsedTime < duration / 5)
        {
            cloud.rectTransform.anchoredPosition -= ((speed / 10) * Time.deltaTime * (speedByTimeCoef.Evaluate(elapsedTime / (duration/5))) * direction);
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0;

        while (elapsedTime < duration)
        {
            cloud.rectTransform.anchoredPosition += (speed * Time.deltaTime * speedByTimeCoef.Evaluate(elapsedTime / (duration)) * direction);
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        Destroy(cloud.gameObject);

    }

    public Vector2[] GetDirections()
    {
        Vector2[] result = new Vector2[lockers.Length];

        for (int i = 0; i < result.Length; ++i)
            result[i] = locationTransform.anchoredPosition.normalized;
        
        //float curAngle = Random.Range(0, 360), step = 360f / lockers.Length;
        //for(int i = 0; i < result.Length; ++i)
        //{
        //    result[i] = new Vector2(Mathf.Cos(Mathf.Deg2Rad * curAngle), Mathf.Sin(Mathf.Deg2Rad * curAngle));
        //    curAngle += step;
        //}

        //Vector2 center = Vector2.zero;
        //foreach (var locker in lockers)
        //{
        //    center += locker.rectTransform.anchoredPosition;
        //}
        //
        //center /= lockers.Length;
        //
        //for (int i = 0; i < lockers.Length; ++i)
        //{
        //    result[i] = (lockers[i].rectTransform.anchoredPosition - center).normalized;
        //}

        return result;
    }
    
}
