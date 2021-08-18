using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestQuest2 : StoryQuest
{
    private void Update()
    {
        if (activated)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                QuestComplete();
            }
        }
    }

    public void LoadScene(int idx)
    {
        SceneManager.LoadScene(idx);
    }

}
