using Geometry;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class FreeGeometryPoint : GeometryPoint {
    protected override void OnDrag() {
        Vector2 mousePosition = FieldCamera.Instance.ToWorldPoint(InputListener.MousePosition);
        if (InputListener.ShiftPressed ) {
            Vector2 nearestGridPoint = Board.Grid.GetNearestGridPoint(mousePosition);
            if (MatchRules(nearestGridPoint)) {
                Vector2 delta = nearestGridPoint - Position;
                MoveLinked(delta);
                transform.position = nearestGridPoint;
            }
        }
        else {
            if (MatchRules(mousePosition)) {
                transform.position = mousePosition;
            }
        }
        _collider.transform.position = mousePosition;
        
        InvokePositionChanging();
        
    }
    protected override void OnDragEnd() {
        _collider.transform.position = transform.position;
    }

    protected override Vector2 GetPosition() {
        return transform.position;
    }

    public override void Move(Vector2 position) {
        transform.position = position;
    }
}
