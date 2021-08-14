using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationLock : MonoBehaviour
{
    [SerializeField] public Image _lock;

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
