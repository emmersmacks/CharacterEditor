using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class PartsLayer : MonoBehaviour
    {
        [SerializeField] 
        private Transform _cameraTargetPoint;

        [SerializeField] 
        private CameraMoveComponent _cameraMoveComponent;
    
        [FormerlySerializedAs("onSet")]
        [SerializeField]
        private Button.ButtonClickedEvent m_OnSet = new Button.ButtonClickedEvent();
        
        [FormerlySerializedAs("onSet")]
        [SerializeField]
        private Button.ButtonClickedEvent m_OnRemove = new Button.ButtonClickedEvent();

        public void Set()
        {
            if(_cameraTargetPoint != null)
                _cameraMoveComponent.SetTarget(_cameraTargetPoint);
            m_OnSet?.Invoke();
        }

        public void Remove()
        {
            m_OnRemove?.Invoke();
        }
    }
}