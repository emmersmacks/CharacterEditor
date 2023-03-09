using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class PartsLayer : MonoBehaviour
    {
        [SerializeField] private CinemachineFreeLook _camera;

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
            _cameraMoveComponent.SetTarget(_camera);
            m_OnSet?.Invoke();
        }

        public void Remove()
        {
            m_OnRemove?.Invoke();
        }
    }
}