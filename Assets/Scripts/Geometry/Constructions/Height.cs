using System;
using UnityEngine;

namespace Geometry {
    public class Height : Construction, ICameraListener {
        private GeometryPoint _from;
        private Side _to;
        private Color _color;
        private string _caption;
        private Label _label = null;
        private DashedLineRenderer _lineRenderer;

        public void Init(Figure parent, GeometryPoint from, Side to, Color color, string caption) {
            _from = from;
            _to = to;
            _color = color;
            _caption = caption;
            FieldCamera.OnCameraChanged += OnCameraChanged;
            base.Init(parent);
        }

        private void OnDisable() {
            FieldCamera.OnCameraChanged -= OnCameraChanged;
        }
        public override void UpdateConstruction() {
            _lineRenderer.SetPosition(0, _from.Position);
            _lineRenderer.SetPosition(1, GetHeightPoint());
            
            if (_label != null) {
                _label.SetText(_caption);
                _label.SetPosition(GetHeightPoint() + (GetHeightPoint() - _from.Position).normalized * 0.2f);
            }
        }

        protected override void CreateConstruction() {
            _lineRenderer = gameObject.AddComponent<DashedLineRenderer>();
            _lineRenderer.Setup(new DashedLineConfig(0.1f, false, _color, Parameters.DefaultSortingOrder, true, false,Parameters.HeightLineDashHeight, 1f));
            if (_caption != String.Empty) {
                _label = GeometricalLabelSystem.Instance.CreateLabel(transform);
            }
        }
        
        private Vector2 GetHeightPoint() {
            Vector2 dir = _to.Start.Position - _to.End.Position;
            float t = Vector2.Dot(_from.Position - _to.End.Position, dir) / dir.sqrMagnitude;
            return _to.End.Position + dir * t;
        }

        public void OnCameraChanged() {
            UpdateConstruction();
        }
    }
}