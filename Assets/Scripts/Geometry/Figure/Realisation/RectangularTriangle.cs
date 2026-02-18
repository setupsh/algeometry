    using System;
    using UnityEngine;

namespace Geometry.Realisations {
    public class RectangularTriangle : Figure {
        public override Vector2 GetCenter() {
            return (Points[0].transform.position + Points[1].transform.position + Points[2].transform.position) / 3f;
        }
        protected override void InitRules() {
            Points[0].Blocked = true;
            Points[1].Blocked = true;
            Points[2].Links.Add(new OrthogonalSnap(Points[2], Points[1], Points[0]));
        }
        protected override void PostUpdate() {
        }

        protected override Vector2[] DefaultPositions() {
            return new Vector2[] {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(1, 1)
            };
        }
        protected override int PointsAmount() {
            return 3;
        }

    }
}