using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Geometry;
public class AddCenterLabelAction : ReturnLessonAction<FigureCenterLabel> {
    [SerializeField] private ReturnLessonAction<Figure> _figure;
    [SerializeField] private string _caption;
    [SerializeField] private Color _color;
    public override FigureCenterLabel Value { get; protected set; }

    protected override void Enter() {
        FigureCenterLabel centerCaption = Board.Instance.Instantiate<FigureCenterLabel>();
        centerCaption.Init(_figure.Value, _caption, _color);
        _figure.Value.AddConstruction(centerCaption);
        Value = centerCaption;
        Complete();
    }

}