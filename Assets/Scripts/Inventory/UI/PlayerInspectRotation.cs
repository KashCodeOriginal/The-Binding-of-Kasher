using System;
using UnityEngine;

public class PlayerInspectRotation : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void OnMouseDrag()
    {
        float xRotation = Input.GetAxis("Mouse Y") * _speed;
        
        transform.Rotate(Vector3.up, xRotation);
    }
}
