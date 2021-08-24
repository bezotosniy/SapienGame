using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAudioController : MonoBehaviour
{
   [SerializeField] private Sound[] _sounds;


   public void PlaySound(int index)
   {
        _sounds[index].PlaySound();
   }
}
