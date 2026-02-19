using System.Collections;
using System.Collections.Generic;
using Geometry;
using UnityEngine;

public class BlockAllPointsAction : LessonAction {
    [SerializeField] private ReturnLessonAction<Figure> _figure;
    protected override void Enter() {
        foreach (var point in _figure.Value.Points) {
            point.Blocked = true;
        }
        Complete();
    }
    
}
