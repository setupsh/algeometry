using UnityEngine;
using System.Collections;
using Geometry;

public class TriangleLineRenderer : MonoBehaviour {
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private GeometryPoint[] _points =  new GeometryPoint[3];
    //[SerializeField] private GeometricalLabel _labelSystem;

    // Update is called once per frame
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
        _lineRenderer.positionCount = _points.Length;
       // _labelSystem.InitializeLabels(3, "A", "B", "C");
        UpdateTriangle();
    }
    private void UpdateTriangle() {
        for (int i = 0; i < _points.Length; i++) {
            _lineRenderer.SetPosition(i, _points[i].transform.position);
        }
        UpdateLetters();
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
            //_labelSystem.UpdateLabelPosition(i, middleBetweenPoints + perpendicular * offsetFromSide);
            Debug.DrawLine(centerOfTriangle, middleBetweenPoints, Color.red);
        }
    }
}
