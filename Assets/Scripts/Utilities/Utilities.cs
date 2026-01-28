using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;

namespace Geometry {
    public static class Utilities {
        public const float Epsilon = 0.0001f;
        public static readonly List<string> Captions = new List<string> {
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S",
        };
        public static bool IsVoid(this Vector2 p) => float.IsNaN(p.x) || float.IsNaN(p.y);

        public static bool ContainsCamera(this Bounds bounds, Vector2 point) {
            return bounds.Contains(new Vector3(point.x, point.y, Camera.main.transform.position.z));
        }
        public static bool Is<T>(this object obj, out T result) where T : class {
            result = obj as T;
            return obj as T != null;
        }

        public static string RichTextEffect(string effectTag, string text) {
            return $"<{effectTag}>{text}</{effectTag}>";
        }
    
        public static bool IsSubclass(this object obj, System.Type type) {
            return obj.GetType().IsSubclassOf(type);
        }
        // TYPING
        public static bool IsA(this object obj, System.Type type) {
            return type.IsAssignableFrom(obj.GetType());
        }

        public static bool TryConvert<T>(this object obj, out T result) {
            result = default;
            if (obj.IsA(typeof(T)) || obj.IsSubclass(typeof(T)))
                result = (T)obj;
            return result != null;
        }

        public static float SnapToGrid(float value, float step) {
            return value > 0f ? Mathf.Ceil(value / step) * step : Mathf.Floor(value / step) * step;
        }

        public static float SnapToGridMin(float value, float step) {
            return Mathf.Floor(value / step) * step;
        }

        public static float SnapToGridMax(float value, float step) {
            return Mathf.Ceil(value / step) * step;
        }
        public static void SetPivotAndAlignment(this TextMeshPro textMeshPro, Vector2 pivot, TextAlignmentOptions alignment) {
            if (pivot != textMeshPro.rectTransform.pivot && alignment != textMeshPro.alignment) {
                textMeshPro.rectTransform.pivot = pivot;
                textMeshPro.alignment = alignment;
            }
        }
        
        
        //-=CIRCLES SECTORS AND ALL ROUNDED SHIT-=
        public static List<Vector2> GetCirclePoints(Vector2 center, int segments, float radius) {
            List<Vector2> result = new List<Vector2>();
            float progressPerStep = (float) 1 / segments;
            float tau = 2f * Mathf.PI;
            float radianPerStep = tau * progressPerStep;
            for (int i = 0; i < segments; i++) {
                float currentRadian = radianPerStep * i;
                result.Add(new Vector2((Mathf.Cos(currentRadian) * radius), Mathf.Sin(currentRadian) * radius) + center);
            }
            return result;
        }

        public static List<Vector3> GenerateSector(Vector2 center, Vector2 start, Vector2 end, int segments = 20) {
            List<Vector3> points = new List<Vector3>(segments + 2);
            for (int i = 0; i < segments + 2; i++) 
                points.Add(Vector2.zero);
            points[0] = center;
            
            Vector2 dirStart = (start - center).normalized;
            Vector2 dirEnd = (end - center).normalized;

            float radius = (start - center).magnitude;
            
            float angleStart = Mathf.Atan2(dirStart.y, dirStart.x);
            float angleEnd = Mathf.Atan2(dirEnd.y, dirEnd.x);
            
            if (angleEnd < angleStart)
                angleEnd += Mathf.PI * 2;

            float angleStep = (angleEnd - angleStart) / segments;
            
            for (int i = 0; i <= segments; i++)
            {
                float angle = angleStart + angleStep * i;
                Vector3 point = center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
                points[i + 1] = point;
            }

            return points;
        }
        public static void FillSector(List<Vector3> targetList, Vector2 center, Vector2 start, Vector2 end, int segments = 20) {
            targetList.Add(center);
    
            Vector2 dirStart = (start - center).normalized;
            Vector2 dirEnd = (end - center).normalized;
            float radius = (start - center).magnitude;
    
            float angleStart = Mathf.Atan2(dirStart.y, dirStart.x);
            float angleEnd = Mathf.Atan2(dirEnd.y, dirEnd.x);
    
            if (angleEnd < angleStart) angleEnd += Mathf.PI * 2;

            float angleStep = (angleEnd - angleStart) / segments;
    
            for (int i = 0; i <= segments; i++) {
                float angle = angleStart + angleStep * i;
                targetList.Add(center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius);
            }
        }
        
        public static List<Vector2> GenerateAngleSector(Vector2 center, Vector2 sideA, Vector2 sideB, float radius, int segments = 20, bool shortestAngle = true) {
            List<Vector2> points = new List<Vector2>();
            points.Add(center);
            
            Vector2 dirA = (sideA - center).normalized;
            Vector2 dirB = (sideB - center).normalized;
            
            float angleA = Mathf.Atan2(dirA.y, dirA.x);
            float angleB = Mathf.Atan2(dirB.y, dirB.x);
            
            float deltaDeg = Mathf.DeltaAngle(angleA * Mathf.Rad2Deg, angleB * Mathf.Rad2Deg);

            if (!shortestAngle) {
                if (deltaDeg > 0)
                    deltaDeg -= 360f;
                else
                    deltaDeg += 360f;
            }

            float deltaRad = deltaDeg * Mathf.Deg2Rad;
            float angleStep = deltaRad / segments;

            for (int i = 0; i <= segments; i++) {
                float angle = angleA + angleStep * i;
                Vector2 point = center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
                points.Add(point);
            }

            return points;
        }
        
        //-=ANGLES-=
        public static float GetSighedAngle(Vector2 a, Vector2 b, Vector2 c) => Vector2.SignedAngle(a - b, c - b);
        
        public static float GetAngle(Vector2 a, Vector2 b, Vector2 c) => Vector2.Angle(a - b, c - b);

        public static string ToStringDecimalPlaces(this float value, int decimalPlaces) {
            return string.Format("{0:F" + decimalPlaces + "}", value);
        }
        
        //-=RULES-HELPERS=-



    }

}