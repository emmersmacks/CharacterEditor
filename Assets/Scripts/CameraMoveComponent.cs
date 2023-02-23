using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveComponent : MonoBehaviour
{
    public float Speed;
    
    private Camera _camera;
    private Transform _target;

    void Start()
    {
        _camera = Camera.main;
    }
    
    void Update()
    {
        if (_target != null)
        {
            var targetPosition = _target.position + Vector3.forward + new Vector3(-0.5f, 0, 0);
            var direction = targetPosition - _camera.transform.position;
            _camera.transform.position += direction.normalized * Speed * Time.deltaTime;
            if (Vector3.Distance(targetPosition, _camera.transform.position) < 0.1)
            {
                _target = null;
            }
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
