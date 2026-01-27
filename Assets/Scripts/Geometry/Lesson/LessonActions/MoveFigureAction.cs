using Geometry;
using UnityEngine;

public class MoveFigureAction : LessonAction {
    [SerializeField] private ReturnLessonAction<Figure> _figureToMove;

    protected override void Enter() {
        _figureToMove.Value.Points[0].Move(Vector2.one * 5f);
        Complete();
    }
}
