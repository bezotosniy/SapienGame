using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using  UnityEngine.SceneManagement;

[System.Serializable]
public class Sound
{
    public string name = "Unknown";
    public float volume = 1 , pitch = 1;
    public AudioMixerGroup group;
    public AudioClip clip;
    public bool loop = false , mode3d = false , playOnAwake = true;
    public void PlaySound()
    {
        AudioManager.Instance.PlaySound(this);
    }

    public void FillAudioSourceParams(ref AudioSource src)
    {
        src.volume = volume;
        src.pitch = pitch;
        src.loop = loop;
        src.clip = clip;
        src.outputAudioMixerGroup = group;
        src.spatialBlend = (mode3d) ? 1 : 0;
        src.playOnAwake = playOnAwake;
    }
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    [SerializeField] AudioMixerGroup musicGroup, effectsGroup;
    [Header("Music")] 
    [SerializeField] Sound[] music;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        musicGroup.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, PlayerPrefs.GetFloat("MusicVolume")));
        effectsGroup.audioMixer.SetFloat("EffectsVolume", Mathf.Lerp(-80, 0, PlayerPrefs.GetFloat("EffectsVolume")));
        
        if (gameObject.GetComponent<AudioSource>() == null)
        {
            gameObject.AddComponent<AudioSource>();
        }
    }


    IEnumerator FadeSwitchMusic(Sound toChange , float duration ,AudioSource src)
    {
        float halfDur = duration / 2;
        while (halfDur > 0)
        {
            src.volume = Mathf.Lerp(0, 1, halfDur * 2 / duration);
            yield return new WaitForSecondsRealtime(0.33f);
            halfDur -= 0.33f;
        }

        src.volume = 0;
        toChange.FillAudioSourceParams(ref src);
        src.Play();
        halfDur = duration / 2;
        while (halfDur > 0)
        {
            src.volume = Mathf.Lerp(0, 1,  1 - (halfDur * 2 / duration));
            yield return new WaitForSecondsRealtime(0.33f);
            halfDur -= 0.33f;
        }
        src.volume = 1;
    }
    
    public void PlaySound(Sound sound)
    {
        GameObject srcObj = new GameObject();
        StartCoroutine(PlayAndDeleteSound(srcObj, sound));
    }

    IEnumerator PlayAndDeleteSound(GameObject soundSource , Sound sound)
    {
        AudioSource src = soundSource.AddComponent<AudioSource>();
        sound.FillAudioSourceParams(ref src);
        src.Play();
        while (src.isPlaying)
        {
            yield return new WaitForSecondsRealtime(0.3f);
        }

        Destroy(soundSource);
    }

    public void PlaySoundAtPoint(Sound sound, Vector3 point)
    {
        GameObject srcObj = new GameObject();
        srcObj.transform.position = point;
        StartCoroutine(PlayAndDeleteSound(srcObj, sound));
    }
}