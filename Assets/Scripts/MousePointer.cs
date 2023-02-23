using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    public Transform SelectedItem;
    public LayerMask CurrentTrackedLayer; 
    
    private Camera _camera;
    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _camera = Camera.main;
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    
    void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit, 100, CurrentTrackedLayer))
        {
            _meshRenderer.enabled = true;
            SelectedItem = hit.transform;
            if(hit.transform.TryGetComponent<BodyPart>(out var part))
                part.IsSelected = true;
            transform.position = hit.point;
        }
        else
        {
            _meshRenderer.enabled = false;
        }
    }
}
