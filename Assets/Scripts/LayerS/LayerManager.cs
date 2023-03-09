using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class LayerManager : MonoBehaviour
    {
        public List<LayerEvent> LayersData;

        private PartsLayer _currentLayer;
        public CameraMoveComponent CameraMoveComponent;

        private void Start()
        {
            foreach(var layerData in LayersData)
            {
                foreach (var button in layerData.Buttons)
                {
                    button.onClick.AddListener(delegate { SetLayer(layerData.Layer); });

                }
            }
        }

        public bool SetLayer(PartsLayer layer)
        {
            foreach (var partsLayer in LayersData)
            {
                if (partsLayer.Layer == layer)
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