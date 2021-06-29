using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovingPoint : MonoBehaviour
{
    public NavMeshAgent agent;
    MovingPercon MP;
    Camera Cam;
    public GameObject Prefab;
    private Ray RayMouse;
    private GameObject Instance;
    Vector3 go;
    bool second;
    bool ButtonTrigger;
    void Start()
    {
        agent.enabled = false;
        Cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        MP = GetComponent<MovingPercon>();
    }
    public void ButtonON() { ButtonTrigger = true; }
    public void ButtonOFF() { ButtonTrigger = false; }
    void Update()
    {
        MP.SecondMoving(second, go,agent);
        if (Input.GetButtonDown("Fire1"))
        {
            if (Cam != null)
            {
                RaycastHit hit;
                var mousePos = Input.mousePosition;
                RayMouse = Cam.ScreenPointToRay(mousePos);

                if (Physics.Raycast(RayMouse.origin, RayMouse.direction, out hit, 40))
                {
                    if (hit.collider.tag == "Ground"&&!ButtonTrigger)
                    {
                        
                        Instance = Instantiate(Prefab);
                        Instance.transform.position = hit.point + hit.normal * 0.01f;
                        second = true;
                        MP.second = false;
                        go = Instance.transform.position;
                        agent.enabled = true;
                        agent.SetDestination(go);
                       
                        Destroy(Instance, 1.5f);

                    }
                }
            }
            else
            {
                Debug.Log("No camera");
            }
        }
    }
}
