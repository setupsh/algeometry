using UnityEngine;
using System.Collections.Generic;
using Geometry;
using PicoTween;
using Resources = Geometry.Resources;

public class ColorizePolygonAction : LessonAction {
    [SerializeField] private List<Vector2> _points;
    [SerializeField] private Color _toColor;
    [SerializeField] private float _duration;


    protected override void Enter() {
        PolygonBuilder polygon = Instantiate(Resources.PolygonPrefab, Board.Instance.transform);
        Material material = polygon.GetComponent<MeshRenderer>().material;
        material.color = Color.clear;
        polygon.BuildPolygon(_points);
        material.Tween(_toColor, _duration, onComplete: () => {
            material.Tween(Color.clear, _duration, onComplete: () => {
                Destroy(polygon.gameObject);
                Complete();
            });
        });
        
    }
}