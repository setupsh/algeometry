using System;
using UnityEngine;
using System.Collections.Generic;

namespace Geometry {
    public struct Line {
        public Vector2 Start, End, Perpendicular, Direction;
    }

    public class LineRendererConfig {
        public float LineWidth { get; }
        public bool Loop { get; }
        public Color LineColor { get; }
        public int SortingOrder { get; }
        public bool Scalable { get; }
        
        public bool AbsolutePosition { get; }

        public LineRendererConfig(float lineWidth, bool loop, Color lineColor, int sortingOrder, bool scalable, bool absolutePosition) {
            LineWidth = lineWidth;
            Loop = loop;
            LineColor = lineColor;
            SortingOrder = sortingOrder;
            Scalable = scalable;
            AbsolutePosition = absolutePosition;
        }
    }
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class GeometricalLineRenderer : MonoBehaviour, ICameraListener {
        [SerializeField] protected List<Vector2> _points = new List<Vector2>();
        [SerializeField] protected float _lineWidth = 0.05f;
        [SerializeField] protected bool _loop;
        [SerializeField] protected Color _lineColor;
        [SerializeField] protected int _sortingOrder = 0;
        [SerializeField] protected bool _scalable = true;
        [SerializeField] protected bool _absolutePosition = false;
        protected float lineWidth;
        protected Mesh mesh;
        protected List<Vector3> vertices  = new List<Vector3>();
        protected List<int> triangles = new List<int>();
        protected List<Vector2> uvs = new List<Vector2>();
        public static readonly Vector2 VOID_POINT = new Vector2(float.NaN, float.NaN); 

        public void Setup(LineRendererConfig config) {
            _lineWidth = config.LineWidth;
            _loop = config.Loop;
            _lineColor = config.LineColor;
            _sortingOrder = config.SortingOrder;
            _scalable = config.Scalable;
            _absolutePosition = config.AbsolutePosition;
            Awake();
        }

        public void ClearPoints() {
            _points.Clear();
        }
        public void SetPosition(int index, Vector2 position) {
            if (_points.Count <= index) {
                _points.Add(position);
                GenerateMesh();
            }
            else {
                _points[index] = position;
                GenerateMesh();
            }
        }

        public void SetPositions(List<Vector2> points) {
            _points = points;
            GenerateMesh();
        }
        
        private void Awake() {
            lineWidth = _lineWidth;
            if (_scalable) FieldCamera.OnCameraChanged += OnCameraChanged;
            else FieldCamera.OnCameraChanged -= OnCameraChanged;
            mesh = new Mesh();
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            GetComponent<MeshFilter>().mesh = mesh;
            ApplyMaterial(meshRenderer);
            GenerateMesh();
        }

        private void OnDisable() {
            if (_scalable) FieldCamera.OnCameraChanged -= OnCameraChanged;
        }
        
        protected virtual void ApplyMaterial(MeshRenderer meshRenderer) {
            meshRenderer.sortingOrder = _sortingOrder;
            meshRenderer.material.color = _lineColor;
        }

        protected virtual void GenerateMesh() {
            vertices.Clear();
            triangles.Clear();
            uvs.Clear();
            mesh.Clear();
            
            for (int i = 0; i < _points.Count - 1; i++) {
                if (_points[i].IsVoid() || _points[i + 1].IsVoid()) continue;
                GenerateLine(i);
            }

            if (_loop && _points.Count > 1) {
                GenerateLine(_points.Count - 1);
            }
            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);
            mesh.SetUVs(0, uvs);
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
        }

        protected virtual void GenerateLine(int index) {
            Line line = CreateLine(index);
            
            int segmentIndex = vertices.Count;
            
            vertices.Add((Vector2) line.Start + line.Perpendicular);
            vertices.Add((Vector2) line.Start - line.Perpendicular);
            vertices.Add((Vector2) line.End + line.Perpendicular);
            vertices.Add((Vector2) line.End - line.Perpendicular);
            
            triangles.Add(segmentIndex + 0);
            triangles.Add(segmentIndex + 2);
            triangles.Add(segmentIndex + 1);
            
            triangles.Add(segmentIndex + 1);
            triangles.Add(segmentIndex + 2);   
            triangles.Add(segmentIndex + 3);
        }

        protected virtual Line CreateLine(int index) {
            Vector2 offset = _absolutePosition? Vector2.zero : transform.position;
            Vector2 start = _points[index] - offset;
            Vector2 end = _points[(index >= _points.Count - 1 && _loop) ? 0 : index + 1] - offset;
            Vector2 direction = (end - start).normalized;
            Vector2 perpendicular = new Vector2(-direction.y, direction.x) * (lineWidth / 2f);

            return new Line {
                Start = start,
                End = end,
                Direction = direction,
                Perpendicular = perpendicular
            };
        }

        public void OnCameraChanged() {
            lineWidth = _lineWidth / FieldCamera.Instance.ZoomLevel;
            GenerateMesh();
        }
    }
}