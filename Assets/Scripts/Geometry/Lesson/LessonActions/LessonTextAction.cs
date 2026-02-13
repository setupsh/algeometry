using System.Collections;
using System.Collections.Generic;
using Geometry;
using UI;
using UnityEngine;

public class AddIndicatorAction : LessonAction {
    [SerializeField] private ReturnLessonAction<Figure> _figure;
    
    protected override void Enter() {
        Board.IndicatorsList.AddIndicator(new AlgebraExpressionInfo(_figure.Value.Points[0].GetIndicatorInfos()[0].GetExpression(), "", "POINT" ));
        Complete();
    }

    private void Exit() {
    }
}
