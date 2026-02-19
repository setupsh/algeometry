using Geometry;
using Geometry.Realisations;
using UnityEngine;
public class LinkCircleToPoint : LessonAction {
    [SerializeField] private ReturnLessonAction<Figure> _circle;
    [SerializeField] private ReturnLessonAction<GeometryPoint> _pointToLink;
    protected override void Enter() {
        ((Circle)_circle.Value).LinkPoint(_pointToLink.Value);
        Complete();
    }
}
