using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoOnNewScene : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform parent;
    public void Start()
    {
        //parent.position = new Vector3(PlayerPrefs.GetFloat("LastX"), PlayerPrefs.GetFloat("LastY"), PlayerPrefs.GetFloat("LastZ"));
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "portal")
        {
            StartCoroutine(LoadYourAsyncScene());
            PlayerPrefs.SetInt("LastScene", SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.SetFloat("LastX", parent.position.x);
            PlayerPrefs.SetFloat("LastY", parent.position.y);
            PlayerPrefs.SetFloat("LastZ", parent.position.z);
        }
    }
    
    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Battle 1");
        
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
