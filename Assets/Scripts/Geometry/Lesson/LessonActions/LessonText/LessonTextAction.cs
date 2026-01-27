using System.Collections;
using System.Collections.Generic;
using Geometry;
using UnityEngine;

public class LessonTextAction : LessonAction {
    [SerializeField] private List<LessonTextElementInstance> _lessonText;
    
    protected override void Enter() {
        Board.LessonText.Invoke(_lessonText);
        Board.LessonText.OnAllTextFinished += Exit;
    }

    private void Exit() {
        Complete();
        Board.LessonText.OnAllTextFinished -= Exit;
    }
}
