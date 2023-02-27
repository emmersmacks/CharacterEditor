using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class LayerSelector : MonoBehaviour
{
    public MousePointer Pointer;
    public LayerManager LayerManager;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Pointer.SelectedItem.TryGetComponent<PartsLayer>(out var layer))
            {
                LayerManager.SetLayer(layer);
            }
        }
    }
}
