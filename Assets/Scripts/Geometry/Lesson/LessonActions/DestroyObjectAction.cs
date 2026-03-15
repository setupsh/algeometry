using Geometry;
using UnityEngine;

public class DestroyFigureAction : LessonAction {
    [SerializeField] private ReturnLessonAction<Figure> _objectToDestroy;

    protected override void Enter() {
        Destroy(_objectToDestroy.Value.gameObject);
        Complete();
    }
}
