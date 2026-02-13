using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Geometry;
public class AddAngleAction : ReturnLessonAction<Angle> {
    [SerializeField] private ReturnLessonAction<Figure> _figure;
    [SerializeField] private ReturnLessonAction<GeometryPoint> _armA;
    [SerializeField] private ReturnLessonAction<GeometryPoint> _vertex;
    [SerializeField] private ReturnLessonAction<GeometryPoint> _armB;
    [SerializeField] private Color _color;
    [SerializeField] private string _uniqueLabel;
    [SerializeField] private bool _showLabel;
    public override Angle Value { get; protected set; }

    protected override void Enter() {
        Angle angle = Board.Instance.Instantiate<Angle>();
        angle.Init(_figure.Value, _armA.Value, _vertex.Value, _armB.Value, _color, _showLabel, _uniqueLabel);
        Value = angle;
        Complete();
    }

}
