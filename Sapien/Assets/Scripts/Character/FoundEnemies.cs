using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundEnemies : MonoBehaviour
{
    public GameObject particle;
    public bool enabledd = true;
    private void OnTriggerEnter(Collider collision)
    {
       if(collision.gameObject.tag == "Enemy"&&enabledd)
        {
            GameObject obj = Instantiate(particle, collision.gameObject.transform);
            StartCoroutine(destroyPart(obj));
            collision.gameObject.GetComponent<HideEnemyWillFind>().enemy.SetActive(true);
            collision.gameObject.GetComponent<HideEnemyWillFind>().cloud.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy"&&enabledd)
        {
            GameObject obj = Instantiate(particle, collision.gameObject.transform);
            StartCoroutine(destroyPart(obj));
            collision.gameObject.GetComponent<HideEnemyWillFind>().enemy.SetActive(false);
            collision.gameObject.GetComponent<HideEnemyWillFind>().cloud.SetActive(true);
        }
    }
    IEnumerator destroyPart(GameObject obj)
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(obj);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
