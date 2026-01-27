using System;
using UnityEngine;
using System.Collections.Generic;
using System.Globalization;
using Unity.Profiling;
using TMPro;

namespace Geometry {
    public class Matrix {
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
        private const string transparentMinus = "<color=#00000000>-";
        private Dictionary<int, Label> valuesX = new Dictionary<int, Label>();
        private Dictionary<int, Label> valuesY = new Dictionary<int, Label>();
        private float ceilSize;
        private Bounds bounds;
        private Vector2 edge;
        private float textSize;
        private int decimalPlaces;
        private Matrix previousMatrixX, previousMatrixY;
        static readonly ProfilerMarker AxisUpdateMarker =
            new ProfilerMarker("AxisCoordinate.UpdateAxis");
        
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
            OnCameraChanged();
        }
        private void OnDestroy() {
            FieldCamera.OnCameraChanged -= OnCameraChanged;
        }

        public void OnCameraChanged() {
            ceilSize = FieldCamera.Instance.CeilSize();
            bounds = FieldCamera.Instance.GetCameraBounds();
            textSize = ceilSize * Parameters.DefaultLabelSize;
            decimalPlaces = GetDecimalPlaces(ceilSize);
            edge = CalculateEdge(bounds);
            
            int minIdxX = Mathf.FloorToInt(bounds.min.x / ceilSize);
            int maxIdxX = Mathf.CeilToInt(bounds.max.x / ceilSize);

            int minIdxY = Mathf.FloorToInt(bounds.min.y / ceilSize);
            int maxIdxY = Mathf.CeilToInt(bounds.max.y / ceilSize);

            OnCameraMove(new Matrix(minIdxX, maxIdxX), Axis.X);
            OnCameraMove(new Matrix(minIdxY, maxIdxY), Axis.Y);
        }


        private void OnCameraMove(Matrix currentMatrix, Axis axis) {
            using (AxisUpdateMarker.Auto()) {
                var activeLabels = (axis == Axis.X) ? valuesX : valuesY;
                var prevMatrix = (axis == Axis.X) ? previousMatrixX : previousMatrixY;

                if (prevMatrix != null) {
                    List<int> toRemove = new List<int>();
                    foreach (var index in activeLabels.Keys) {
                        if (index < currentMatrix.Min || index > currentMatrix.Max) toRemove.Add(index);
                    }
                    foreach (int index in toRemove) RemoveLabel(index, axis);
                }
                
                for (int i = currentMatrix.Min; i <= currentMatrix.Max; i++) {
                    if (i == 0) continue;
                    if (!activeLabels.ContainsKey(i)) {
                        InitLabel(i, axis);
                    } else {
                        UpdateLabelPosition(activeLabels[i], i * ceilSize, i, axis);
                    }
                }
                
                if (axis == Axis.X) previousMatrixX = currentMatrix;
                else previousMatrixY = currentMatrix;
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
                text += transparentMinus;
            }
            label.TextMeshPro.SetText(text);
            label.TextMeshPro.fontSize = textSize;

            switch (axis) {
                case Axis.X: {
                    label.TextMeshPro.alignment = TextAlignmentOptions.Top;
                    label.TextMeshPro.rectTransform.pivot = new Vector2(0.5f, 1f); break;
                }
                case Axis.Y: {
                    label.TextMeshPro.alignment = TextAlignmentOptions.Right;
                    label.TextMeshPro.rectTransform.pivot = new Vector2(1f, 0.5f); break;
                }    
            }
            UpdateLabelPosition(label, value, index, axis);
        }

        private void UpdateLabelPosition(Label label,float value,int index, Axis axis) {
            Vector2 worldPos = GetWorldPosition(axis, value);
                
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

        private Vector2 CalculateEdge(Bounds bounds) => bounds.center.y > 0f ? (bounds.center - bounds.extents) : (bounds.center + bounds.extents);

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