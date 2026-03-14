using Geometry;
using PicoTween;
using UnityEngine;

public class ColorizeSideAction : LessonAction {
    [SerializeField] private ReturnLessonAction<Figure> _figure;
    [SerializeField] private int _sideIndex;
    [SerializeField] private Color _targetColor;
    [SerializeField] private float _duration;
    
    protected override void Enter() {
        0f.Tween(() => 0f,
            t => _figure.Value.lineRenderer.SetSegmentColor(_sideIndex, Color.Lerp(_figure.Value.lineRenderer.CurrentConfig.LineColor, _targetColor, t)), 1f, _duration,
            onComplete: () => 0f.Tween(() => 0f, t => _figure.Value.lineRenderer.SetSegmentColor(_sideIndex, Color.Lerp(_targetColor, _figure.Value.lineRenderer.CurrentConfig.LineColor, t)), 1f, _duration, onComplete: Complete)
        );    
    }
}
