using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinEditor : MonoBehaviour
{
    public GameObject PointerObject;
    public MeshFilter MeshFilter;
    
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
        _mesh = MeshFilter.mesh;
        _meshBase = _mesh.vertices;
        _collider = MeshFilter.GetComponent<MeshCollider>();
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
                //PointerObject.transform.position = _currentHit.point;

                var vertexArr = _mesh.vertices;
                for (int i = 0; i < _updatedVertices.Count; i++)
                {
                    var mouseDragDirection = Input.mousePosition - _dragStartPoint;
                    var directionIndex = GetDirectionIndex(mouseDragDirection);

                    var vertexIndex = _updatedVertices[i];
                    var startEditVertexPosition = _meshEdited[vertexIndex];
                    var vertexWorldPosition = _currentHit.transform.TransformPoint(startEditVertexPosition);
                    var approximationDistance = Vector3.Distance(vertexWorldPosition, _currentHit.point);

                    var futurePosition = GetNextVertexPosition(directionIndex, approximationDistance, vertexIndex);
                    var baseVertexPosition = _meshBase[vertexIndex];
                    var transitionDistance = Vector3.Distance(futurePosition, baseVertexPosition);

                    if (transitionDistance < VertexTransitionMax)
                    {
                        vertexWorldPosition = futurePosition;
                        vertexArr[vertexIndex] = vertexWorldPosition;
                    }
                    else
                    {
                        var maxFuturePosition = GetMaxNextPosition(vertexIndex, directionIndex);
                        vertexWorldPosition = baseVertexPosition + maxFuturePosition;
                        vertexArr[vertexIndex] = vertexWorldPosition;
                    }
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

    private Vector3 GetMaxNextPosition(int vertexIndex, float directionIndex)
    {
        var maxFuturePosition = _normals[vertexIndex].normalized * VertexTransitionMax * directionIndex;
        return maxFuturePosition;
    }

    private float GetDirectionIndex(Vector3 mouseDragDirection)
    {
        var directionIndex = 0f;

        var vectorsAngle = Vector3.Angle(mouseDragDirection,
            _camera.WorldToScreenPoint(_currentHit.normal + _currentHit.point) -
            _camera.WorldToScreenPoint(_currentHit.point));
        if (vectorsAngle < 90)
        {
            directionIndex = 1f;
        }
        else
        {
            directionIndex = -1;
        }

        return directionIndex;
    }

    private Vector3 GetNextVertexPosition(float directionIndex, float approximationDistance, int vertexIndex)
    {
        var scaledDirection = directionIndex * Vector3.Distance(Input.mousePosition, _dragStartPoint);
        var vertexRemoteness = BrushSize - approximationDistance;
        var normal = _normals[vertexIndex];
        var editStartPoint = _meshEdited[vertexIndex];
        var futurePosition = editStartPoint + (scaledDirection * Force * vertexRemoteness * normal);
        return futurePosition;
    }
}
