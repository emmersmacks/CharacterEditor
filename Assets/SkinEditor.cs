using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinEditor : MonoBehaviour
{
    public GameObject GObject;
    
    public MeshFilter _meshFilter;
    
    public float Force;
    public float VertexTransitionMax = 0.005f;
    public float BrushSize = 0.005f;

    private Mesh _mesh;
    private MeshCollider _collider;
    private Camera _camera;
    
    private Vector3[] _meshBase;
    private Vector3[] _meshEdited;
    private List<Vector3> _normals;

    private Vector3 _dragStartPoint;
    private RaycastHit _currentHit;
    private List<int> _updatedVertices;

    private void Start()
    {
        _mesh = _meshFilter.mesh;
        _meshBase = _mesh.vertices;
        _collider = _meshFilter.GetComponent<MeshCollider>();
        _camera = Camera.main;
        _normals = new List<Vector3>(_mesh.vertexCount);
        _mesh.GetNormals(_normals);
        _meshEdited = _mesh.vertices;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, 100))
            {
                _dragStartPoint = Input.mousePosition;
                _currentHit = hit;
                _updatedVertices = new List<int>();
                for (int i = 0; i < _mesh.vertexCount; i++)
                {
                    var vertexPosition = _currentHit.transform.TransformPoint(_mesh.vertices[i]);
                    var vertexDistance = Vector3.Distance(vertexPosition, _currentHit.point);
                    if (vertexDistance < BrushSize)
                    {
                        _updatedVertices.Add(i);
                    }
                }
                
            }
        }
        
        if (Input.GetMouseButton(0))
        {
            if (_currentHit.point != Vector3.zero)
            {
                GObject.transform.position = _currentHit.point;
                Debug.Log(_camera.WorldToScreenPoint(_currentHit.normal + _currentHit.point));
                Debug.DrawRay(_camera.WorldToScreenPoint(_currentHit.point), _camera.WorldToScreenPoint(_currentHit.normal + _currentHit.point) - _camera.WorldToScreenPoint(_currentHit.point), Color.red);
                var vertexArr = _mesh.vertices;
                for (int i = 0; i < _updatedVertices.Count; i++)
                {
                    var vertexIndex = _updatedVertices[i];

                    var vertexPosition = _currentHit.transform.TransformPoint(_meshEdited[vertexIndex]);
                        var vertexDistance = Vector3.Distance(vertexPosition, _currentHit.point);
                    
                        var mouseDragDirection = Input.mousePosition - _dragStartPoint;
                        var direction = 0f;
                        Debug.DrawRay(_dragStartPoint, mouseDragDirection, Color.magenta);
                        //Debug.DrawRay(Vector3.zero, _camera.WorldToScreenPoint(_currentHit.normal), Color.red);
                        var vectorsAngle = Vector3.Angle(mouseDragDirection, _camera.WorldToScreenPoint(_currentHit.normal + _currentHit.point) - _camera.WorldToScreenPoint(_currentHit.point));
                        Debug.Log(vectorsAngle);
                        if (vectorsAngle < 90)
                        {
                            direction = 1f * Vector3.Distance(Input.mousePosition, _dragStartPoint);
                        }
                        else
                        {
                            direction = -1 * Vector3.Distance(Input.mousePosition, _dragStartPoint);
                        }
                    var futurePosition = _meshEdited[vertexIndex] + (direction * Force * (BrushSize - vertexDistance) * _normals[vertexIndex]);
                    var transitionDistance = Vector3.Distance(futurePosition, _meshBase[vertexIndex]);

                    if (transitionDistance < VertexTransitionMax)
                    {
            
                        vertexPosition = futurePosition;
                        vertexArr[vertexIndex] = vertexPosition;
                    }
                    //else
                    //{
                    //    vertexPosition = _meshBase[vertexIndex] + ((_meshEdited[vertexIndex] - _meshBase[vertexIndex]).normalized * VertexTransitionMax);
                    //    vertexArr[vertexIndex] = vertexPosition;
                    //}
                }

                _mesh.vertices = vertexArr;
                _collider.sharedMesh = _mesh;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _currentHit.point = Vector3.zero;
            _dragStartPoint = Vector3.zero;
            _meshEdited = _mesh.vertices;
        }
    }
    
    public const float Epsilon = 0.00001f;

    public static bool AreCodirected(Vector2 a, Vector2 b)
    {
        return Vector2.Dot(a, b) > 1 - Epsilon;
    }
}
