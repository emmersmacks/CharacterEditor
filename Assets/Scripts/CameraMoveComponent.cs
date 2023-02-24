using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveComponent : MonoBehaviour
{
    public float Speed;
    
    private Camera _camera;
    private Transform _target;

    private Transform _currentPosition;

    void Start()
    {
        _camera = Camera.main;
    }
    
    void Update()
    {
        if (_target != null)
        {
            var targetPosition = _target.position + new Vector3(-0.2f, 0, 0.5f);
            var direction = targetPosition - _camera.transform.position;
            _camera.transform.position += direction.normalized * Speed * Time.deltaTime;
            if (Vector3.Distance(targetPosition, _camera.transform.position) < 0.01)
            {
                _currentPosition = _target;
                _target = null;
            }
        }
    }

    public void SetTarget(Transform target)
    {
        if (_currentPosition != target)
        {
            _currentPosition = target;
            _target = target;
        }
    }
}
