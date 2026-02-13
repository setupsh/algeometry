using Geometry;
using UnityEngine;

public class GetPointFromFigureAction : ReturnLessonAction<GeometryPoint> {
    [SerializeField] private ReturnLessonAction<Figure> _figure;
    [SerializeField] private int _pointIndex;
    public override GeometryPoint Value { get; protected set; }

    protected override void Enter() {
        Value = _figure.Value.Points[_pointIndex];
        Complete();
    }
}
