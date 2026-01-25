using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;
using Geometry;

namespace Geometry {
    public class Angle : Construction {
        private GeometryPoint _vertex, _armA, _armB;
        private int _arcAmount;
        private Color _color;
        private GeometricalLineRenderer _lineRenderer;
        private float _angleValue;
        private bool _showLabel;
        private Label _label = null;
        public float Value => _angleValue;

        public void Init(Figure parent, GeometryPoint armA, GeometryPoint vertex, GeometryPoint armB, Color color, bool showLabel) {
            _vertex = vertex;
            _armA = armA;
            _armB = armB;
            _color = color;
            _showLabel = showLabel;
            FieldCamera.OnCameraChanged += OnCameraChanged;

            base.Init(parent);
        }

        private void OnDisable() {
            FieldCamera.OnCameraChanged -= OnCameraChanged;
        }
        public override void UpdateConstruction() {
            _angleValue = Utilities.GetAngle(_armA.transform.position, _vertex.transform.position, _armB.transform.position);
            List<Vector2> points = Utilities.GenerateAngleSector(_vertex.transform.position, _armA.transform.position, _armB.transform.position, CalculateRadius(), 25);
            points.RemoveAt(0);
            for (int i = 0; i < points.Count; i++) {
                _lineRenderer.SetPosition(i, points[i]);
            }
            if (_showLabel && _label) {
                _label.TextMeshPro.text = _angleValue.ToStringDecimalPlaces(2);
                _label.SetPosition(_vertex.Position - (_vertex.Position - points[Mathf.RoundToInt(points.Count / 2f)]) * Parameters.OffsetTextFromAngleMultiplier);
                //TODO Динамически обновлять размер
                _label.TextMeshPro.fontSize = Parameters.DefaultLabelSize;
                _label.TextMeshPro.color = _color;
            }
            
        }

        protected override void CreateConstruction() {
            _lineRenderer = gameObject.AddComponent<GeometricalLineRenderer>();
            _lineRenderer.Setup(new LineRendererConfig(Parameters.AngleWidth, false, _color, Parameters.DefaultSortingOrder - 1, true, false));
            if (_showLabel) {
                _label = GeometricalLabelSystem.Instance.CreateLabel(transform, Parameters.LabelSortingOrder);
            }
        }

        private float CalculateRadius() {
            float AngleRadius = Parameters.AngleRadius;
            return FieldCamera.Instance.ZoomLevel > 1f ? Parameters.AngleRadius / (FieldCamera.Instance.ZoomLevel) : Parameters.AngleRadius;
            
            //if (Vector2.Distance(_vertex.Position, _armA.Position) < 1.5f || Vector2.Distance(_vertex.Position, _armA.Position) < 1.5f) {
            //    return AngleRadius;
            //}
            if (_angleValue < 30f) {
                if (_angleValue < 10f) {
                    return AngleRadius + (Mathf.Cos(_angleValue * Mathf.Deg2Rad) * AngleRadius * 5f);
                }
                return AngleRadius + (Mathf.Cos(_angleValue * Mathf.Deg2Rad) * AngleRadius * 2.5f);
            }
            return AngleRadius + (Mathf.Clamp(Mathf.Cos(_angleValue * Mathf.Deg2Rad), 0, 1) * AngleRadius);
        }
        
        public void OnCameraChanged() {
            UpdateConstruction();
        }
    }
    [System.Serializable]
    public class AngleGenerator : ConstructionGenerator {
        public override void Generate(List<IGeometryValue> arguments) {
            Board.Instance.Instantiate<Angle>().Init(
                Get<Figure>(arguments, 0),
                Get<GeometryPoint>(arguments, 1),
                Get<GeometryPoint>(arguments, 2),
                Get<GeometryPoint>(arguments, 3),
                Get<Color>(arguments, 4),
                Get<bool>(arguments, 5));
        }
    }
}
