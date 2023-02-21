using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinEditor : MonoBehaviour
{
    public GameObject GObject;
    
    public Mesh _cashedMesh;
    public float Force;

    
    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 100))
            {
                GObject.transform.position = hit.point;
                
                var mesh = hit.transform.GetComponent<MeshFilter>().mesh;
                var collider = hit.transform.GetComponent<MeshCollider>();
                
                var vertexArr = mesh.vertices;
                for (int i = 0; i < mesh.vertexCount; i++)
                {
                    var vertexPosition = hit.transform.TransformPoint(mesh.vertices[i]);
                    if (Vector3.Distance(vertexPosition, hit.point) < 0.1f)
                    {
                        vertexPosition += Vector3.forward * Time.deltaTime * Force;
                        mesh.vertices[i] = vertexPosition;
                    }
                }

                mesh.vertices = vertexArr;
                collider.sharedMesh = mesh;
            }
        }
        
    }
}
