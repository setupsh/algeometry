using UnityEngine;
using System.Collections.Generic;
namespace Geometry {
    public static class GeometryUtil {
        public static Vector2 ProjectPointOnCircle(
            Vector2 point,
            Vector2 center,
            float radius
        ) {
            Vector2 dir = point - center;
            if (dir.sqrMagnitude < 1e-8f)
                return center + Vector2.right * radius;

            return center + dir.normalized * radius;
        }
    }
    public sealed class SolverPoint {
        public Vector2 Position;
        public bool Locked;

        public SolverPoint(Vector2 position) {
            Position = position;
        }
    }

    public interface IGeometricConstraint {
        /// <returns>true, если constraint что-то изменил</returns>
        bool Project();
    }

    public sealed class GeometricSolver {
        public readonly List<SolverPoint> Points = new();
        public readonly List<IGeometricConstraint> Constraints = new();

        public int MaxIterations { get; set; } = 10;
        public float Epsilon { get; set; } = 1e-5f;

        public void Solve() {
            for (int iteration = 0; iteration < MaxIterations; iteration++) {
                bool anyChange = false;

                foreach (var constraint in Constraints) {
                    if (constraint.Project()) {
                        anyChange = true;
                    }
                }

                if (!anyChange)
                    break; // система сошлась
            }
        }
    }
    public sealed class FixedDistanceConstraint : IGeometricConstraint {
        private readonly SolverPoint _a;
        private readonly SolverPoint _b;
        private readonly float _distance;
        private readonly float _epsilon;

        public FixedDistanceConstraint(
            SolverPoint a,
            SolverPoint b,
            float distance,
            float epsilon = 1e-5f
        ) {
            _a = a;
            _b = b;
            _distance = distance;
            _epsilon = epsilon;
        }

        public bool Project() {
            Vector2 delta = _b.Position - _a.Position;
            float current = delta.magnitude;

            if (Mathf.Abs(current - _distance) < _epsilon)
                return false;

            if (current < 1e-8f)
                return false;

            Vector2 dir = delta / current;

            if (!_a.Locked && !_b.Locked) {
                Vector2 center = (_a.Position + _b.Position) * 0.5f;
                _a.Position = center - dir * (_distance * 0.5f);
                _b.Position = center + dir * (_distance * 0.5f);
            }
            else if (!_a.Locked) {
                _a.Position = _b.Position - dir * _distance;
            }
            else if (!_b.Locked) {
                _b.Position = _a.Position + dir * _distance;
            }

            return true;
        }
    }
    public sealed class IsoscelesTriangleConstraint : IGeometricConstraint {
        private readonly SolverPoint _a;
        private readonly SolverPoint _b;
        private readonly SolverPoint _c;
        private readonly float _epsilon;

        public IsoscelesTriangleConstraint(
            SolverPoint a,
            SolverPoint b,
            SolverPoint c,
            float epsilon = 1e-5f
        ) {
            _a = a;
            _b = b;
            _c = c;
            _epsilon = epsilon;
        }

        public bool Project() {
            float dAB = Vector2.Distance(_a.Position, _b.Position);
            float dAC = Vector2.Distance(_a.Position, _c.Position);

            if (Mathf.Abs(dAB - dAC) < _epsilon)
                return false;

            float target = (dAB + dAC) * 0.5f;

            bool changed = false;

            if (!_b.Locked) {
                _b.Position = GeometryUtil.ProjectPointOnCircle(
                    _b.Position,
                    _a.Position,
                    target
                );
                changed = true;
            }

            if (!_c.Locked) {
                _c.Position = GeometryUtil.ProjectPointOnCircle(
                    _c.Position,
                    _a.Position,
                    target
                );
                changed = true;
            }

            return changed;
        }
    }


}