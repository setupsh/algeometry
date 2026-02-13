using System;
using UnityEngine;
using System.Collections.Generic;
using System.Globalization;
using Unity.Profiling;
using TMPro;

namespace Geometry {
    public struct Matrix {
        public int Min { get; private set; }
        public int Max { get; private set; }
        public Matrix(int min, int max) {
            Min = min;
            Max = max; 
        }
    }
        
    public class AxisCoordinate : MonoBehaviour {
        private GeometricalLineRenderer _lineRenderer;
        [SerializeField] private Color _lineColor;
        [SerializeField] private Color _labelColor;
        [SerializeField] private Transform _bufferFolder;
        private Stack<Label> buffer = new Stack<Label>();
        private Dictionary<int, Label> valuesX = new Dictionary<int, Label>();
        private Dictionary<int, Label> valuesY = new Dictionary<int, Label>();
        private float ceilSize;
        private Bounds bounds;
        private Vector2 edge;
        private float textSize;
        private int decimalPlaces;
        private bool CameraAbove => edge.y > 0;
        private bool CameraRight => edge.x > 0;
        private bool previousCameraAbove;
        private bool previousCameraRight;
        private List<int> moveToDelete = new List<int>(100);
        private List<int> reinitializeToDelete = new List<int>(100);
        
        public enum Axis {X, Y}
        private void Start() {
            _lineRenderer = gameObject.AddComponent<GeometricalLineRenderer>();
            _lineRenderer.Setup(new LineRendererConfig(Parameters.DefaultLineWidth * 2f, false, _lineColor, Parameters.DefaultSortingOrder, true, true));
            for (int i = 0; i < 100; i++) {
                Label label = GeometricalLabelSystem.Instance.CreateLabel(_bufferFolder, Parameters.DefaultSortingOrder);
                label.gameObject.name = $"Label {i}";
                buffer.Push(label);
            }
            FieldCamera.OnCameraChanged += OnCameraChanged;
            FieldCamera.OnCameraZoom += OnCameraZoom;
            FieldCamera.OnCameraMove += OnCameraMove;
            OnCameraChanged();
            OnCameraMove();
            OnCameraZoom();
        }
        private void OnDestroy() {
            FieldCamera.OnCameraChanged -= OnCameraChanged;
            FieldCamera.OnCameraZoom -= OnCameraZoom;
            FieldCamera.OnCameraMove -= OnCameraMove;
        }

        private void OnCameraChanged() {
            UpdateValues();
            UpdateAllLabelSides();
        }

        private void OnCameraZoom() {
            ReinitializeLabels(CalculateMatrix(Axis.X), Axis.X);
            ReinitializeLabels(CalculateMatrix(Axis.Y), Axis.Y);
        }

        private void OnCameraMove() {
            MoveLabels(CalculateMatrix(Axis.X), Axis.X);
            MoveLabels(CalculateMatrix(Axis.Y), Axis.Y);
        }

        private void UpdateValues() {
            ceilSize = FieldCamera.Instance.CeilSize();
            bounds = FieldCamera.Instance.GetCameraBounds();
            textSize = ceilSize * Parameters.DefaultLabelSize;
            decimalPlaces = GetDecimalPlaces(ceilSize);
            edge = CalculateEdge(bounds);
        }

        private Matrix CalculateMatrix(Axis axis) {
            switch (axis) {
                case Axis.X: return new Matrix(Mathf.FloorToInt(bounds.min.x / ceilSize), Mathf.FloorToInt(bounds.max.x / ceilSize));
                case Axis.Y: return new Matrix(Mathf.FloorToInt(bounds.min.y / ceilSize), Mathf.FloorToInt(bounds.max.y / ceilSize));
                default: throw new NullReferenceException();
            }
        }
        private void MoveLabels(Matrix currentMatrix, Axis axis) {
            var activeLabels = (axis == Axis.X) ? valuesX : valuesY;
            moveToDelete.Clear();
            
            foreach (var index in activeLabels.Keys) {
                if (index < currentMatrix.Min || index > currentMatrix.Max) moveToDelete.Add(index);
            }
            foreach (int index in moveToDelete) RemoveLabel(index, axis);
            
            for (int i = currentMatrix.Min; i <= currentMatrix.Max; i++) {
                if (i == 0) continue;
                if (!activeLabels.ContainsKey(i)) {
                    InitLabel(i, axis);
                } else {
                    UpdateLabelPosition(activeLabels[i], i * ceilSize, i, axis);
                }
            }
        }

