using UnityEngine;
using ShopSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class InventoryItemClothes : MonoBehaviour, IPointerDownHandler
{
    
    public int index;
    public Slots slots;
    public ShopController shopController;
    [SerializeField] private GameObject[] _head;
    [SerializeField] private GameObject[] _glases;
    [SerializeField] private GameObject[] _bags;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _refuseButton;
    [SerializeField] private Image[] _dressingImage;
    

    


    private void Start()
    {
        LoadInfo();
        _confirmButton.onClick.AddListener(() => Confirmed());
        _refuseButton.onClick.AddListener(() => Refused());
       

       
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {

            if (slots.isUsedClothesInventory[index] == true)
            {

                _panel.SetActive(true);

            }
        }
    }




    private void InstantiateObject()
    {
        if (slots._prefabs[index].CompareTag("Head"))
        {
            for(int i = 0; i < _head.Length; i++)
            {
                _head[i].GetComponent<MeshFilter>().sharedMesh = slots._prefabs[index].GetComponent<MeshFilter>().sharedMesh;
                _head[i].GetComponent<MeshRenderer>().sharedMaterial = slots._prefabs[index].GetComponent<MeshRenderer>().sharedMaterial;
                
            }
            SetDressingImage(0);
        }
        else if (slots._prefabs[index].CompareTag("Glases"))
        {
            for (int i = 0; i < _head.Length; i++)
            {
                _glases[i].GetComponent<MeshFilter>().sharedMesh = slots._prefabs[index].GetComponent<MeshFilter>().sharedMesh;
                _glases[i].GetComponent<MeshRenderer>().sharedMaterial = slots._prefabs[index].GetComponent<MeshRenderer>().sharedMaterial;
            }
            SetDressingImage(1);
        }
        else if (slots._prefabs[index].CompareTag("Bag"))
        {
            for (int i = 0; i < _head.Length; i++)
            {
                _bags[i].GetComponent<MeshFilter>().sharedMesh = slots._prefabs[index].GetComponent<MeshFilter>().sharedMesh;
                _bags[i].GetComponent<MeshRenderer>().sharedMaterial = slots._prefabs[index].GetComponent<MeshRenderer>().sharedMaterial;
            }
            SetDressingImage(2);

        }
    }


    public void Confirmed()
    {
        InstantiateObject();
        _panel.SetActive(false);
    }


    public void Refused()
    {
        _panel.SetActive(false);
    }


  

    private void SetDressingImage(int count)
    {

        _dressingImage[count].sprite = slots.slotsClothesImages[index].sprite;
        _dressingImage[count].enabled = true;
        _dressingImage[count].preserveAspect = true;
        slots.IsUsedClothes[index] = true;
       

    }


    public void LoadInfo()
    {
        if(slots.IsUsedClothes[index] == true)
        {
            InstantiateObject();
        }
      
    }
}
