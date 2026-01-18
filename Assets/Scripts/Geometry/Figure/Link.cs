using UnityEditor.UI;
using UnityEngine;
using Geometry.Realisations;

namespace Geometry {
    public enum Coordinate {X, Y, Both}
    public abstract class Link {
        protected GeometryPoint _selfPoint;
        protected GeometryPoint _linkPoint; 

        public Link(GeometryPoint selfPoint, GeometryPoint linkPoint) {
            _selfPoint = selfPoint;
            _linkPoint = linkPoint;
        }
        public abstract void Move(Vector2 delta);
    }

    public class Mirror : Link {
        private Coordinate _coordinate;

        public Mirror(GeometryPoint self, GeometryPoint target, Coordinate coord) : base(self, target) {
            _coordinate = coord;
        }

        public override void Move(Vector2 delta) {
            Vector2 constrainedDelta = new Vector2(
                (_coordinate == Coordinate.X || _coordinate == Coordinate.Both) ? -delta.x : 0,
                (_coordinate == Coordinate.Y || _coordinate == Coordinate.Both) ? -delta.y : 0
            );

            Vector2 targetPos = _linkPoint.Position + constrainedDelta;

            if (_linkPoint.MatchRules(targetPos)) {
                _linkPoint.Position = targetPos;
            }
        }
    }

    public class Copy : Link {
        private Coordinate _coordinate;

        public Copy(GeometryPoint self, GeometryPoint target, Coordinate coord) : base(self, target) {
            _coordinate = coord;
        }

        public override void Move(Vector2 delta) {
            Vector2 constrainedDelta = new Vector2(
                (_coordinate == Coordinate.X || _coordinate == Coordinate.Both) ? delta.x : 0,
                (_coordinate == Coordinate.Y || _coordinate == Coordinate.Both) ? delta.y : 0
            );

            Vector2 targetPos = _linkPoint.Position + constrainedDelta;

            if (_linkPoint.MatchRules(targetPos)) {
                _linkPoint.Position = targetPos;
            }
        }
    }
}