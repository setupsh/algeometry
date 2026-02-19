using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Geometry;
public class AddPointIndicatorAction : LessonAction {
    [SerializeField] private ReturnLessonAction<GeometryPoint> _point;
    [SerializeField] private GeometryPoint.Indicators _function;

    protected override void Enter() {
        Board.IndicatorsList.AddIndicator(_point.Value.GetIndicatorInfos()[(int)_function]);
        Complete();
    }
}
