using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class PartsLayer : MonoBehaviour
    {
        [FormerlySerializedAs("onSet")]
        [SerializeField]
        private Button.ButtonClickedEvent m_OnSet = new Button.ButtonClickedEvent();
        
        [FormerlySerializedAs("onSet")]
        [SerializeField]
        private Button.ButtonClickedEvent m_OnRemove = new Button.ButtonClickedEvent();

        public void Set()
        {
            m_OnSet?.Invoke(); 
        }

        public void Remove()
        {
            m_OnRemove?.Invoke();
        }
    }
}