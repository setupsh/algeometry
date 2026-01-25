using Geometry;
using UnityEngine;

public class SpawnFigureAction : LessonAction {
    [SerializeField] private Figures _figureType;
    [SerializeField] private Vector2 _center;
    public Figure Instance { get; private set; }
    protected override void Enter() {
        Instance = Board.FigureSpawner.SpawnFigure(_figureType, _center);
    }
}
