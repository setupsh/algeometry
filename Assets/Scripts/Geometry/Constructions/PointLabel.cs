using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
namespace Geometry {
    public class PointLabel : Construction, ICameraListener {
        private GeometryPoint _point;
        private string _caption;
        private Label _label = null;
        private Color _color;
        private OffsetConfig _offset;
        
        public void Init(Figure parent, GeometryPoint point, Color color, OffsetConfig offsetConfig) {
            _point = point;
            _color = color;
            _offset = offsetConfig;
            base.Init(parent);
            FieldCamera.OnCameraChanged += OnCameraChanged;
        }

        private void OnDisable() {
            FieldCamera.OnCameraChanged -= OnCameraChanged;
        }
        public override void UpdateConstruction() {
            _label.TextMeshPro.fontSize = Parameters.DefaultLabelSize * FieldCamera.Instance.CeilSize();
            _label.SetPosition(_offset.CalculateOffset());
        }

        protected override void CreateConstruction() {
            _label = GeometricalLabelSystem.Instance.CreateLabel(transform, Parameters.LabelSortingOrder);
            _label.TextMeshPro.text = _point.Label;
            _label.TextMeshPro.color = _color;
            UpdateConstruction();
        }

        public void OnCameraChanged() {
            UpdateConstruction();
        }
    }

    [System.Serializable]
    public class LabelsGenerator : ConstructionGenerator {
        public override void Generate(List<IGeometryValue> arguments) {
            Figure figure = Get<Figure>(arguments, 0);
            Color color = Get<Color>(arguments, 1);
            float multiplier = Get<float>(arguments, 2);
            foreach (GeometryPoint point in figure.Points) {
                Board.Instance.Instantiate<PointLabel>().Init(figure, point, color, new CentroidOffset(figure.GetCenter, () => point.Position, multiplier));
            }
        }
    }
}