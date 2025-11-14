using UnityEngine;

namespace Geometry {
    public class DashedLineConfig : LineRendererConfig {
        public float DashLenght { get; }
        public float TimeMultiplier { get; }
        public DashedLineConfig(float lineWidth, bool loop, Color lineColor, int sortingOrder, bool scalable, bool absolutePosition, float dashLenght, float timeMultiplier) : base(lineWidth, loop, lineColor, sortingOrder, scalable, absolutePosition) {
            DashLenght = dashLenght;
            TimeMultiplier = timeMultiplier;
        }
    }
    public class DashedLineRenderer :  GeometricalLineRenderer {
        [SerializeField] private float _dashLenght;
        [SerializeField] private float _timeMultiplier = 0f;
        private float accumulatedLength;

        public void Setup(DashedLineConfig dashedLineConfig) {
            _dashLenght = dashedLineConfig.DashLenght;
            _timeMultiplier = dashedLineConfig.TimeMultiplier;
            base.Setup(dashedLineConfig);
        }
        protected override void GenerateMesh() {
            accumulatedLength = 0f;
            base.GenerateMesh();
        }

        protected override void ApplyMaterial(MeshRenderer meshRenderer) {
            meshRenderer.material.shader = Shader.Find(ShadersKeys.DashedLine.Name);
            meshRenderer.material.SetFloat(ShadersKeys.DashedLine.Length, _dashLenght);
            meshRenderer.material.SetFloat(ShadersKeys.DashedLine.TimeMultiplier, _timeMultiplier);
            base.ApplyMaterial(meshRenderer);
        }

        protected override void GenerateLine(int index) {
            if (index >= _points.Count - 1 && !_loop || _points[index] == VOID_POINT) return;
            base.GenerateLine(index);
            Line line = CreateLine(index);
            float segmentLength = Vector2.Distance(line.Start, line.End);
                
            uvs.Add(new Vector2(accumulatedLength, 1));
            uvs.Add(new Vector2(accumulatedLength, 0));
            uvs.Add(new Vector2(accumulatedLength + segmentLength, 1));
            uvs.Add(new Vector2(accumulatedLength + segmentLength, 0));
            accumulatedLength += segmentLength;
        }
    }
}