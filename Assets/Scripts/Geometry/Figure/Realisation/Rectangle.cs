using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace Geometry.Realisations {
    public class Rectangle : Figure {
        public override Vector2 GetCenter() {
            return (Points[0].transform.position + Points[1].transform.position + Points[2].transform.position + Points[3].transform.position) / 4f;
        }

        protected override void InitRules() {
            RuleHelper.PairWithEachOther.NoEqualPosition(Points);
            RuleHelper.PairWithEachOther.NonCollinear(Points);
            Points[1].Blocked = true;
            Points[3].Blocked = true;
            Points[2].Links.Add(new Copy(Points[2], Points[3], Coordinate.Y));
            Points[2].Links.Add(new Copy(Points[2], Points[1], Coordinate.X));
            Points[0].Links.Add(new Copy(Points[0], Points[1], Coordinate.Both));
            Points[0].Links.Add(new Copy(Points[0], Points[2], Coordinate.Both));
            Points[0].Links.Add(new Copy(Points[0], Points[3], Coordinate.Both));
        }

        protected override Vector2[] DefaultPositions() {
            return new Vector2[] {
                new Vector2(0, 0),
                new Vector2(2, 0),
                new Vector2(2, 1),
                new Vector2(0, 1),
            };
        }

        protected override int PointsAmount() {
            return 4;
        }

        protected override void PostUpdate() {
            
        }
    }
}