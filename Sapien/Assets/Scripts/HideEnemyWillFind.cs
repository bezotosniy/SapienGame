using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HideEnemyWillFind : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cloud, enemy;
    NavMeshAgent agent;
    Vector3 point;
    public Animator anim;
    bool stop;
    void Start()
    {
        
        point = GameObject.FindGameObjectWithTag("portal").transform.position;
        agent = GetComponent<NavMeshAgent>();
        
       
    }
    public void SetDestin()
    {
        agent.SetDestination(point);
        StartCoroutine(LikeUpdate());
    }
    IEnumerator LikeUpdate()
    {
        while (true)
        {
            Debug.Log("1");
            yield return new WaitForFixedUpdate();
            if (Vector3.Distance(transform.position, point) < 3f)
            {
                Debug.Log("2");
                agent.enabled = false;
                StartCoroutine(lerpp());
                yield break;
            }
        }
    }
    IEnumerator lerpp()
    {
        while (true)
        {
            Debug.Log("3");
            transform.position = Vector3.Lerp(transform.position, point, 1* Time.deltaTime);
            if (Vector3.Distance(transform.position, point) < .1f)
            {
                Debug.Log("4");
                StartCoroutine(FalseActive());
                stop = true;
                yield break;
            }
        }
    }
    // Update is called once per frame
   
    IEnumerator FalseActive()
    {
        
            Debug.Log("wuojj");
            anim.SetTrigger("Jump");
            yield return new WaitForSeconds(.5f);
           this.gameObject.SetActive(false);
        
    }
}
