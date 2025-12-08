using System;
using UnityEngine;
using Geometry;
using PrimeTween;
namespace Geometry {
    public class GridGeometryPoint : GeometryPoint {
        private Grid _grid;
        private Vector2 _inGridPosition;
        
        private void Start() {
            _grid = Board.Instance.Grid;
            _inGridPosition = _grid.GetNearestGridPoint(transform.position);
            transform.position = _inGridPosition;
            InvokePositionChanging();
        }
        protected override void OnDrag() {
            Vector2 mousePosition = FieldCamera.Instance.ToWorldPoint(InputListener.MousePosition);
            Vector2 nearestGridPoint = _grid.GetNearestGridPoint(mousePosition);
            if (nearestGridPoint != _inGridPosition && MatchRules(nearestGridPoint)) {
                Vector2 delta = nearestGridPoint - _inGridPosition;
                _inGridPosition = nearestGridPoint;
                MoveLinked(delta);
                Tween.Position(transform, nearestGridPoint, Parameters.Instance.GridPointTween);
            }
            _collider.transform.position = mousePosition;

            InvokePositionChanging();
        }
        protected override void OnDragEnd() {
            _collider.transform.position = transform.position;
        }

        public override void Move(Vector2 position) {
            Vector2 nearestGridPoint = _grid.GetNearestGridPoint(position);
            _inGridPosition = nearestGridPoint;
            Tween.Position(transform, nearestGridPoint, Parameters.Instance.GridPointTween);
        }

        protected override Vector2 GetPosition() {
            return _inGridPosition;
        }
    }

}