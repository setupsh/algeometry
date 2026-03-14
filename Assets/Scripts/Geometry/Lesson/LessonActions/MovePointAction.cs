using Geometry;
using UnityEngine;

public class MovePointAction : LessonAction {
    [SerializeField] private ReturnLessonAction<GeometryPoint> _pointToMove;
    [SerializeField] private Vector2 _by;

    protected override void Enter() {
        _pointToMove.Value.Move(_pointToMove.Value.Position + _by);
        Complete();
    }
}
