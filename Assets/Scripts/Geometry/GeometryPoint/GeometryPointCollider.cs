using System;
using UnityEngine;

namespace Geometry {
    [RequireComponent(typeof(Collider2D))]
    public class GeometryPointCollider : MonoBehaviour {
        [SerializeField] private Collider2D _collider;
        public event System.Action OnDrag;
        public event System.Action OnDragEnd;
        private void OnMouseDrag() {
            OnDrag?.Invoke();
        }

        private void OnMouseUp() {
            OnDragEnd?.Invoke();    
        }
    }
}