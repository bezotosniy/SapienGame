using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEditor.SearchService;
using UnityEngine.UI;

public class NavigationManager : MonoBehaviour
{
    [SerializeField] private Camera camera;

    private Vector3 targetPosition;
    private RectTransform pointer;
    public Transform target;
    public float border = 100f;
    //public string targetName;
    private Vector2 borderInUI;
    private Vector3 maxScale = new Vector3(1.5f, 1.5f, 1.0f);
    private void Awake()
    {
        //target = GameObject.Find(targetName).GetComponent<Transform>();
        pointer = GameObject.Find("NavigatorPointer").GetComponent<RectTransform>();
    }

    void Update()
    {
        targetPosition = new Vector3(target.position.x, target.position.y, target.position.z);
        Vector3 TargetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        bool isOffScreen = TargetPositionScreenPoint.x <= 0 || TargetPositionScreenPoint.x >= Screen.width || TargetPositionScreenPoint.y <= 0 || TargetPositionScreenPoint.y >= Screen.height || targetPosition.z <= Camera.main.transform.position.z;

        //Debug.Log(isOffScreen + " " + TargetPositionScreenPoint);

        if (isOffScreen)    
        {
            Vector3 cappedTargetScreenPosition = TargetPositionScreenPoint;
            if (cappedTargetScreenPosition.x <= border)
            {
                cappedTargetScreenPosition.x = border;
                Debug.Log("Right");
            }
            else if (cappedTargetScreenPosition.x >= Screen.width - border)
            {
                cappedTargetScreenPosition.x = Screen.width - border;
                Debug.Log("Left");
            }
            if (cappedTargetScreenPosition.y <= border)
            {
                cappedTargetScreenPosition.y = border;
                Debug.Log("Down");
            }
            else if (cappedTargetScreenPosition.y >= Screen.height - border)
            {
                cappedTargetScreenPosition.y = Screen.height - border;
                Debug.Log("Top");
            }

            cappedTargetScreenPosition.y /= Screen.height;
            cappedTargetScreenPosition.x /= Screen.width;
            
            Vector3 direction = (target.position - camera.transform.position).normalized;
            Vector3 project = Vector3.ProjectOnPlane(direction, camera.transform.forward).normalized;
            
            //Debug.Log((target.position - camera.transform.position).magnitude);
            
            
            //direction = camera.transform.TransformVector(direction);
            //direction.y = 0;
            direction = project;
            direction /= 2;
            
            //Debug.Log(direction);

            GameObject WorldObject = target.gameObject;

            RectTransform UI_Element = pointer;

            RectTransform CanvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
            
            Vector3 newScale = Vector3.Lerp(maxScale, Vector3.one, 
                ((target.position - camera.transform.position).magnitude / 20.0f));

            //if (direction.z < 0)
            //    direction.y = -0.5f;
            //float secondDim = Mathf.Clamp(direction.z + direction.y , -0.5f , 0.5f);
            Vector2 WorldObject_ScreenPosition = new Vector2(
                ((direction.x * CanvasRect.sizeDelta.x)),
                ((direction.y * CanvasRect.sizeDelta.y)));
            
            UI_Element.anchoredPosition = WorldObject_ScreenPosition;
            UI_Element.localScale = newScale;
        }
        else if (!isOffScreen)
        {
            GameObject WorldObject = target.gameObject;
            //
            RectTransform UI_Element = pointer;
            
            RectTransform CanvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
            
            
            Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(WorldObject.transform.position);
            Vector2 WorldObject_ScreenPosition = new Vector2(
            ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
            ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));
            
            Vector3 newScale = Vector3.Lerp(maxScale, Vector3.one, 
                ((target.position - camera.transform.position).magnitude / 20.0f));
            
            UI_Element.anchoredPosition = WorldObject_ScreenPosition;
            UI_Element.localScale = newScale;
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    RaycastHit hit;
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        if (hit.transform.name == targetName)
        //        { print(targetName + " is clicked by mouse"); }
        //        TargetChange(hit.transform.name);
        //    }
        //}
    }

    public void TargetChange(string name)
    {
        //targetName = name;
        target = GameObject.Find(name).GetComponent<Transform>();
        Debug.Log("Click");
    }

    private void OnDrawGizmos()
    {
        Vector3 direction = (target.position - camera.transform.position).normalized;
        Vector3 project = Vector3.ProjectOnPlane(direction, camera.transform.forward).normalized;
        //Debug.Log(project);
        Gizmos.DrawRay(camera.transform.position , project);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(camera.transform.position , direction * 100);
        direction = camera.transform.TransformVector(direction);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(camera.transform.position , direction * 100);
    }
}
