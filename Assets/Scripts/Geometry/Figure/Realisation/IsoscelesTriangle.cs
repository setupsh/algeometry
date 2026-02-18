    using UnityEngine;

namespace Geometry.Realisations {
    public class IsoscelesTriangle : Triangle {
        protected override void InitRules() {
            RuleHelper.PairWithEachOther.NoEqualPosition(Points);
            RuleHelper.PairWithEachOther.NonCollinear(Points);
            Points[0].Links.Add(new Copy(Points[0], Points[1], Coordinate.Both));
            Points[0].Links.Add(new Copy(Points[0], Points[2], Coordinate.Both));
            Points[1].Links.Add(new Mirror(Points[1], Points[2], Coordinate.X));
            Points[1].Links.Add(new Copy(Points[2], Points[2], Coordinate.Y));
            Points[2].Blocked = true;
        }

        protected override Vector2[] DefaultPositions() {
            return new Vector2[] {
                new Vector2(0, 2),
                new Vector2(-1, 0),
                new Vector2(1, 0)
            };
        }
    }
}