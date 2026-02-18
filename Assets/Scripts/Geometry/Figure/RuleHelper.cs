using System.Linq;

namespace Geometry {
    public static class RuleHelper {
        public static class PairWithEachOther {
            public static void NoEqualPosition(GeometryPoint[] points) {
                foreach (var point in points) {
                    point.Rules.Add(new PositionNotEqual(points.Where(pnt => pnt != point).ToArray()));
                }
            }

            public static void NonCollinear(GeometryPoint[] points) {
                foreach (var point in points) {
                    point.Rules.Add(new NonCollinear(points.Where(pnt => pnt != point).ToArray()));
                }
            }
        }
    }
}