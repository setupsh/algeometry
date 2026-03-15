using System.Collections;
using System.Collections.Generic;
using Algebra;
using Geometry;
using UnityEngine;

public class LessonSummaryAddTextAction : LessonAction {
    [SerializeField] private List<LessonSummaryInstance> _summary;
    
    protected override void Enter() {
        foreach (LessonSummaryInstance instance in _summary) {
            if (instance.PlainText) {
                Board.LessonSummary.AddSummary(Board.AlgebraExpressionViewGenerator.Generate(new VariableExpression(0.0, instance.Content), Board.LessonSummary.transform).Root);
            }
            else {
                Board.LessonSummary.AddSummary(Board.AlgebraExpressionViewGenerator.Generate(new AlgebraExpressionParser().Parse(Lexer.Tokenize(instance.Content)), Board.LessonSummary.transform).Root);
            }
        }
    }
}
