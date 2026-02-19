using System;
using UnityEngine;
using System.Collections.Generic;
using Geometry.Realisations;

namespace Geometry.Realisations {
    public class Circle : Figure {
        [SerializeField] private int _segments;
        private List<GeometryPoint> linkedPoints =  new List<GeometryPoint>();
        private float Radius => Vector2.Distance(Points[0].transform.position, Points[1].transform.position);
        private Vector2 Center => Points[0].transform.position;

        public void LinkPoint(GeometryPoint point) {
            if (linkedPoints.Contains(point)) {
                linkedPoints.Remove(point);
                point.Constrain = null;
            }
            else {
                linkedPoints.Add(point);
                point.Constrain = new OnCircleConstrain(this);
                OnFigureChanged += () => point.Position = point.Position;
            }
        }

        private void OnDestroy() {
            foreach (GeometryPoint point in linkedPoints) {
                point.Constrain = null;
            }
        }
        
        protected override void DrawFigure() {
            List<Vector2> points = GetCirclePoints();
            _lineRenderer.SetPosition(PointsAmount(), GeometricalLineRenderer.VOID_POINT);
            for (int i = 0; i < points.Count; i++) {
                _lineRenderer.SetPosition(PointsAmount() + i + 1, points[i]);
            }
        }

        public override Vector2 GetCenter() {
            return Points[0].Position;
        }

        protected override int PointsAmount() {
            return 2;
        }

        protected override void InitRules() {
            Points[0].Links.Add(new Copy(Points[0], Points[1], Coordinate.Both));
        }

        protected override Vector2[] DefaultPositions() {
            return new Vector2[] {
                new Vector2(0, 0), new Vector2(0, 1)
            };
        }
        private List<Vector2> GetCirclePoints() {
            List<Vector2> result = new List<Vector2>();
            float progressPerStep = (float) 1 / _segments;
            float tau = 2f * Mathf.PI;
            float radianPerStep = tau * progressPerStep;
            for (int i = 0; i <= _segments; i++) {
                float currentRadian = radianPerStep * i;
                result.Add(new Vector2((Mathf.Cos(currentRadian) * Radius), Mathf.Sin(currentRadian) * Radius) + Center);
            }
            return result;
        }

        public Vector2 GetProjection(Vector2 point) {
            Vector2 direction =  point - Center;
            return Center + direction.normalized * Radius;
        }
    }
    [System.Serializable]
    public class PointLinkedToCircleGenerator : ConstructionGenerator {
        public override void Generate(List<IGeometryValue> arguments) {
            Circle circle = Get<Circle>(arguments, 0);
            GeometryPoint pointToLink = Get<GeometryPoint>(arguments, 1);
            circle.LinkPoint(pointToLink);
        }
    }
}

