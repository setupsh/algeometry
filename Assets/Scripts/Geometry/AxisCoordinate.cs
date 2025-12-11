using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;

namespace Geometry {
    public class AxisCoordinate : MonoBehaviour, ICameraListener {
        private GeometricalLineRenderer _lineRenderer;
        [SerializeField] private Color _lineColor;
        [SerializeField] private Color _labelColor;
        private List<Label> bufferX = new List<Label>(50);
        private List<Label> bufferY = new List<Label>(50);
        private float ceilSize;
        private int bufferSize;
        
        public enum Axis {X, Y}
        private void Start() {
            _lineRenderer = gameObject.AddComponent<GeometricalLineRenderer>();
            _lineRenderer.Setup(new LineRendererConfig(Parameters.Instance.DefaultLineWidth * 2f, false, _lineColor, Parameters.Instance.DefaultSortingOrder, true, true));
            for (int i = 0; i < bufferX.Capacity; i++) {
                bufferX.Add(GeometricalLabelSystem.Instance.CreateLabel("AxisCoordinate"));
                bufferX[i].SetColor(_labelColor);
            }
            for (int i = 0; i < bufferY.Capacity; i++) {
                bufferY.Add(GeometricalLabelSystem.Instance.CreateLabel("AxisCoordinate"));
                bufferY[i].SetColor(_labelColor);
            }
            FieldCamera.OnCameraChanged += OnCameraChanged;
        }

        private void OnDestroy() {
            FieldCamera.OnCameraChanged -= OnCameraChanged;
        }

        public void OnCameraChanged() {
            ceilSize = FieldCamera.Instance.CeilSize();
            Bounds bounds = FieldCamera.Instance.GetCameraBounds();
            float minX = Utilities.SnapToGridMin(bounds.min.x, ceilSize);
            float maxX = Utilities.SnapToGridMax(bounds.max.x, ceilSize);
            UpdateAxis(Axis.X, minX, maxX, bounds);
            
        }

        private void UpdateAxis(Axis axis, float min, float max, Bounds bounds) {
            int count = Mathf.Min(Mathf.RoundToInt((max - min) / ceilSize), bufferX.Capacity);
            if (count != bufferSize) {
                OffUnusedBuffer(axis, count);
            }
            bufferSize = count;
            float textSize = ceilSize * Parameters.Instance.DefaultLabelSize;
            float offset = 0f;
            Vector2 edge = CalculateEdge(bounds);
            
            for (int i = 0; i < count; i++) {
                float value = min + (ceilSize * (i + 1));
                Label label = GetLabel(axis, i);
                label.SetColor(_labelColor);
                label.SetSize(textSize);
                if (i == 0) {
                    label.ForceUpdate();
                    offset = label.GetRenderer().bounds.extents.y * 2f;
                    Debug.Log(offset);
                }
                label.SetText(Round(value, GetDecimalPlaces(ceilSize)).ToString());
                Vector2 worldPos = GetWorldPosition(axis, value, offset);
                
                if (bounds.ContainsCamera(worldPos)) {
                    label.SetPosition(worldPos);
                }
                else if (ContainsProjection(axis, bounds, worldPos)) {
                    SetProjectionPosition(label, worldPos, axis, bounds.center.y, offset, edge);
                }
                else {
                    label.SetColor(Color.clear);
                }
            }        
        }

        private Vector2 CalculateEdge(Bounds bounds) => bounds.center.y > 0f ? (bounds.center - bounds.extents) : (bounds.center + bounds.extents);

        private Vector2 GetWorldPosition(Axis axis, float value, float offset) {
            switch (axis) {
                case Axis.X: return new Vector2(value, -offset); 
                case Axis.Y: return new Vector2(-offset, value); 
                default: throw new ArgumentOutOfRangeException(nameof(axis), axis, null);
            }
        }

        private bool ContainsProjection(Axis axis, Bounds bounds, Vector2 worldPosition) {
            switch (axis) {
                case Axis.X: return bounds.ContainsCamera(new Vector2(worldPosition.x, bounds.center.y)); 
                case Axis.Y: return bounds.ContainsCamera(new Vector2(bounds.center.x, worldPosition.y)); 
                default: throw new ArgumentOutOfRangeException(nameof(axis), axis, null);
            }
        }

        private void SetProjectionPosition(Label label, Vector2 worldPosition, Axis axis, float boundsCenterY, float offset, Vector2 edgePosition) {
            if (boundsCenterY < 0f) offset = -offset;
            switch (axis) {
                case Axis.X: label.SetPosition(new Vector2(worldPosition.x, edgePosition.y + offset)); break;
                case Axis.Y: label.SetPosition(new Vector2(edgePosition.x  + offset, worldPosition.y)); break;
                default: throw new ArgumentOutOfRangeException(nameof(axis), axis, null);
            }
        }

        private Label GetLabel(Axis axis, int index) {
            switch (axis) {
                case Axis.X: return bufferX[index];
                case Axis.Y: return bufferY[index];
                default: throw new ArgumentOutOfRangeException(nameof(axis), axis, null);
            }
        }

        private void OffUnusedBuffer(Axis axis, int from) {
            List<Label> labels = axis == Axis.X ? bufferX : bufferY;
            if (from < labels.Capacity) {
                for (int i = from; i < labels.Capacity; i++) {
                    labels[i].SetColor(Color.clear);
                }
            }
        }

        private float Round(float value, int decimalsPlaces) {
            float places = Mathf.Pow(10f, decimalsPlaces);
            return MathF.Round(value * places) / places;
        }
        private int GetDecimalPlaces(float number) {
            if (number >= 1f) return 0;
            if (Mathf.Approximately(number, 0f)) return 0;
            int decimals = Mathf.Max(0, -Mathf.FloorToInt(Mathf.Log10(number)));
            return Mathf.Min(decimals, 6);
        }

    }
}