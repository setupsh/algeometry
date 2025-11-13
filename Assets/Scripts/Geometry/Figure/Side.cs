using UnityEngine;

namespace Geometry {
    public class Side : MonoBehaviour {
        public GeometryPoint Start {get; private set;}
        public GeometryPoint End {get; private set;}
        public Figure Parent {get; private set;}

        private Label _label;
        
        public void Init(Figure parent, GeometryPoint start, GeometryPoint end) {
            Parent = parent;
            Start = start;
            End = end;
            transform.name = $"{parent.name} side";
        }

        public void AssignLabel(string label) {
            _label = GeometricalLabelSystem.Instance.CreateLabel(Parent.name);
            _label.SetText(label);
            UpdateLabel();
        }

        public void UpdatePosition() {
            transform.position = GetMiddle();
        }

        public void UpdateLabel() {
            if (!_label) return; 
            Vector2 side = Start.transform.position - End.transform.position;
            Vector2 perpendicular = new Vector2(-side.y, side.x).normalized;
            Vector2 toCenter = (Parent.GetCenter() - GetMiddle()).normalized;
            if (Vector2.Dot(perpendicular, toCenter) > 0f) {
                perpendicular = -perpendicular;
            }
            _label.SetPosition(GetMiddle() + perpendicular * (_label.GetFontSize() / 100f));
        }
        
        
        public Vector2 GetMiddle() => (Start.transform.position + End.transform.position) * 0.5f;
    }
}