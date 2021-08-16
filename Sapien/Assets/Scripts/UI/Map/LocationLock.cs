using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LocationLock : MonoBehaviour
{
    [SerializeField] public Image _lock;
    public GameObject particles;
    public float particleScale;
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
        StartCoroutine(UnlockLocation());
    }

    IEnumerator UnlockLocation()
    {
        _lock.rectTransform.localPosition += Vector3.right * 100;
        yield return null;
    }
}
