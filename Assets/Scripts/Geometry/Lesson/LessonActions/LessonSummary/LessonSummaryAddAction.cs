using System.Collections;
using System.Collections.Generic;
using Algebra;
using Geometry;
using UnityEngine;

public class LessonSummaryAddTextAction : LessonAction {
    [SerializeField] private string _summary;
    
    protected override void Enter() {
        Board.LessonSummary.AddSummary(Board.AlgebraExpressionViewGenerator.Generate(new VariableExpression(0, _summary), Board.LessonSummary.transform).Root);
    }
}
