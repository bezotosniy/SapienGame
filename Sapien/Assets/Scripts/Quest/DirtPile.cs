using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DirtPile : MonoBehaviour
{
    public Transform dirtPile1, dirtPile2;
    private Vector3 startScale1, startScale2;
    
    private void Start()
    {
        startScale1 = dirtPile1.localScale;
        startScale2 = dirtPile2.localScale;
    }

    public void EraseDirt(float normalizedCount)
    {
        Vector3 eraseAmount = startScale1 * normalizedCount;
        //eraseAmount.x = eraseAmount.z = 0;
        if ((dirtPile1.localScale - eraseAmount).y < 0)
            eraseAmount = dirtPile1.localScale;
        dirtPile1.DOScale(dirtPile1.localScale - eraseAmount, 0.5f);
        dirtPile2.DOScale(dirtPile2.localScale + eraseAmount, 0.5f);
    }

    public void Reset()
    {
        dirtPile1.DOScale(startScale1, 0.5f);
        dirtPile2.DOScale(startScale2, 0.5f);
    }
}
