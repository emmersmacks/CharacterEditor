using DefaultNamespace;
using UnityEngine;

public class LayerSelector : MonoBehaviour
{
    public MousePointer Pointer;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Pointer.SelectedItem.TryGetComponent<PartsLayer>(out var layer))
            {
                layer.Set();
            }
        }
    }
}
