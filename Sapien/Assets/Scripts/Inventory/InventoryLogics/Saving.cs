using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Saving : MonoBehaviour
{
    public Repository repository;


    public void Load()
    {
        repository = JsonUtility.FromJson<Repository>(File.ReadAllText(Application.streamingAssetsPath + "/SaveController.json"));
    }

    public void Save()
    {
        File.WriteAllText(Application.streamingAssetsPath + "/SaveController.json", JsonUtility.ToJson(repository));
    }


    [System.Serializable]
   public class Repository
    {
        public bool[] isOpened;
    
    }
}
