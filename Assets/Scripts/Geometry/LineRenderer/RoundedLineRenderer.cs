using UnityEngine;

namespace Geometry {
    public class RoundedLineRenderer : GeometricalLineRenderer {
        [SerializeField] private int _roundCornersSegments = 16;

        public void Setup(float baseLineWidth, bool loop, Color lineColor, int sortingOrder, bool scalable, int roundCornersSegments) {
            _roundCornersSegments = roundCornersSegments;
            base.Setup(baseLineWidth, loop, lineColor, sortingOrder, scalable);
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