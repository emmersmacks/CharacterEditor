using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class LayerManager : MonoBehaviour
    {
        public PartsLayer[] Layers;

        private PartsLayer _currentLayer;
        public CameraMoveComponent CameraMoveComponent;

        public bool SetLayer(PartsLayer layer)
        {
            foreach (var partsLayer in Layers)
            {
                if (partsLayer == layer)
                {
                    if(layer.IsFixed)
                        CameraMoveComponent.SetCameraMode(ECameraMode.Fixed);
                    else
                    {
                        CameraMoveComponent.SetCameraMode(ECameraMode.Observable);
                    }
                    _currentLayer = layer;
                    _currentLayer.Set();
                    return true;
                }
            }
            return false;
        }
    }
}