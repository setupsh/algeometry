using System;
using UI;
using UnityEngine;
using System.Collections.Generic;
using Algebra;

namespace Geometry {
    public class Side : MonoBehaviour, IIndicable {
        public GeometryPoint Start {get; private set;}
        public GeometryPoint End {get; private set;}
        public Figure Parent {get; private set;}

        private Label label;
        
        public void Init(Figure parent, GeometryPoint start, GeometryPoint end) {
            Parent = parent;
            Start = start;
            End = end;
            transform.name = $"{parent.name} side";
        }

        public void AssignLabel(string label) {
            this.label = GeometricalLabelSystem.Instance.CreateLabel(transform, Parameters.LabelSortingOrder);
            this.label.TextMeshPro.SetText(label);
            UpdateLabel();
        }

        public void UpdatePosition() {
            transform.position = GetMiddle();
        }

        public void UpdateLabel() {
            if (!label) return; 
            Vector2 side = Start.transform.position - End.transform.position;
            Vector2 perpendicular = new Vector2(-side.y, side.x).normalized;
            Vector2 toCenter = (Parent.GetCenter() - GetMiddle()).normalized;
            if (Vector2.Dot(perpendicular, toCenter) > 0f) {
                perpendicular = -perpendicular;
            }
            label.SetPosition(GetMiddle() + perpendicular * (Parameters.DefaultLabelSize * FieldCamera.Instance.CeilSize()));
        }
        
        
        public Vector2 GetMiddle() => (Start.transform.position + End.transform.position) * 0.5f;

        public string GetBoardMenuCaption() {
            return String.Empty;
        }

        public List<IndicatorInfo> GetIndicatorInfos() {
            return new List<IndicatorInfo>() {
                new AlgebraExpressionInfo(new NumberExpression(() => Vector2.Distance(Start.Position, End.Position)), $"Length of {Start.Label} - {End.Label}", $"{Start.Label} - {End.Label}")
            };
            //var indicatorInfos = new List<IndicatorInfo>();
            //indicatorInfos.Add(new TextInfo(() => $"{Start.Label} - {End.Label} = {Vector2.Distance(Start.Position, End.Position)}",
            //    () => $"Side of {Parent.name}: {Start.Label} - {End.Label}"));
            //return indicatorInfos;
        }

        public List<IIndicable> GetChildrenIndicators() {
            return null;
        }
    }
}