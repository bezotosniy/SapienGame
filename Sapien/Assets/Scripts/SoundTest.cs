using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
    [SerializeField] private Sound testSound;

    private void Start()
    {
        testSound.PlaySound();
    }
}
