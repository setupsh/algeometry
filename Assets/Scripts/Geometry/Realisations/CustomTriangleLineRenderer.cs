using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Geometry;
using UnityEngine.Events;

public class CustomTriangleLineRenderer : MonoBehaviour {
    [SerializeField] private GeometricalLineRenderer _lineRenderer;
    [SerializeField] private GeometryPoint[] _points =  new GeometryPoint[3];
    //[SerializeField] private GeometricalLabel _labelSystem;
    [SerializeField] private float _letterFromSideOffset;
    
    private void OnEnable() {
        foreach (GeometryPoint point in _points) {
            point.OnPositionChanged += UpdateTriangle;
        }
    }
    private void OnDisable() {
        foreach (GeometryPoint point in _points) {
            point.OnPositionChanged -= UpdateTriangle;
        }
    }
    private void Start() {
        //_labelSystem.InitializeLabels(3, "A", "Ʊ", "C");
        UpdateTriangle();
    }
    private void UpdateTriangle() {
        for (int i = 0; i < _points.Length; i++) {
            _lineRenderer.SetPosition(i, _points[i].transform.position);
        }
        
        //Debug.Log(Mathf.CeilToInt(Utilities.GetAngle(_points[1].transform.position, _points[2].transform.position, _points[0].transform.position)));
        UpdateLetters(_letterFromSideOffset);
    }

    private void OnDrawGizmosSelected() {
        List<Vector2> segment = Utilities.GenerateAngleSector(_points[2].transform.position, _points[0].transform.position, _points[1].transform.position, 0.5f);
        for (int i = 0; i < segment.Count; i++) {
            Gizmos.DrawSphere(segment[i], 0.01f);
        }
    }

    private void UpdateLetters(float offsetFromSide = 0.25f) {
        Vector2 centerOfTriangle = (_points[0].transform.position + _points[1].transform.position + _points[2].transform.position) / 3;
        for (int i = 0; i < _points.Length; i++) {
            Vector2 middleBetweenPoints =  (_points[i].transform.position + _points[i == _points.Length - 1 ? 0 : i + 1].transform.position) * 0.5f;
            
            Vector2 side = _points[i == _points.Length - 1 ? 0 : i + 1].transform.position - _points[i].transform.position;
            
            Vector2 perpendicular = new Vector2(-side.y, side.x).normalized;
            
            Vector2 toCenter = (centerOfTriangle - middleBetweenPoints).normalized;
            
            if (Vector2.Dot(perpendicular, toCenter) > 0f) {
                perpendicular = -perpendicular;
            }
           // _labelSystem.UpdateLabelPosition(i, middleBetweenPoints + perpendicular * offsetFromSide);
        }
    }

}
