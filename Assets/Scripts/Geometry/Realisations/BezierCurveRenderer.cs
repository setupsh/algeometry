using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Geometry;
public class BezierCurveRenderer : MonoBehaviour {
    [SerializeField] private GeometryPoint[] points = new GeometryPoint[4];
    [SerializeField] private float _step;
    
    private void OnEnable() {
        foreach (GeometryPoint point in points) {
            point.OnPositionChanged += UpdateCurve;
        }
    }
    private void OnDisable() {
        foreach (GeometryPoint point in points) {
            point.OnPositionChanged -= UpdateCurve;
        }
    }

    private void UpdateCurve() {
        Debug.Log("UpdateCurve");
        Vector2[] curve = BezierCurve.GenerateBezierCurve( points[0].transform, points[1].transform, points[2].transform, points[3].transform, _step );
        
        for (int i = 0; i < curve.Length; i++) {
            Debug.Log(curve[i]);
            if (i + 1 > curve.Length - 1) break;
            Debug.DrawLine(curve[i], curve[i + 1], Color.red);
        }
    }
}
