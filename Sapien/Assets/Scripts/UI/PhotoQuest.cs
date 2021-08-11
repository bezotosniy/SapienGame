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
    public bool SpellSuccessful = false;
    public Sprite picture;
    public GameObject blur;
    public GameObject WordScreen;

    void Start()
    {
        target = GameObject.Find(TargetName);
        transform = GetComponent<Transform>();
        TargetTransform = target.GetComponent<Transform>();
        transform.position = new Vector3(TargetTransform.position.x, TargetTransform.position.y + 0.7f, TargetTransform.position.z);
        target.AddComponent<MeshCollider>();
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
                    StartCoroutine(WordSpellingQuestOpener());
                    hit = new RaycastHit();
                }   
            }
        }

        if (SpellSuccessful)
        {
            Debug.Log("Camera");
            CameraOpener();
            blur.SetActive(true);
            VoiceRecognition.GetComponent<VoiceRegontion2>().RecognitionSuccessful = 0;
            SpellSuccessful = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public int SpellCount = 0;

    public IEnumerator WordSpellingQuestOpener()
    {
        VoiceRecognition.SetActive(true);
        VoiceRecognition.GetComponent<VoiceRegontion2>().Task[0] = "Flower";
        while (true)
        {
            if (VoiceRecognition.GetComponent<VoiceRegontion2>().RecognitionSuccessful == 1)
            {
                Debug.Log("Proceeded");
                VoiceRecognition.GetComponent<VoiceRegontion2>().RecognitionSuccessful = 0;
                VoiceRecognition.SetActive(false);
                SpellCount++;
                if(SpellCount == 1)
                {
                    SpellSuccessful = true;
                }
                StartCoroutine(WordSpellingQuestOpener());
                break;
            }
            else { yield return new WaitForSeconds(1f); }
        }
        yield return null;
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

    public void PictureOpener()
    {
        WordScreen.GetComponent<Animator>().Play("NewPicture");
        WordScreen.GetComponent<Transform>().Find("WordPic").GetComponent<Transform>().Find("Image").GetComponent<Transform>().Find("Pic1").gameObject.SetActive(true);
    }

    public void PictureSubmit()
    {
        GetComponent<AudioSource>().Play();
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
