using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Geometry {
    [RequireComponent(typeof(Collider2D))]
    public class GeometryPointCollider : MonoBehaviour, IDragHandler, IEndDragHandler {
        [SerializeField] private CircleCollider2D _collider;
        [SerializeField] private float _initialRadius;
        public event System.Action OnDrag;
        public event System.Action OnDragEnd;
        private void Awake() {
            FieldCamera.OnCameraChanged += OnCameraChanged;
            _collider.radius = _initialRadius;
        }
        private void OnDisable() {
            FieldCamera.OnCameraChanged -= OnCameraChanged;
        }
        public void OnCameraChanged() {
            _collider.radius = _initialRadius * FieldCamera.Instance.CeilSize();
        }

        void IDragHandler.OnDrag(PointerEventData eventData) {
            OnDrag?.Invoke();
        }

        public void OnEndDrag(PointerEventData eventData) {
            OnDragEnd?.Invoke(); 
        }
    }
}