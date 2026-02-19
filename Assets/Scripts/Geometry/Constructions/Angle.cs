using System;
using UnityEngine;
using System.Collections.Generic;
using Algebra;
using UnityEngine.Rendering;
using Geometry;
using UI;

namespace Geometry {
    public class Angle : Construction, IIndicable {
        private GeometryPoint vertex, armA, armB;
        private Color color;
        private GeometricalLineRenderer lineRenderer;
        private float angleValue;
        private float angleValueRad => angleValue * Mathf.Deg2Rad;
        private bool showLabel;
        private string uniqueLabel;
        private Label label = null;
        private float preferredRadius;
        private bool independenceFromFigure;

        public void Init(Figure parent, GeometryPoint armA, GeometryPoint vertex, GeometryPoint armB, Color color, bool showLabel ,string uniqueLabel = "", bool independenceFromFigure = true) {
            this.vertex = vertex;
            this.armA = armA;
            this.armB = armB;
            this.color = color;
            this.showLabel = showLabel;
            this.uniqueLabel = uniqueLabel;
            this.independenceFromFigure = independenceFromFigure;
            
            FieldCamera.OnCameraZoom += OnCameraZoom;

            base.Init(parent);
        }

        private void OnDisable() {
            FieldCamera.OnCameraZoom -= OnCameraZoom;
        }
        public override void UpdateConstruction() {
            if (independenceFromFigure) {
                angleValue = (float)Utilities.GetAngleFigure(vertex.Position, armA.Position, armB.Position, Parent.GetCenter());
            }
            else {
                angleValue = Utilities.GetSighedAngle(armA.Position, vertex.Position, armB.Position);
                if (angleValue < 0) {
                    angleValue += 360f;
                }
            }
            Debug.Log(angleValue);
            List<Vector2> points;
            if (Math.Abs(angleValue - 90f) < 1e-4f) {
                lineRenderer.ClearPoints();
                points = new List<Vector2>() {
                    vertex.Position + (armA.Position - vertex.Position).normalized * CalculateRadius(),
                    vertex.Position + (armA.Position - vertex.Position).normalized * CalculateRadius() + (armB.Position - vertex.Position).normalized * CalculateRadius(),
                    vertex.Position + (armB.Position - vertex.Position).normalized * CalculateRadius()
                };
            }
            else {
                if (independenceFromFigure)
                    points = Utilities.GenerateAngleSector(vertex.transform.position, armA.transform.position, armB.transform.position, CalculateRadius(), Parent.GetCenter());
                else 
                    points = Utilities.GenerateAngleSectorStandalone(vertex.transform.position, armA.transform.position, armB.transform.position, CalculateRadius());
                points.RemoveAt(0);
            }
            for (int i = 0; i < points.Count; i++) {
                lineRenderer.SetPosition(i, points[i]);
            }
            if (showLabel && label) {
                label.SetPosition((vertex.Position + points[points.Count / 2]) * 0.5f);
                label.TextMeshPro.fontSize = Parameters.DefaultLabelSize * FieldCamera.Instance.CeilSize();
            }
            
        }

        protected override void CreateConstruction() {
            lineRenderer = gameObject.AddComponent<RoundedLineRenderer>();
            lineRenderer.Setup(new RoundedLineRendererConfig(Parameters.AngleWidth, false, color, Parameters.DefaultSortingOrder - 1, true, false, 2));
            preferredRadius = Parameters.AngleRadius * FieldCamera.Instance.CeilSize();
            if (showLabel) {
                label = GeometricalLabelSystem.Instance.CreateLabel(transform, Parameters.LabelSortingOrder);
                label.TextMeshPro.text = GetCaption();
                label.TextMeshPro.color = color;
                label.TextMeshPro.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            }
            UpdateConstruction();
        }

        private float CalculateRadius() {
            if (Vector2.Distance(vertex.Position, armA.Position) < preferredRadius || Vector2.Distance(vertex.Position, armB.Position) < preferredRadius) {
                return Mathf.Min(Vector2.Distance(vertex.Position, armA.Position), Vector2.Distance(vertex.Position, armB.Position));
            }
            return preferredRadius;
            //if (_angleValue < 30f) {
            //    if (_angleValue < 10f) {
            //        return AngleRadius + (Mathf.Cos(_angleValue * Mathf.Deg2Rad) * AngleRadius * 5f);
            //    }
            //    return AngleRadius + (Mathf.Cos(_angleValue * Mathf.Deg2Rad) * AngleRadius * 2.5f);
            //}
            //return AngleRadius + (Mathf.Clamp(Mathf.Cos(_angleValue * Mathf.Deg2Rad), 0, 1) * AngleRadius);
        }

        private string GetCaption() {
            if (uniqueLabel == String.Empty) {
                return $"{vertex.Label}";
            }
            return uniqueLabel;
        }
        
        private void OnCameraZoom() {
            preferredRadius = Parameters.AngleRadius * FieldCamera.Instance.CeilSize();
            UpdateConstruction();
        }

        public string GetBoardMenuCaption() {
            return $"Угол {GetCaption()}";
        }
        
        public enum Indicators {Sin, Cos, Tan, Ctg, Deg, Rad}
        
        public List<IndicatorInfo> GetIndicatorInfos() {
            return new List<IndicatorInfo>() {
                new RawNumberInfo(new NumberExpression(() => Math.Sin(angleValueRad)), "Синус",
                    $"sin({GetCaption()})"),
                new AlgebraExpressionInfo(new NumberExpression(() => Math.Cos(angleValueRad)), "Косинус",
                    $"cos({GetCaption()})"),
                new AlgebraExpressionInfo(new NumberExpression(() => Math.Tan(angleValueRad)), "Тангенс",
                    $"tan({GetCaption()})"),
                new AlgebraExpressionInfo(new NumberExpression(() => 1 / Math.Tan(angleValueRad)), "Котангенс",
                    $"ctg({GetCaption()})"),
                new AlgebraExpressionInfo(new NumberExpression(() => angleValue), "Угол (градусы)",
                $"{GetCaption()}°"),
                new RawNumberInfo(new NumberExpression(() => angleValueRad), "Угол (радианы)",
                    $"{GetCaption()}")
            };
        }

        public List<IIndicable> GetChildrenIndicators() {
            return null;
        }
    }
    [System.Serializable]
    public class AngleGenerator : ConstructionGenerator {
        public override void Generate(List<IGeometryValue> arguments) {
            Board.Instance.Instantiate<Angle>().Init(
                Get<Figure>(arguments, 0),
                Get<GeometryPoint>(arguments, 1),
                Get<GeometryPoint>(arguments, 2),
                Get<GeometryPoint>(arguments, 3),
                Get<Color>(arguments, 4),
                Get<bool>(arguments, 5));
        }
    }
}
