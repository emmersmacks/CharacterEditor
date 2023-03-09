using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(MeshEditorSettings))]
    public class MeshEditor : UnityEditor.Editor
    {
        void OnSceneGUI()
        {
            var settingsPoint = FindObjectsOfType<MeshEditorSettings>();

            foreach (var settings in settingsPoint)
            {
                var meshFilter = settings.GetComponent<MeshFilter>();
                var mesh = meshFilter.sharedMesh;
        
                if (mesh != null)
                {
                    var vertices = new Point[mesh.vertexCount];
        
                    for (var i = 0; i < mesh.vertexCount; i++)
                    {
                        vertices[i] = new Point();
                        vertices[i].Position = mesh.vertices[i];
                        vertices[i].Type = ESelectTypes.NotSelected;
                    }
                    //Debug.Log(Input.anyKeyDown);
                    if (Event.current.type == EventType.KeyDown)
                    {
                        
                        Ray ray = Camera.main.ScreenPointToRay(Event.current.mousePosition);

                        var isHitted = Physics.Raycast(ray, out var hit);
                        
                        //Debug.Log(isHitted);
                        if (isHitted)
                        {
                            for (int i = 0; i < mesh.vertexCount; i++)
                            {
                                var vertexDistance = Vector3.Distance(mesh.vertices[i], hit.point);
                                //Debug.Log(vertexDistance);
                                if (vertexDistance < settings.BrushSize)
                                {
                                    vertices[i].Type = ESelectTypes.Selected;
                                }
                                else
                                {
                                    vertices[i].Type = ESelectTypes.NotSelected;

                                }
                            }
        
                            
                        }
                    }

                    for (int i = 0; i < vertices.Length; i++)
                    {
                        var vertex = vertices[i];
                        if (vertex.Type == ESelectTypes.NotSelected)
                        {
                            Handles.color = Color.yellow;
                            //Gizmos.DrawSphere(vertex.Position + settings.transform.position, settings.Radius);
                            Handles.DrawSolidDisc(vertex.Position + settings.transform.position,
                                Camera.current.transform.forward, 0.001f);
                            continue;
                        }
                        Handles.color = Color.red;
                        //Gizmos.DrawSphere(vertex.Position + settings.transform.position, settings.Radius);
                        Handles.DrawSolidDisc(vertex.Position + settings.transform.position,
                            Camera.current.transform.forward, 0.001f);
                        

                    }
                }
            }
            
            
        }
    }
}