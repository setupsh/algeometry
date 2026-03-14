using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Geometry;

namespace Geometry {
    public class Grid : MonoBehaviour {
        [SerializeField] private GeometricalLineRenderer _lineRendererReference;
        [SerializeField] private GameObject _lineStorage;
        [SerializeField] private float _lineWidth;
        [SerializeField] private Color _lineColor;
        private GeometricalLineRenderer lineRendererVertical,  lineRendererHorizontal;
        private Bounds cameraBounds;
        private float ceilSize = 1f;
        private float startX, endX,  startY, endY;
        private int overflowLines = 3;
        private float zoom;
        private void Start() {
            ceilSize = FieldCamera.Instance.CeilSize();
            cameraBounds = FieldCamera.Instance.GetCameraBounds();
            lineRendererVertical = SetupLineRenderer(_lineWidth, _lineColor);
            lineRendererHorizontal = SetupLineRenderer(_lineWidth, _lineColor);
            startX = Utilities.SnapToGrid(cameraBounds.min.x, ceilSize);
            startY = Utilities.SnapToGrid(cameraBounds.min.y, ceilSize);
            zoom = FieldCamera.Instance.ZoomLevel;
            BuildGrid();
        }

        private void OnEnable() {
            FieldCamera.OnCameraChanged += OnCameraChanged;
        }

        private void OnDisable() {
            FieldCamera.OnCameraChanged -= OnCameraChanged;
        }

        private void BuildGrid() {
            float startX = Utilities.SnapToGridMin(cameraBounds.min.x, ceilSize) - ceilSize * overflowLines;
            float endX = Utilities.SnapToGridMax(cameraBounds.max.x, ceilSize) + ceilSize * overflowLines;
            float startY = Utilities.SnapToGridMin(cameraBounds.min.y , ceilSize) - ceilSize * overflowLines;
            float endY = Utilities.SnapToGridMax(cameraBounds.max.y , ceilSize) + ceilSize * overflowLines;
            
            lineRendererHorizontal.transform.localPosition = Vector2.zero;
            lineRendererVertical.transform.localPosition = Vector2.zero;

            List<Vector2> verticalPoints = new List<Vector2>();
            for (float x = startX; x <= endX; x += ceilSize) {
                verticalPoints.Add(new Vector2(x, startY));
                verticalPoints.Add(new Vector2(x, endY));
                verticalPoints.Add(GeometricalLineRenderer.VOID_POINT);
            }
            lineRendererVertical.SetPositions(verticalPoints);
            
            List<Vector2> horizontalPoints = new List<Vector2>();
            for (float y = startY; y <= endY; y += ceilSize) {
                horizontalPoints.Add(new Vector2(startX, y));
                horizontalPoints.Add(new Vector2(endX, y));
                horizontalPoints.Add(GeometricalLineRenderer.VOID_POINT);
            }
            lineRendererHorizontal.SetPositions(horizontalPoints);
            this.startX = Utilities.SnapToGridMin(cameraBounds.min.x, ceilSize);
            this.startY = Utilities.SnapToGridMin(cameraBounds.min.y, ceilSize);
        }

        private void OffsetGrid() {
            float newStartX = Utilities.SnapToGridMin(cameraBounds.min.x, ceilSize);
            float newStartY = Utilities.SnapToGridMin(cameraBounds.min.y , ceilSize);
            float deltaY = newStartY - startY;
            float deltaX = newStartX - startX;
            
            foreach (Transform child in _lineStorage.transform) {
                if (!Mathf.Approximately(deltaX, 0f)) child.localPosition += (Vector3) Vector2.right * deltaX;
                if (!Mathf.Approximately(deltaY, 0f)) child.localPosition += (Vector3) Vector2.up * deltaY;
            }
            startX = newStartX;
            startY = newStartY;
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
            cameraBounds = newBounds;

            if (!Mathf.Approximately(zoom, FieldCamera.Instance.ZoomLevel)) {
                zoom = FieldCamera.Instance.ZoomLevel;
                BuildGrid();
            }
            else {
                OffsetGrid();
                
            }
        }
    }
}