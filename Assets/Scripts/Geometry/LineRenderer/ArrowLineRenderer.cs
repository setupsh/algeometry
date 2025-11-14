using UnityEngine;
namespace Geometry {
    public class ArrowLineConfig : LineRendererConfig {
        public float ArrowWidth { get; }
        public float ArrowHeight { get; }
        public ArrowLineConfig(float lineWidth, bool loop, Color lineColor, int sortingOrder, bool scalable, bool absolutePosition, float arrowWidth, float arrowHeight) : base(lineWidth, loop, lineColor, sortingOrder, scalable, absolutePosition) {
            ArrowWidth = arrowWidth;
            ArrowHeight = arrowHeight;
        }
    }
    public class ArrowLineRenderer : GeometricalLineRenderer {
        [SerializeField] private float _arrowWidth;
        [SerializeField] private float _arrowHeight;

        public void Setup(ArrowLineConfig arrowLineConfig) {
            _arrowWidth = arrowLineConfig.ArrowWidth;
            _arrowHeight = arrowLineConfig.ArrowHeight;
            base.Setup(arrowLineConfig);
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
            
            line.End -= (line.Direction * _arrowHeight);
            return line;
        }
    }
}