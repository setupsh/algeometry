using UnityEditor.UI;
using UnityEngine;

namespace Geometry {
    public enum Coordinate {X, Y, Both}
    public abstract class Link {
        protected GeometryPoint _selfPoint, _linkPoint;

        public Link(GeometryPoint selfPoint, GeometryPoint linkPoint) {
            _selfPoint = selfPoint;
            _linkPoint = linkPoint;
        }
        public abstract void Move(Vector2 delta);
    }

    public class Mirror : Link {
        private Coordinate _coordinate;

        public Mirror(GeometryPoint selfPoint, GeometryPoint linkPoint, Coordinate coordinate) : base(selfPoint, linkPoint) {
            _coordinate = coordinate;
        }

        public override void Move(Vector2 delta) {
            if (_coordinate == Coordinate.Both) {
                if (_linkPoint.MatchRules(_linkPoint.Position - delta)) {
                    _linkPoint.Move(_linkPoint.Position - delta);
                } 
            } else if (_coordinate == Coordinate.X) {
                if (_linkPoint.MatchRules(new Vector2(_linkPoint.Position.x - delta.x, _linkPoint.Position.y))) {
                    _linkPoint.Move(new Vector2(_linkPoint.Position.x - delta.x, _linkPoint.Position.y));
                }
            } else if  (_coordinate == Coordinate.Y) {
                if  (_linkPoint.MatchRules(new  Vector2(_linkPoint.Position.x, _linkPoint.Position.y - delta.y))) {
                    _linkPoint.Move(new Vector2(_linkPoint.Position.x, _linkPoint.Position.y - delta.y));
                }
            }
        }
    }

    public class Copy : Link {
        private Coordinate _coordinate;

        public Copy(GeometryPoint selfPoint, GeometryPoint linkPoint, Coordinate coordinate) : base(selfPoint, linkPoint) {
            _coordinate = coordinate;
        }

        public override void Move(Vector2 delta) {
            if (_coordinate == Coordinate.Both) {
                if (_linkPoint.MatchRules(_linkPoint.Position + delta)) {
                    _linkPoint.Move(_linkPoint.Position + delta);
                } 
            } else if (_coordinate == Coordinate.X) {
                if (_linkPoint.MatchRules(new Vector2(_linkPoint.Position.x + delta.x, _linkPoint.Position.y))) {
                    _linkPoint.Move(new Vector2(_linkPoint.Position.x + delta.x, _linkPoint.Position.y));
                }
            } else if  (_coordinate == Coordinate.Y) {
                if  (_linkPoint.MatchRules(new  Vector2(_linkPoint.Position.x, _linkPoint.Position.y + delta.y))) {
                    _linkPoint.Move(new Vector2(_linkPoint.Position.x, _linkPoint.Position.y + delta.y));
                }
            }
        }
    }
}