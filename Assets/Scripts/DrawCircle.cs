using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DrawCircle : MonoBehaviour {
    [SerializeField] private int _segments;
    [SerializeField] private float _radius;

    private List<Vector2> GetCirclePoints(int segments, float radius) {
        List<Vector2> result = new List<Vector2>();
        float progressPerStep = (float) 1 / segments;
        float tau = 2f * Mathf.PI;
        float radianPerStep = tau * progressPerStep;
        for (int i = 0; i < segments; i++) {
            float currentRadian = radianPerStep * i;
            result.Add(new Vector2(Mathf.Cos(currentRadian) * radius, Mathf.Sin(currentRadian) * radius));
        }
        return result;
    }

    private void Update() {
        List<Vector2> points = GetCirclePoints(_segments, _radius);
        for (int i = 0; i < _segments; i++) {
            Debug.DrawLine(points[i], points[i + 1 >= points.Count ? 0 : i + 1], Color.red);
        }
    }
}
