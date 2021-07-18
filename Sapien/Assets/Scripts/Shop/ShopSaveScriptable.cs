using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShopSystem
{

    [CreateAssetMenu(fileName = "ShopData", menuName = "Resourses/ShopData")]
    public class ShopSaveScriptable : ScriptableObject
    {
        public int selectedIndex;
        public ShopItem[] itemsInfo;

      

      
    }

    [System.Serializable]
    public class ShopItem
    {
        public Image image;
        public GameObject prefab;
        public int price;
        public string name;
        public bool isUnlocked;
       
    }


}
