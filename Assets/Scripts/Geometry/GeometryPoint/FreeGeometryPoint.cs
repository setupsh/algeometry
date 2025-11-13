using Geometry;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class FreeGeometryPoint : GeometryPoint {
    protected override void OnDrag() {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x , Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        if (MatchRules(mousePosition)) {
            transform.position = mousePosition;
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
