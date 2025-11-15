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
            Debug.Log(_point.Label);
            _label.SetText(_point.Label);
            _label.SetColor(_color);
            _label.SetPosition(_offset.CalculateOffset());
        }

        protected override void CreateConstruction() {
            _label = GeometricalLabelSystem.Instance.CreateLabel(Parent.name + "Constructions");
        }

        public void OnCameraChanged() {
            UpdateConstruction();
        }
    }
}