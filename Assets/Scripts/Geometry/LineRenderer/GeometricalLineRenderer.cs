using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;

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
    public class 
        GeometricalLineRenderer : MonoBehaviour {
        [SerializeField] protected List<Vector2> _points = new List<Vector2>();
        [SerializeField] protected float _lineWidth = 0.05f;
        [SerializeField] protected bool _loop;
        [SerializeField] protected Color _lineColor;
        [SerializeField] protected int _sortingOrder = 0;
        [SerializeField] protected bool _scalable = true;
        [SerializeField] protected bool _absolutePosition = false;
        [SerializeField] protected List<Color> _segmentColors = new List<Color>();
        
        public List<Vector2> Points => _points;
        public List<Color> SegmentColors => _segmentColors;
        public LineRendererConfig CurrentConfig => _config;
        private LineRendererConfig _config;
        protected float lineWidth;
        protected Mesh mesh;
        protected List<Vector3> vertices  = new List<Vector3>();
        protected List<int> triangles = new List<int>();
        protected List<Vector2> uvs = new List<Vector2>();
        protected List<Color> colors = new List<Color>();  
        public static readonly Vector2 VOID_POINT = new Vector2(float.NaN, float.NaN); 

        public void Setup(LineRendererConfig config) {
            _lineWidth = config.LineWidth;
            _loop = config.Loop;
            _lineColor = config.LineColor;
            _sortingOrder = config.SortingOrder;
            _scalable = config.Scalable;
            _absolutePosition = config.AbsolutePosition;
            _config = config;
            GenerateMesh();
            OnCameraZoom();
        }
        
        protected virtual void Awake() {
            Init();
            Setup(new LineRendererConfig(_lineWidth, _loop, _lineColor, _sortingOrder, _scalable, _absolutePosition));
        }

        protected void Init() {
            if (_scalable) FieldCamera.OnCameraZoom += OnCameraZoom;
            mesh = new Mesh();
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            GetComponent<MeshFilter>().mesh = mesh;
            ApplyMaterial(meshRenderer);
            OnCameraZoom();
        }
        
        public void ClearPoints() {
            _points.Clear();
            GenerateMesh();
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

        private void OnDisable() {
            if (_scalable) FieldCamera.OnCameraZoom -= OnCameraZoom;
        }
        
        protected virtual void ApplyMaterial(MeshRenderer meshRenderer) {
            meshRenderer.material = Materials.Get(MaterialType.Sprite);
            meshRenderer.sortingOrder = _sortingOrder;
        }
        
        public void SetSegmentColor(int index, Color color) {
            while (_segmentColors.Count <= index)
                _segmentColors.Add(_lineColor);
 
            _segmentColors[index] = color;
            GenerateMesh();
        }
        
        public void SetSegmentColors(List<Color> colors) {
            _segmentColors = new List<Color>(colors);
            GenerateMesh();
        }
        public void ClearSegmentColors() {
            _segmentColors.Clear();
            GenerateMesh();
        }
        protected Color GetSegmentColor(int index) {
            if (_segmentColors != null && index < _segmentColors.Count)
                return _segmentColors[index];
            return _lineColor;
        }    

        protected virtual void GenerateMesh() {
            vertices.Clear();
            triangles.Clear();
            uvs.Clear();
            colors.Clear();
            mesh.Clear();
    
            for (int i = 0; i < _points.Count - 1; i++) {
                if (_points[i].IsVoid() || _points[i + 1].IsVoid()) continue;
                GenerateLine(i);
            }

            if (_loop && _points.Count > 1) {
                GenerateLine(_points.Count - 1);
            }
            
            while (colors.Count < vertices.Count)
                colors.Add(_lineColor);

            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);
            mesh.SetUVs(0, uvs);
            mesh.SetColors(colors);
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
            
            Color segColor = GetSegmentColor(index);
            
            colors.Add(segColor);
            colors.Add(segColor);
            colors.Add(segColor);
            colors.Add(segColor);
            
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

        private void OnCameraZoom() {
            lineWidth = _lineWidth * FieldCamera.Instance.CeilSize();
            GenerateMesh();
        }
    }
}