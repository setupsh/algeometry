using System.Collections;
using System.Collections.Generic;
using Geometry;
using UnityEngine;
using Resources = Geometry.Resources;

public class SpawnGeometryPointAction : ReturnLessonAction<GeometryPoint> {
    [SerializeField] private Vector2 _position;
    [SerializeField] private string _label;
    public override GeometryPoint Value { get; protected set; }

    protected override void Enter() {
        GeometryPoint point = Instantiate(Resources.FreeGeometryPointPrefab, _position, Quaternion.identity, Board.Instance.transform);
        point.Blocked = true;
        point.Label = _label;
        Value = point;
        Complete();
    }

}
