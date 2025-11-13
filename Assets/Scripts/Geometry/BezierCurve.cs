using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Geometry {
    public class BezierCurve {

        public static Vector2[] GenerateBezierCurve(Transform p0, Transform p1, Transform p2, Transform p3, float step = 0.01f) {
            Vector2 p0Position = p0.position;
            Vector2 p1Position = p1.position;
            Vector2 p2Position = p2.position;
            Vector2 p3Position = p3.position;
            List<Vector2> result = new List<Vector2>();
            for (float t = 0; t <= 1; t += step) {
                result.Add(GenerateBezierCurvePoint(t, p0Position, p1Position, p2Position, p3Position));
            }
            return result.ToArray();
        }

        private static Vector2 GenerateBezierCurvePoint(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3) {
            float u = 1 - t;
            float uu = u * u;
            float uuu = uu * u;
            float tt = t * t;
            float ttt = tt * t;
            
            Vector2 point = 
                uuu * p0 +           // (1-t)^3 * P0
                3 * uu * t * p1 +    // 3*(1-t)^2*t * P1
                3 * u * tt * p2 +    // 3*(1-t)*t^2 * P2
                ttt * p3;            // t^3 * P3
            
            return point;
        }
    }
}