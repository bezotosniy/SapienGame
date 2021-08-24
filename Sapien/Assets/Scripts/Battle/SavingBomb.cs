using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SavingBomb : MonoBehaviour
{ 
   public Repository _saving;
    public void Load()
    {
        _saving = JsonUtility.FromJson<Repository>(File.ReadAllText(Application.streamingAssetsPath + "/SaveBombsFills.json"));
    }

    public void Save()
    {
        File.WriteAllText(Application.streamingAssetsPath + "/SaveBombsFills.json", JsonUtility.ToJson(_saving));
    }






   [System.Serializable]
   public class Repository
   {
     public bool[] IsBombsFillOpened;
   }
}
