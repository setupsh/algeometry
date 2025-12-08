using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using PrimeTween;

namespace Geometry {
    public abstract class Rule {
        public int Priority { get; protected set; } = 1;
        public abstract bool Match(Vector2 position);
    }

    public class PositionNotEqual : Rule {
        private GeometryPoint[] _points;
        public PositionNotEqual(GeometryPoint[] points) {
            _points = points;
        }

        public override bool Match(Vector2 position) {
            foreach (var point in _points) {
                if (point.Position.Equals(position)) {
                    return false;
                }
            }
            return true;
        }
    }

    public class NonCollinear : Rule {
        private GeometryPoint[] _points;

        public NonCollinear(GeometryPoint[] points) {
            _points = points;
        }

        public override bool Match(Vector2 position) {
            const float epsilon = 0.001f;
            for (int i = 0; i < _points.Length; i++) {
                for (int j = i + 1; j < _points.Length; j++) {
                    Vector2 a = _points[i].Position;
                    Vector2 b = _points[j].Position;
                    Vector2 c = position;

                    float triangleArea = MathF.Abs((a.x - b.x) * (c.y - b.y) - (a.y - b.y) * (c.x - b.x));
                    Debug.Log(triangleArea);
                    if (triangleArea < epsilon) {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}