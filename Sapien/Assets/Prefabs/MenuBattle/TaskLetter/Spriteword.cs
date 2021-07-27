using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spriteword : MonoBehaviour
{

    public Sprite SpriteImage;
    public Sprite SpriteTask;
    public string TextTask; 
    public Image OneTask;
    public Text textTaskOneWord;

   

    public Text Backtext;

    
    public void GenerateTask()
    {
        
        textTaskOneWord.text = "";
        OneTask.sprite = SpriteTask;
        Backtext.enabled = true;
        
    }

    public void GenerateTaskHarder()
    {
        textTaskOneWord.text = "";
        OneTask.sprite = SpriteTask;
        Backtext.enabled = false;
    }
}

