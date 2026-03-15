using Geometry;
using UnityEngine;

public class SetObjectActive : LessonAction {
    [SerializeField] private bool _active;
    [SerializeField] private Transform _object;

    protected override void Enter() {
        _object.gameObject.SetActive(_active);
        Complete();
    }
}