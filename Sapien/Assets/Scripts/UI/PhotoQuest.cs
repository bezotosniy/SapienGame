using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;

public class PhotoQuest : MonoBehaviour
{
    public string TargetName;
    public Transform transform;
    public GameObject target;
    public Transform TargetTransform;
    public GameObject VoiceRecognition;
    public bool TargetIsPressed = false;
    public Sprite picture;

    void Start()
    {
        target = GameObject.Find(TargetName);
        transform = GetComponent<Transform>();
        TargetTransform = target.GetComponent<Transform>();
        transform.position = new Vector3(TargetTransform.position.x, TargetTransform.position.y + 0.7f, TargetTransform.position.z);
        target.AddComponent<MeshCollider>();
        /*target.AddComponent(typeof(EventTrigger));
        EventTrigger trigger = target.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { this.gameObject.GetComponent<PhotoQuest>().WordSpellingQuestOpener(); });
        trigger.triggers.Add(entry);*/
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(transform.rotation.x, transform.rotation.y + 180f, transform.rotation.z);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == TargetName)
                {
                    WordSpellingQuestOpener();
                    hit = new RaycastHit();
                }   
            }
        }

        if (TargetIsPressed)
        {
            if (VoiceRecognition.GetComponent<VoiceRegontion2>().RecognitionSuccessful == 1)
            {
                Debug.Log("Camera");
                CameraOpener();
                VoiceRecognition.GetComponent<VoiceRegontion2>().RecognitionSuccessful = 0;
                TargetIsPressed = false;
            }
        }
    }

    public void WordSpellingQuestOpener()
    {
        VoiceRecognition.SetActive(true);
        VoiceRecognition.GetComponent<VoiceRegontion2>().Task[0] = "Flower";
        TargetIsPressed = true;
    }

    public void CameraOpener()
    {
        Debug.Log("CameraOpener");
        VoiceRecognition.SetActive(false);
        GameObject.Find("PhoneButton").GetComponent<PhoneManager>().OnClickCameraOpen();
        GameObject.Find("Cursor").GetComponent<CursorFollow>().targetName = TargetName;
        Camera.main.transform.LookAt(TargetTransform);
        TargetName = "0";
    }
}
