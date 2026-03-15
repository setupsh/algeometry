using System;
using UnityEngine;

[RequireComponent(typeof(LessonSequence))]
public class GroupedActions : LessonAction {
    [SerializeField] private LessonSequence _sequence;
    
    protected override void Enter() {
        if (_sequence.AutoStart) {
            throw new SystemException("Nested sequence with autostart");
        }
        _sequence.Launch();
        _sequence.OnEnd += (Complete);
    }

    private void Exit() {
    }
}