        private void ReinitializeLabels(Matrix matrix, Axis axis) {
            Dictionary<int, Label> values = axis == Axis.X ? valuesX : valuesY;
            reinitializeToDelete.Clear();
            foreach (var kvp in values) {
                reinitializeToDelete.Add(kvp.Key);
            }
            foreach (var key in reinitializeToDelete) {
                values.Remove(key, out Label label);
                label.Renderer.enabled = false;
                buffer.Push(label);
            }
            for (int i = matrix.Min; i <= matrix.Max; i++) {
                if (i == 0) continue;
                InitLabel(i, axis);
            }
        }

        private void RemoveLabel(int index, Axis axis) {
            switch (axis) {
                case Axis.X: {
                    valuesX.Remove(index, out Label label);
                    label.Renderer.enabled = false;
                    buffer.Push(label);
                    break;
                }
                case Axis.Y: {
                    valuesY.Remove(index, out Label label);
                    label.Renderer.enabled = false;
                    buffer.Push(label);
                    break;
                }
            }
        }
        private void InitLabel(int index, Axis axis) {
            Label label = buffer.Pop();
            label.Renderer.enabled = true;
            switch (axis) {
                case Axis.X: valuesX[index] = label; break;
                case Axis.Y: valuesY[index] = label; break;
            }
            float value = index * ceilSize;
            string text = Round(value, decimalPlaces).ToString(CultureInfo.CurrentCulture);
            if (value < 0f && axis == Axis.X) {
                text = text + Utilities.TransparentMinus;
            }
            label.TextMeshPro.SetText(text);
            label.TextMeshPro.fontSize = textSize;

            switch (axis) {
                case Axis.X: {
                    label.TextMeshPro.rectTransform.pivot = new Vector2(0.5f, 1f); break;
                }
                case Axis.Y: {
                    label.TextMeshPro.rectTransform.pivot = new Vector2(1f, 0.5f); break;
                }    
            }
            UpdateLabelPosition(label, value, index, axis);
        }

        private void UpdateLabelPosition(Label label,float value,int index, Axis axis) {
            Vector2 worldPos = GetWorldPosition(axis, value);
            UpdateLabelSide(label, axis);
            if (bounds.ContainsCamera(worldPos)) {
                label.SetPosition(worldPos);
            }
            else if (ContainsProjection(axis, bounds, worldPos)) {
                SetProjectionPosition(label, worldPos, axis, edge);
            }
            else {
                RemoveLabel(index, axis);
            }
        }

        private Vector2 CalculateEdge(Bounds bounds) {
            Vector2 result = Vector2.zero;
            if (bounds.min.y > 0) {
                result.y = bounds.min.y;
            } else if (bounds.max.y < 0) {
                result.y = bounds.max.y;
            }
            if (bounds.min.x > 0) {
                result.x = bounds.min.x;
            } else if (bounds.max.x < 0) {
                result.x = bounds.max.x;
            }
            return result;
        }
        private void UpdateAllLabelSides() {
            bool sideChanged = previousCameraAbove != CameraAbove || previousCameraRight != CameraRight;
            if (!sideChanged) {
                return;
            }
            previousCameraAbove = CameraAbove;
            previousCameraRight = CameraRight;
            foreach (var label in valuesX.Values) {
                UpdateLabelSide(label, Axis.X);
            }

            foreach (var label in valuesY.Values) {
                UpdateLabelSide(label, Axis.Y);
            }
        }
        private void UpdateLabelSide(Label label, Axis axis) {
            var textMeshPro = label.TextMeshPro;

            if (axis == Axis.X) {
                textMeshPro.rectTransform.pivot = CameraAbove ? new Vector2(0.5f, 0f) : new Vector2(0.5f, 1f);
            }
            else {
                textMeshPro.rectTransform.pivot = CameraRight ? new Vector2(0f, 0.5f) : new Vector2(1f, 0.5f);
            }
        }

        private Vector2 GetWorldPosition(Axis axis, float value) {
            switch (axis) {
                case Axis.X: return new Vector2(value, 0); 
                case Axis.Y: return new Vector2(0, value); 
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

        private void SetProjectionPosition(Label label, Vector2 worldPosition, Axis axis, Vector2 edgePosition) {
            switch (axis) {
                case Axis.X: label.SetPosition(new Vector2(worldPosition.x, edgePosition.y)); break;
                case Axis.Y: label.SetPosition(new Vector2(edgePosition.x, worldPosition.y)); break;
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