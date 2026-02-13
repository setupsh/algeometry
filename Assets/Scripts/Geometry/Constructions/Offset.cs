using System;
using UnityEngine;
namespace Geometry {
    public abstract class OffsetConfig {
        public abstract Vector2 CalculateOffset();
    }

    public class CentroidOffset : OffsetConfig {
        private Func<Vector2> center;
        private Func<Vector2> position;
        private float distance;
        public CentroidOffset(Func<Vector2> center, Func<Vector2> position, float distance) {
            this.center = center;
            this.position = position;
            this.distance = distance;
        }

        public CentroidOffset(Figure figure, GeometryPoint point, float distance) {
            center = figure.GetCenter;
            position = () => point.Position;
            this.distance = distance;
        }

        public override Vector2 CalculateOffset() {
            return position() + (position() - center()).normalized * (distance * FieldCamera.Instance.CeilSize());
        }
    }

    public class StaticOffset : OffsetConfig {
        private Func<Vector2> position;
        private Vector2 offset;

        public StaticOffset(Func<Vector2> position, Vector2 offset) {
            this.position = position;
            this.offset = offset;
        }

        public override Vector2 CalculateOffset() {
            return position() + (offset * FieldCamera.Instance.CeilSize());
        }
    }
}