using System;
using UnityEngine;

namespace Geometry {
    [RequireComponent(typeof(Collider2D))]
    public class GeometryPointCollider : MonoBehaviour {
        [SerializeField] private CircleCollider2D _collider;
        [SerializeField] private float _initialRadius;
        public event System.Action OnDrag;
        public event System.Action OnDragEnd;
        private void OnMouseDrag() {
            OnDrag?.Invoke();
        }

        private void OnMouseUp() {
            OnDragEnd?.Invoke();    
        }
        private void Awake() {
            FieldCamera.OnCameraChanged += OnCameraChanged;
            _collider.radius = _initialRadius;
        }
        private void OnDisable() {
            FieldCamera.OnCameraChanged -= OnCameraChanged;
        }
        public void OnCameraChanged() {
            _collider.radius = _initialRadius / FieldCamera.Instance.ZoomLevel;
        }
    }
}