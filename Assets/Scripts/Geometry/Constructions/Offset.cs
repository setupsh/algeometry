using System;
using UnityEngine;
namespace Geometry {
    public abstract class OffsetConfig {
        public abstract Vector2 CalculateOffset();
    }

    public class CentroidOffset : OffsetConfig {
        private Func<Vector2> _center;
        private Func<Vector2> _position;
        private float _multiplier;
        public CentroidOffset(Func<Vector2> center, Func<Vector2> position, float multiplier) {
            _center = center;
            _position = position;
            _multiplier = multiplier;
        }

        public override Vector2 CalculateOffset() {
            Vector2 center = _center();
            Vector2 position = _position();
            return position + (position - center).normalized * (_multiplier * FieldCamera.Instance.CeilSize()) / 5f;
        }
    }
}