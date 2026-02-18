using UnityEngine;
using Geometry.Realisations;

namespace Geometry {
    public enum Coordinate {X, Y, Both}
    public abstract class Link {
        protected GeometryPoint selfPoint;
        protected GeometryPoint linkPoint; 

        public Link(GeometryPoint selfPoint, GeometryPoint linkPoint) {
            this.selfPoint = selfPoint;
            this.linkPoint = linkPoint;
        }
        public abstract void Move(Vector2 delta);
    }

    public class Mirror : Link {
        private Coordinate coordinate;

        public Mirror(GeometryPoint self, GeometryPoint target, Coordinate coord) : base(self, target) {
            coordinate = coord;
        }

        public override void Move(Vector2 delta) {
            Vector2 constrainedDelta = new Vector2(
                (coordinate == Coordinate.X || coordinate == Coordinate.Both) ? -delta.x : 0,
                (coordinate == Coordinate.Y || coordinate == Coordinate.Both) ? -delta.y : 0
            );

            Vector2 targetPos = linkPoint.Position + constrainedDelta;

            if (linkPoint.MatchRules(targetPos)) {
                linkPoint.Position = targetPos;
            }
        }
    }

    public class Copy : Link {
        private Coordinate coordinate;

        public Copy(GeometryPoint self, GeometryPoint target, Coordinate coord) : base(self, target) {
            coordinate = coord;
        }

        public override void Move(Vector2 delta) {
            Vector2 constrainedDelta = new Vector2(
                (coordinate == Coordinate.X || coordinate == Coordinate.Both) ? delta.x : 0,
                (coordinate == Coordinate.Y || coordinate == Coordinate.Both) ? delta.y : 0
            );

            Vector2 targetPos = linkPoint.Position + constrainedDelta;

            if (linkPoint.MatchRules(targetPos)) {
                linkPoint.Position = targetPos;
            }
        }
    }
    public class OrthogonalSnap : Link {
        private GeometryPoint pivotPoint;

        public OrthogonalSnap(GeometryPoint self, GeometryPoint target, GeometryPoint pivot) 
            : base(self, target) {
            pivotPoint = pivot;
        }

        public override void Move(Vector2 delta) {
            Vector2 currentPosition = selfPoint.Position + delta;
            Vector2 pointPosition = pivotPoint.Position;

            Vector2 targetPosition;
            bool isYAxisQuarter = (currentPosition.x < pointPosition.x ^ currentPosition.y < pointPosition.y);

            if (isYAxisQuarter) {
                targetPosition = new Vector2(pointPosition.x, currentPosition.y);
            } else {
                targetPosition = new Vector2(currentPosition.x, pointPosition.y);
            }

            if (linkPoint.MatchRules(targetPosition)) {
                linkPoint.Move(targetPosition); 
            }
        }
    }
}