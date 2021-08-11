using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorFollow : MonoBehaviour
{
    public GameObject picture;
    public GameObject camera;
    public GameObject Pic1;
    public GameObject Pic2;
    public string targetName;

    void Update()
    {
        GetComponent<RectTransform>().position = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == targetName)
                {
                    hit = new RaycastHit();
                    StartCoroutine(PictureIsTaken());
                    camera.GetComponent<CameraManager>().enabled = false;
                    this.enabled = false;
                    
                }
            }
        }
    }

    public IEnumerator PictureIsTaken()
    {
        picture.SetActive(true);
        GameObject.Find("PhoneButton").GetComponent<Animator>().Play("Picture");
        Pic1.GetComponent<Image>().sprite = picture.GetComponent<Image>().sprite;
        GameObject.Find("abc").GetComponent<PhotoQuest>().PictureSubmit();
        yield return new WaitForSeconds(3f);
        picture.SetActive(false);
        GameObject.Find("PhoneButton").GetComponent<PhoneManager>().OnClickCameraClose();
        yield return new WaitForSeconds(1f);
        GameObject.Find("PhoneButton").GetComponent<PhoneManager>().OnPointerClickWordIcon("WordScreen");
        yield return new WaitForSeconds(0.5f);
        GameObject.Find("abc").GetComponent<PhotoQuest>().PictureOpener();
    }
}
