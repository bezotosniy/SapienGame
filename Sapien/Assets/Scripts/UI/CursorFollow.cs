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
                    picture.SetActive(true);
                    picture.GetComponent<RectTransform>().position = this.gameObject.GetComponent<RectTransform>().position;
                    GameObject.Find("PhoneButton").GetComponent<Animator>().Play("Picture");
                    camera.GetComponent<CameraManager>().enabled = false;
                    this.enabled = false;
                    Pic1.GetComponent<Image>().sprite = picture.GetComponent<Image>().sprite;
                    GameObject.Find("abc").SetActive(false);
                    hit = new RaycastHit();
                }
            }
        }
    }
}
