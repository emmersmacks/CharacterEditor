using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMouseComponent : MonoBehaviour
{
    public float RotationSpeed;
    
    private Vector3 _startMousePosition;
    private Vector3 _startRotateAngle;
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _startMousePosition = Input.mousePosition;
            _startRotateAngle = transform.rotation.eulerAngles;
        }
        
        if (Input.GetMouseButton(1))
        {
            var direction = Input.mousePosition - _startMousePosition;
            transform.rotation = Quaternion.Euler(_startRotateAngle + new Vector3(0, direction.x * -1 * RotationSpeed, 0));
        }
    }
}
