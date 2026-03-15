using System.Collections;
using System.Collections.Generic;
using Algebra;
using Geometry;
using UnityEngine;

public class LessonSummaryClearAction : LessonAction {
    protected override void Enter() {
        Board.LessonSummary.Clear();
        Complete();
    }
}
