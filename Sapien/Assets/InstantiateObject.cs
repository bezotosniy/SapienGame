using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateObject : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject _someWords;
    void Start()
    {
        GameObject prefab;
        prefab = Instantiate(_prefab, new Vector2(20, 80), Quaternion.identity) as GameObject;
        prefab.transform.SetParent(_someWords.transform, false);
    }

  
}
