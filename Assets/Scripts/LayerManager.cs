using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class LayerManager : MonoBehaviour
    {
        public PartsLayer[] Layers;

        private PartsLayer _currentLayer;

        public bool SetLayer(PartsLayer layer)
        {
            foreach (var partsLayer in Layers)
            {
                if (partsLayer == layer)
                {
                    _currentLayer = layer;
                    _currentLayer.Set();
                    return true;
                }
            }
            return false;
        }
    }
}