using Cinemachine;
using UnityEngine;

public class CameraMoveComponent : MonoBehaviour
{
    public CinemachineFreeLook CurrentCamera;
    private Transform _currentPosition;

    void Start()
    {
        Debug.Log(CurrentCamera.m_XAxis.m_MaxSpeed);
    }
    
    void Update()
    {
        CurrentCamera.m_XAxis.m_MaxSpeed = 0;
        CurrentCamera.m_YAxis.m_MaxSpeed = 0;

        if (Input.GetMouseButton(1))
        {
            CurrentCamera.m_XAxis.m_MaxSpeed = 300;
            CurrentCamera.m_YAxis.m_MaxSpeed = 5;
        }
    }

    public void SetTarget(CinemachineFreeLook cinemachineFreeLook)
    {
        CurrentCamera.Priority = 0;
        CurrentCamera.gameObject.SetActive(false);
        cinemachineFreeLook.Priority = 1;
        cinemachineFreeLook.gameObject.SetActive(true);
        CurrentCamera = cinemachineFreeLook;
    }
}