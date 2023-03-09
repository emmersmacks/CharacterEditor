using UnityEngine;

namespace DefaultNamespace
{
    public class BodyPart : MonoBehaviour
    {
        public Material SelectedMaterial;
        public Material DefaultMaterial;

        internal bool IsSelected;

        private MeshRenderer _meshRenderer;
        
        private void Start()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Update()
        {
            if (IsSelected)
                _meshRenderer.material = SelectedMaterial;
            else
            {
                _meshRenderer.material = DefaultMaterial;
            }

            IsSelected = false;
        }
    }
}