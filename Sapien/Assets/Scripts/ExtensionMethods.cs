using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public static class ExtensionMethods
{
    public static Transform FindDeepChild(this Transform aParent, string aName)
    {
        Queue<Transform> queue = new Queue<Transform>();
        queue.Enqueue(aParent);
        while (queue.Count > 0)
        {
            var c = queue.Dequeue();
            if (c.name == aName)
                return c;
            foreach(Transform t in c)
                queue.Enqueue(t);
        }
        return null;
    }

    public static IEnumerator LoadSceneWithTransition(float fadeDuration , string sceneName, Slider progressBar = null , VideoClip videoClip = null)
    {
        progressBar = FadePanel.SceneFadePanel.progressBar;
        VideoPlayer _videoPlayer = GameObject.FindObjectOfType<VideoPlayer>(true);
        
        FadePanel.SceneFadePanel.ChangePanelAlpha(fadeDuration, 1);
        yield return new WaitForSecondsRealtime(fadeDuration);
        
        _videoPlayer.gameObject.SetActive(true);
        
        if (_videoPlayer != null && videoClip != null)
        {
            _videoPlayer.clip = videoClip;
            _videoPlayer.Play();
            Debug.Log("Video started");

            AsyncOperation loading = SceneManager.LoadSceneAsync(sceneName);
            
            while (!loading.isDone)
            {
                progressBar.value = Mathf.Clamp01(loading.progress / 0.9f);
                yield return null;
            }
        

        }
        //---------------------DEBUG---------------------------------------
        else
        {
            _videoPlayer.Play();

            AsyncOperation _loading = SceneManager.LoadSceneAsync(sceneName);

            while (!_loading.isDone)
            {
                progressBar.value = Mathf.Clamp01(_loading.progress / 0.9f);
                yield return null;
            }

            _videoPlayer.gameObject.SetActive(false);
        }
        //---------------------DEBUG---------------------------------------
    }
}
