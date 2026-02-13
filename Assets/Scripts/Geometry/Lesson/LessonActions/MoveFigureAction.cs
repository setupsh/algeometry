using Geometry;
using UnityEngine;

public class MoveFigureAction : LessonAction {
    [SerializeField] private ReturnLessonAction<Figure> _figureToMove;
    [SerializeField] private Vector2 _by;

    protected override void Enter() {
        foreach (GeometryPoint point in _figureToMove.Value.Points) {
            point.Position += _by;
        }
        Complete();
    }
}
