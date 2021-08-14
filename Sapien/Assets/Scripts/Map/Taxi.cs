using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taxi : MonoBehaviour
{
    
    private bool isClicked;
    private RaycastHit hit;
    private Ray ray;
    private MapInPhone map;

    [SerializeField]private QuickOutline[] outlines;
    private void Start()
    {
        map = GameManager.Instance._phoneManager.Map;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            isClicked = true;
    }

    private void FixedUpdate()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!GameManager.Instance.IsPhoneOpened && Physics.Raycast(ray, out hit))
        {
            if ((hit.transform.IsChildOf(this.gameObject.transform) || hit.transform == this.transform))
            {
                foreach (var outline in outlines)
                {
                    outline.enabled = true;
                }
                if (isClicked)
                {
                    Debug.Log("TAXI");
                    map.OpenMap("Taxi");
                }
            }
            else
            {
                foreach (var outline in outlines)
                {
                    outline.enabled = false;
                }
            }
        }
        isClicked = false;
    }
}
