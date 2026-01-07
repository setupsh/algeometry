using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Geometry;

namespace Geometry {
    public class Grid : MonoBehaviour, ICameraListener {
        [SerializeField] private GeometricalLineRenderer _lineRendererReference;
        [SerializeField] private GameObject _lineStorage;
        [SerializeField] private float _lineWidth;
        [SerializeField] private Color _lineColor;
        private GeometricalLineRenderer _lineRendererVertical,  _lineRendererHorizontal;
        private Bounds _cameraBounds;
        private float _ceilSize = 1f;
        private float ceilSize;
        private float _startX, _endX,  _startY, _endY;
        private int overflowLines = 3;
        private float _zoom;
        private void Start() {
            ceilSize = _ceilSize / FieldCamera.Instance.ZoomLevel;
            _cameraBounds = FieldCamera.Instance.GetCameraBounds();
            _lineRendererVertical = SetupLineRenderer(_lineWidth, _lineColor);
            _lineRendererHorizontal = SetupLineRenderer(_lineWidth, _lineColor);
            _startX = Utilities.SnapToGrid(_cameraBounds.min.x, ceilSize);
            _startY = Utilities.SnapToGrid(_cameraBounds.min.y, ceilSize);
            _zoom = FieldCamera.Instance.ZoomLevel;
            BuildGrid();
        }

        private void OnEnable() {
            FieldCamera.OnCameraChanged += OnCameraChanged;
        }

        private void OnDisable() {
            FieldCamera.OnCameraChanged -= OnCameraChanged;
        }

        private void BuildGrid() {
            float startX = Utilities.SnapToGridMin(_cameraBounds.min.x, ceilSize) - ceilSize * overflowLines;
            float endX = Utilities.SnapToGridMax(_cameraBounds.max.x, ceilSize) + ceilSize * overflowLines;
            float startY = Utilities.SnapToGridMin(_cameraBounds.min.y , ceilSize) - ceilSize * overflowLines;
            float endY = Utilities.SnapToGridMax(_cameraBounds.max.y , ceilSize) + ceilSize * overflowLines;
            
            _lineRendererHorizontal.transform.localPosition = Vector2.zero;
            _lineRendererVertical.transform.localPosition = Vector2.zero;

            List<Vector2> verticalPoints = new List<Vector2>();
            for (float x = startX; x <= endX; x += ceilSize) {
                verticalPoints.Add(new Vector2(x, startY));
                verticalPoints.Add(new Vector2(x, endY));
                verticalPoints.Add(GeometricalLineRenderer.VOID_POINT);
            }
            _lineRendererVertical.SetPositions(verticalPoints);
            
            List<Vector2> horizontalPoints = new List<Vector2>();
            for (float y = startY; y <= endY; y += ceilSize) {
                horizontalPoints.Add(new Vector2(startX, y));
                horizontalPoints.Add(new Vector2(endX, y));
                horizontalPoints.Add(GeometricalLineRenderer.VOID_POINT);
            }
            _lineRendererHorizontal.SetPositions(horizontalPoints);
            _startX = Utilities.SnapToGridMin(_cameraBounds.min.x, ceilSize);
            _startY = Utilities.SnapToGridMin(_cameraBounds.min.y, ceilSize);
        }

        private void OffsetGrid() {
            float newStartX = Utilities.SnapToGridMin(_cameraBounds.min.x, ceilSize);
            float newStartY = Utilities.SnapToGridMin(_cameraBounds.min.y , ceilSize);
            float deltaY = newStartY - _startY;
            float deltaX = newStartX - _startX;
            
            foreach (Transform child in _lineStorage.transform) {
                if (!Mathf.Approximately(deltaX, 0f)) child.localPosition += (Vector3) Vector2.right * deltaX;
                if (!Mathf.Approximately(deltaY, 0f)) child.localPosition += (Vector3) Vector2.up * deltaY;
            }
            _startX = newStartX;
            _startY = newStartY;
        }

        private GeometricalLineRenderer SetupLineRenderer(float lineWidth, Color color) {
            GeometricalLineRenderer lineRenderer = Instantiate(_lineRendererReference, _lineStorage.transform);
            lineRenderer.Setup(new LineRendererConfig(lineWidth, false, color, Parameters.GridSortingOrder, true, true));
            return lineRenderer;
        }

        public Vector2 GetNearestGridPoint(Vector2 position) {
            int ix = Mathf.RoundToInt(position.x / ceilSize);
            int iy = Mathf.RoundToInt(position.y / ceilSize);
            
            float gridX = ix * ceilSize;
            float gridY = iy * ceilSize;

            return new Vector2(gridX, gridY);
        }

        public void OnCameraChanged() {
            float niceSize = FieldCamera.Instance.CeilSize();
            var newBounds = FieldCamera.Instance.GetCameraBounds();

            ceilSize = niceSize;
            _cameraBounds = newBounds;

            if (!Mathf.Approximately(_zoom, FieldCamera.Instance.ZoomLevel)) {
                _zoom = FieldCamera.Instance.ZoomLevel;
                BuildGrid();
            }
            else {
                OffsetGrid();
                
            }
        }
    }
}