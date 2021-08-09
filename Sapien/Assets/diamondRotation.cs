using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class diamondRotation : MonoBehaviour
{
    private void Start()
    {
          gameObject.GetComponent<MeshRenderer>().material.DOColor(Color.blue, 0.01f);
    }
    private void Update()
    {
        transform.rotation *= Quaternion.Euler(0f, 50f*Time.deltaTime, 0f);
    }
}
