using Geometry;
using UnityEngine;

public class DestroyPointAction : LessonAction {
    [SerializeField] private ReturnLessonAction<GeometryPoint> _pointToDestroy;

    protected override void Enter() {
        Destroy(_pointToDestroy.Value.gameObject);
        Complete();
    }
}
