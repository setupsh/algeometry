using Geometry.Realisations;
using UnityEngine;
namespace Geometry {
    public abstract class Constrain {
        public abstract Vector2 GetConstrainedPosition(Vector2 position);
    }

    public class OnCircleConstrain : Constrain {
        private Circle circle;

        public OnCircleConstrain(Circle circle) {
            this.circle = circle;
        }
        public override Vector2 GetConstrainedPosition(Vector2 position) {
            return circle.GetProjection(position);
        }
    }
    
    
}