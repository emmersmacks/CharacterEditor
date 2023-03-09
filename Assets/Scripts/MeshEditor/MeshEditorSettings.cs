using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[ExecuteInEditMode]
public class MeshEditorSettings : MonoBehaviour
{
    public float Radius;
    public float BrushSize;

    void OnDrawGizmosSelected()
    {
        
        
    }
}

public struct Point
{
    public ESelectTypes Type;
    public Vector3 Position;
}

public enum ESelectTypes
{
    NotSelected,
    Selected
}
