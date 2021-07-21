using UnityEngine;

public class ModelMovement : MonoBehaviour
{
    [Range(0, 15)]
    [SerializeField] private float _speed;

   
    void Update()
    {
        transform.Rotate(new Vector3(0, -180, 0), _speed);
    }
}
