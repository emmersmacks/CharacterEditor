using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraMoveComponent : MonoBehaviour
{
    public float Speed;

    public CinemachineVirtualCamera FixedCamera;
    public CinemachineVirtualCamera FreeLookCameraCamera;
    
    private Camera _camera;
    private CinemachineBrain _cameraBrain;
    private Transform _target;

    private Transform _currentPosition;
    private ECameraMode _cameraMode;

    void Start()
    {
        _camera = Camera.main;
        _cameraBrain = _camera.GetComponent<CinemachineBrain>();
    }
    
    void Update()
    {
        if (_cameraMode == ECameraMode.Fixed)
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
        else
        {
            
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

    public void SetCameraMode(ECameraMode mode)
    {
        _cameraMode = mode;

        if (mode == ECameraMode.Fixed)
        {
            FixedCamera.Priority = 2;
            FreeLookCameraCamera.Priority = 1;
        }
        else
        {
            FixedCamera.Priority = 1;
            FreeLookCameraCamera.Priority = 2;
        }
    }
}

public enum ECameraMode
{
    Fixed,
    Observable
}
