using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace Geometry.Realisations {
    public class Triangle : Figure {
        public override Vector2 GetCenter() {
            return (_points[0].transform.position + _points[1].transform.position + _points[2].transform.position) / 3f;
        }

        protected override void InitRules() {
            RuleHelper.PairWithEachOther.NoEqualPosition(_points);
            RuleHelper.PairWithEachOther.NonCollinear(_points);
            _points[0].AddLink(new Copy(_points[0], _points[1], Coordinate.Both));
            PointLabel height = new GameObject().AddComponent<PointLabel>();
            height.Init(this, _points[0], "A", Color.white, Vector2.zero);
            AddConstruction(height);
        }
        
        protected override int PointsAmount() {
            return 3;
        }
    }
}