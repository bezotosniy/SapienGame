using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class NavigationManager : MonoBehaviour
{
    [SerializeField] private Camera camera;

    private Vector3 targetPosition;
    private RectTransform pointer;
    private Transform pointerObj;
    public Transform target;
    public float border = 100f;

    private void Awake()
    {
        target = GameObject.Find("SM_Prop_ParkingMeter_02").GetComponent<Transform>();
        targetPosition = new Vector3(target.position.x, target.position.y, target.position.z);
        pointer = GameObject.Find("NavigatorPointer").GetComponent<RectTransform>();
        pointerObj = GameObject.Find("PointerObj").GetComponent<Transform>();
    }

    void Update()
    {
        /*Vector3 toPosition = targetPosition;
        Vector3 fromPosition = GameObject.Find("Player").GetComponent<Transform>().position;
        Vector3 Direction = (toPosition - fromPosition).normalized;
        float angle = UtilsClass.GetAngleFromVectorFloat(Direction);
        pointer.localEulerAngles = new Vector3(0, 0, angle);*/

        Vector3 TargetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        bool isOffScreen = TargetPositionScreenPoint.x <= 0 || TargetPositionScreenPoint.x >= Screen.width || TargetPositionScreenPoint.y <= 0 || TargetPositionScreenPoint.y >= Screen.height || targetPosition.z <= Camera.main.transform.position.z;
        Debug.Log(isOffScreen + " " + TargetPositionScreenPoint);

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

            Vector3 pointerWorldPosition = camera.ScreenToWorldPoint(cappedTargetScreenPosition);
            pointer.position = pointerWorldPosition;
            pointer.position = cappedTargetScreenPosition;
            pointer.localPosition = new Vector3(-pointer.localPosition.x, -pointer.localPosition.y, 0f);

        }
        else if (!isOffScreen)
        {
            GameObject WorldObject = target.gameObject;

            RectTransform UI_Element = pointer;

            RectTransform CanvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();


            Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(WorldObject.transform.position);
            Vector2 WorldObject_ScreenPosition = new Vector2(
            ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
            ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

            UI_Element.anchoredPosition = WorldObject_ScreenPosition;
        }

        /*pointerObj.position = targetPosition;
        pointer.position = Camera.main.WorldToScreenPoint(pointerObj.position);

        bool isOffScreen = pointer.position.x <= 0 || pointer.position.x >= Screen.width || pointer.position.y <= 0 || pointer.position.y >= Screen.height;
        Debug.Log(isOffScreen + " " + pointer);

        if (isOffScreen)
        {
            Vector3 cappedTargetScreenPosition = pointer.position;
            if (cappedTargetScreenPosition.x < border)
            {
                cappedTargetScreenPosition.x = border;
                Debug.Log("Right");
            }
            else if (cappedTargetScreenPosition.x > Screen.width - border)
            {
                cappedTargetScreenPosition.x = Screen.width - border;
                Debug.Log("Left");
            }
            if (cappedTargetScreenPosition.y < border)
            {
                cappedTargetScreenPosition.y = border;
                Debug.Log("Down");
            }
            else if (cappedTargetScreenPosition.y > Screen.height - border)
            {
                cappedTargetScreenPosition.y = Screen.height - border;
                Debug.Log("Top");
            }
            pointer.position = cappedTargetScreenPosition;
        }*/
    }
}
