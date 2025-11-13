using UnityEngine;
namespace Geometry {
    public class ArrowLineRenderer : GeometricalLineRenderer {
        [SerializeField] private float _arrowWidth;
        [SerializeField] private float _arrowHeight;

        public void Setup(float lineWidth, bool loop, Color lineColor, int sortingOrder, bool scalable, float arrowWidth, float arrowHeight) {
            _arrowWidth = arrowWidth;
            _arrowHeight = arrowHeight;
            base.Setup(lineWidth, loop, lineColor, sortingOrder, scalable);
        }
        protected override void GenerateLine(int index) {
            int segmentIndex = vertices.Count;
            base.GenerateLine(index);
            Line line = CreateLine(index);
            
            vertices.Add((Vector2) line.End + (line.Perpendicular * _arrowWidth));
            vertices.Add((Vector2) line.End - (line.Perpendicular * _arrowWidth));
            vertices.Add((Vector2) line.End + (line.Direction * _arrowHeight));

            triangles.Add(segmentIndex + 4);
            triangles.Add(segmentIndex + 6);
            triangles.Add(segmentIndex + 5);
        }

        protected override Line CreateLine(int index) {
            Line line = base.CreateLine(index);
            
            line.End = line.End - (line.Direction * _arrowHeight);
            return line;
        }
    }
}