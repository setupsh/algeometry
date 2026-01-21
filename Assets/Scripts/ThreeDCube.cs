using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Geometry;

public class ThreeDCube : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GeometricalLineRenderer _lineRenderer;
    [SerializeField] private GeometricalLineRenderer _juicyLineRenderer;
    [SerializeField] private float _size = 2f;
    [SerializeField] private float _rotationDamping = 500f;
    [SerializeField] private float _throwForce = 30f;
    private Vector3 _rotationVelocity = Vector3.zero;
    

    private Vector3[] _cubeVertices;
    private int[] _cubeIndices;
    private Vector3 _currentRotationEuler;
    private List<Vector2> projectedPoints = new List<Vector2>(16);
    private Vector2 startPoint, endPoint;

    void Start() {
        _cubeVertices = new Vector3[] {
            new Vector3(-1, -1,  1), new Vector3(1, -1,  1), new Vector3(1,  1,  1), new Vector3(-1,  1,  1),
            new Vector3(-1, -1, -1), new Vector3(1, -1, -1), new Vector3(1,  1, -1), new Vector3(-1,  1, -1)
        };
        
        _cubeIndices = new int[] { 
            0, 1, 2, 3, 0, 
            4, 5, 6, 7, 4,
            5, 1, 2, 6, 7, 3 
        };
        for (int i = 0; i < _cubeIndices.Length; i++) 
            projectedPoints.Add(Vector2.zero);
        
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireCube(transform.position, Vector3.one * _size);
    }

    private void Update() {
        _rotationVelocity = Vector3.MoveTowards(
            _rotationVelocity, 
            Vector3.one * 5f, 
            _rotationDamping * Time.deltaTime);
        _currentRotationEuler += _rotationVelocity * Time.deltaTime;
        DrawCube();
    }

    private void OnMouseDown() {
        _juicyLineRenderer.SetPosition(0, Camera.main.ScreenToWorldPoint(InputListener.MousePosition));
    }

    private void OnMouseDrag() {
        _juicyLineRenderer.SetPosition(1, Camera.main.ScreenToWorldPoint(InputListener.MousePosition));
    }

    private void OnMouseUp() {
        _rotationVelocity = (_juicyLineRenderer.Points[1] - _juicyLineRenderer.Points[0]) ;
        _rotationVelocity = new Vector3(_rotationVelocity.y, -_rotationVelocity.x, 0) * _throwForce;
        _juicyLineRenderer.ClearPoints();
    }
    

    private void DrawCube() {
        Quaternion rotation = Quaternion.Euler(_currentRotationEuler);
        Vector2 centerPoint = transform.position;
        for (int i = 0; i < _cubeIndices.Length; i++) {
            int index = _cubeIndices[i];
            Vector3 vertex = _cubeVertices[index] * (_size * 0.5f);
            
            Vector3 rotated = rotation * vertex;
            
            Vector2 finalPoint = new Vector2(rotated.x, rotated.y);
            
            projectedPoints[i] = (centerPoint + finalPoint);
            
        }
        _lineRenderer.SetPositions(projectedPoints);
    }
}