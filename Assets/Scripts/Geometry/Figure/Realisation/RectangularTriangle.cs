    using UnityEngine;

namespace Geometry.Realisations {
    public class RectangularTriangle : Triangle {
        protected override void InitRules() {
            //RuleHelper.PairWithEachOther.NoEqualPosition(Points);
            //RuleHelper.PairWithEachOther.NonCollinear(Points);
            Points[0].Blocked = true;
            Points[1].Blocked = true;
            Points[2].AddLink(new Copy(Points[2], Points[1], Coordinate.X));
        }

        protected override Vector2[] DefaultPositions() {
            return new Vector2[] {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(1, 1)
            };
        }
    }
}