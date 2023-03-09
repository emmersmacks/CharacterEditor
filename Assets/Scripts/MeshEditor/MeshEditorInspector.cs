using NaughtyAttributes;
using UnityEngine;

public class MeshEditorInspector : MonoBehaviour
{
    [Button("Open editor")]
    public void OpenEditorWindow()
    {
        var meshEditorWindow = new MeshEditorWindow();
        meshEditorWindow.Show();
    }
}
