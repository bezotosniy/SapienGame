using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slots : MonoBehaviour
{
    [Header("Furniture")]
    public bool[] isUsedFurnitureInventory;
    public GameObject[] slotsFurnitureInventory;
    public Image[] slotsFurnitureImages;
    public GameObject[] furniture;
    public GameObject[] type;


    [Header("Clothes")]
    [Space(20f)]
    public bool[] isUsedClothesInventory;
    public GameObject[] _slotsClothesInventory;
    public GameObject[] _prefabs;
    public Image[] slotsClothesImages;
    public bool[] IsUsedClothes;
    [Header("Plot")]
    [Space(20f)]
    public bool[] isUsedPlotInventory;
    public GameObject[] _slotsPlotInventory;
    [Header("Other")]
    [Space(20f)]
    public bool[] isUsedOtherInventory;
    public GameObject[] _slotsOtherInventory;

  
}
