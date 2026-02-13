using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Geometry;
public class AddAngleIndicatorAction : LessonAction {
    [SerializeField] private ReturnLessonAction<Angle> _angle;
    [SerializeField] private Angle.Indicators _function;

    protected override void Enter() {
        Board.IndicatorsList.AddIndicator(_angle.Value.GetIndicatorInfos()[(int)_function]);
        Complete();
    }
}
