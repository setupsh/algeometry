using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitAction : LessonAction {
    [SerializeField] private float _waitSeconds;
    protected override void Enter() {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait() {
        yield return new WaitForSeconds(_waitSeconds);
        Complete();
    }
}
