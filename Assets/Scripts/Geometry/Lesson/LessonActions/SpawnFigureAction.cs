using Geometry;
using UnityEngine;

public class SpawnFigureAction : ReturnLessonAction<Figure> {
    [SerializeField] private Figures _figureType;
    [SerializeField] private Vector2 _center;
    public override Figure Value { get; protected set; }

    protected override void Enter() {
        Value = Board.FigureSpawner.SpawnFigure(_figureType, _center);
        Complete();
    }
}
