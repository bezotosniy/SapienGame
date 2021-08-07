using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoTask: MonoBehaviour
{
    public RectTransform transform;

    void Start()
    {
        transform = GetComponent<RectTransform>();
    }

    void Update()
    {
        //transform.position;        
    }
}
