    using System;
    using UnityEngine;

namespace Geometry.Realisations {
    public class RectangularTriangle : Figure {
        [SerializeField] private GeometricalLineRenderer _redLineRenderer;
        [SerializeField] private GeometricalLineRenderer _blueLineRenderer;
        public override Vector2 GetCenter() {
            return (Points[0].transform.position + Points[1].transform.position + Points[2].transform.position) / 3f;
        }
        protected override void InitRules() {
            Points[0].Blocked = true;
            Points[1].Blocked = true;
            Points[2].Links.Add(new OrthogonalSnap(Points[2], Points[1], Points[0]));
        }

        protected override void DrawFigure() {
            _lineRenderer.SetPosition(0, Points[0].Position);
            _lineRenderer.SetPosition(1, Points[2].Position);
            _redLineRenderer.SetPosition(0, Points[0].Position);
            _redLineRenderer.SetPosition(1, Points[1].Position);
            _blueLineRenderer.SetPosition(0, Points[1].Position);
            _blueLineRenderer.SetPosition(1, Points[2].Position);
        }

        protected override Vector2[] DefaultPositions() {
            return new Vector2[] {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(1, 1)
            };
        }
        protected override int PointsAmount() {
            return 3;
        }

    }
}