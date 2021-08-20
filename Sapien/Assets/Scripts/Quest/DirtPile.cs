using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtPile : MonoBehaviour
{
    private List<GameObject> dirt = new List<GameObject>();

    private void Start()
    {
        foreach (var gm in GetComponentsInChildren<Transform>())
        {
            dirt.Add(gm.gameObject);
        }
        dirt.Remove(this.gameObject);
    }

    public void EraseDirt(float normalizedCount)
    {
        int countToErase = (int)(dirt.Count * normalizedCount);
        for (int i = dirt.Count - 1; i >= 0; --i)
        {
            dirt[i].SetActive(false);
            //Destroy(dirt[i]);
            dirt.RemoveAt(i);   
        }
    }
}
