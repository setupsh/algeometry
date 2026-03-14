using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace Geometry.Realisations {
    public class Triangle : Figure {
        public override Vector2 GetCenter() {
            float area = 0f;
            Vector2 center = Vector2.zero;
            int n = PointsAmount();
    
            for (int i = 0; i < n; i++) {
                Vector2 a = Points[i].Position;
                Vector2 b = Points[(i + 1) % n].Position;
                float cross = a.x * b.y - b.x * a.y;
                area += cross;
                center += (a + b) * cross;
            }
    
            area /= 2f;
            return center / (6f * area);
            return (Points[0].transform.position + Points[1].transform.position + Points[2].transform.position) / 3f;
        }

        protected override void InitRules() {
            RuleHelper.PairWithEachOther.NoEqualPosition(Points);
            RuleHelper.PairWithEachOther.NonCollinear(Points);
        }

        protected override Vector2[] DefaultPositions() {
            return new Vector2[] {
                new Vector2(0, 1),
                new Vector2(0, 0),
                new Vector2(1, 0),
            };
        }

        protected override int PointsAmount() {
            return 3;
        }

        protected override void PostUpdate() {
            
        }
    }
}