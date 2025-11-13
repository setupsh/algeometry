using JetBrains.Annotations;
using UnityEngine;
namespace Geometry {
    public class PointLabel : Construction, ICameraListener {
        private GeometryPoint _point;
        private string _caption;
        private Label _label = null;
        private Color _color;
        private Vector2 _offset;
        
        public void Init(Figure parent, GeometryPoint point, string caption, Color color, Vector2 offset) {
            _point = point;
            _caption = caption;
            _color = color;
            _offset = offset;
            base.Init(parent);
            FieldCamera.OnCameraChanged += OnCameraChanged;
        }

        private void OnDisable() {
            FieldCamera.OnCameraChanged -= OnCameraChanged;
        }
        public override void UpdateConstruction() {
            if (Parent != null) {
                _label.SetText(_caption);
                _label.SetColor(_color);
                _label.SetPosition(_point.Position + (_point.Position - Parent.GetCenter()).normalized * 0.2f);
            }
            else {
                _label.SetText(_caption);
                _label.SetColor(_color);
                _label.SetPosition(_point.Position + _offset);
            }
        }

        protected override void CreateConstruction() {
            _label = GeometricalLabelSystem.Instance.CreateLabel(Parent.name + "Constructions");
        }

        public void OnCameraChanged() {
            UpdateConstruction();
        }
    }
}