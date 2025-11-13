using UnityEngine;
namespace Geometry {
    public class StaticGeometryPoint : GeometryPoint {
        protected override void OnDrag() {
        }
        protected override void OnDragEnd() {
        }

        protected override Vector2 GetPosition() {
            return transform.position;
        }

        public void SetPosition(Vector2 position) {
            transform.position = position;
        }

        public override void Move(Vector2 position) {
            
        }
        
    }
}