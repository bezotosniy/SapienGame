using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [HideInInspector]public PhoneManager _phoneManager;
    public VolumeProfile WizardModeProfile;
    public int WizardModeRendererIndex = 1;
    private int lastRendererIndex = 0;
    public VolumeProfile defaultProfile;
    private Camera mainCamera;
    public event Action OnWizardModeEnabling;
    public event Action OnWizardModeEnabled;
    public event Action OnWizardModeDisable;
    public event Action OnPostProcessingChanged;

    private Coroutine CR_ChangingProfile;
    public bool IsPhoneOpened
    {
        get
        {
            int cnt = 0;
            foreach(Transform child in _phoneManager.transform){
                if(child.gameObject.activeSelf)
                    cnt++;
            }

            return cnt > 1;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
            _phoneManager = GameObject.FindObjectOfType<PhoneManager>();
            SceneManager.sceneLoaded += OnSceneLoaded;
            OnSceneLoaded(SceneManager.GetActiveScene() , LoadSceneMode.Single);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _phoneManager = GameObject.FindObjectOfType<PhoneManager>();
        mainCamera = Camera.main;

    }

    public void EnableWizardMode()
    {
        OnWizardModeEnabling?.Invoke();
        Volume mainVolume = mainCamera.gameObject.GetComponent<Volume>();
        if (mainVolume != null)
        {
            if (CR_ChangingProfile == null)
                CR_ChangingProfile = StartCoroutine(SmoothChangeProfile(mainVolume, WizardModeProfile, WizardModeRendererIndex));
        }
    }

    public void DisableWizardMode()
    {
        OnWizardModeDisable?.Invoke();
        Volume mainVolume = mainCamera.gameObject.GetComponent<Volume>();
        if (mainVolume != null)
        {
            if (CR_ChangingProfile == null)
                CR_ChangingProfile = StartCoroutine(SmoothChangeProfile(mainVolume, defaultProfile, 0));
        }
    }

    void ChangeVolume(ref Volume volume , VolumeProfile changeTo)
    {
        volume.profile = changeTo;
        OnPostProcessingChanged?.Invoke();
    }
    
    IEnumerator SmoothChangeProfile(Volume volume, VolumeProfile changeTo , int RendererIndex)
    {
        Debug.Log($"Changing {changeTo.name} {RendererIndex}");
        float elapsedTime = 0 , duration = 1.5f, speed = 1 / duration;
        if (changeTo != volume.profile)
        {
            while (elapsedTime < duration)
            {
                volume.weight = Mathf.Clamp(volume.weight - speed * Time.deltaTime, 0.001f, 1);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            ChangeVolume(ref volume, changeTo);

            mainCamera.GetComponent<UniversalAdditionalCameraData>().SetRenderer(RendererIndex);
        }

        elapsedTime = 0;

        while (elapsedTime < duration)
        {
            volume.weight = Mathf.Clamp(volume.weight + speed* Time.deltaTime , 0.001f , 1);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if (changeTo == WizardModeProfile)
            OnWizardModeEnabled?.Invoke();
        volume.weight = 1;
        CR_ChangingProfile = null;
    }
}
