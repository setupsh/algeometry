using UnityEngine;

namespace Geometry {
    public class RoundedLineRendererConfig : LineRendererConfig {
        public int RoundedCornersSegments { get; }
        public RoundedLineRendererConfig(float lineWidth, bool loop, Color lineColor, int sortingOrder, bool scalable, bool absolutePosition, int roundedCornersSegments) : base(lineWidth, loop, lineColor, sortingOrder, scalable, absolutePosition) {
            RoundedCornersSegments = roundedCornersSegments;
        }
    }
    public class RoundedLineRenderer : GeometricalLineRenderer {
        [SerializeField] private int _roundCornersSegments = 16;

        public void Setup(RoundedLineRendererConfig config) {
            _roundCornersSegments = config.RoundedCornersSegments;
            base.Setup(config);
        }
        protected override void GenerateLine(int index) {
            base.GenerateLine(index);
            Line line = CreateLine(index);
            int arcStart = vertices.Count;
            foreach (var point in Utilities.GenerateSector(line.End, line.End - line.Perpendicular, line.End + line.Perpendicular, _roundCornersSegments)) {
                vertices.Add( point);
            }
                
            for (int i = 1; i < _roundCornersSegments + 1; i++) {
                triangles.Add(arcStart);
                triangles.Add(arcStart + i + 1);
                triangles.Add(arcStart + i );
            }
        } 
    }
}