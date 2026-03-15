using Geometry;
using UnityEngine;
using PicoTween;
public class FocusOnFigureAction : LessonAction {
    [SerializeField] private ReturnLessonAction<Figure> _figureToFocus;


    protected override void Enter() {
        ((Vector2) FieldCamera.Instance.transform.position).Tween(
            getter: () => FieldCamera.Instance.transform.position,
            setter: value => FieldCamera.Instance.SetPosition(value),
            endValue: _figureToFocus.Value.GetCenter(),
            duration: 0.2f,
            onComplete: Complete
        );
    }
}
