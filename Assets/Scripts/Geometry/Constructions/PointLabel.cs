using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
namespace Geometry {
    public class PointLabel : Construction {
        private GeometryPoint point;
        private string caption;
        private Label label;
        private Color color;
        private OffsetConfig offsetConfig;
        
        public void Init(Figure parent, GeometryPoint point, Color color, OffsetConfig offsetConfig) {
            this.point = point;
            this.color = color;
            this.offsetConfig = offsetConfig;
            base.Init(parent);
            FieldCamera.OnCameraZoom += UpdateTextSize;
        }

        private void OnDisable() {
            FieldCamera.OnCameraZoom -= UpdateTextSize;
        }
        public override void UpdateConstruction() {
            label.SetPosition(offsetConfig.CalculateOffset());
        }

        protected override void CreateConstruction() {
            label = GeometricalLabelSystem.Instance.CreateLabel(transform, Parameters.LabelSortingOrder);
            label.TextMeshPro.text = point.Label;
            label.TextMeshPro.color = color;
            UpdateConstruction();
        }

        private void UpdateTextSize() {
            label.TextMeshPro.fontSize = Parameters.DefaultLabelSize * FieldCamera.Instance.CeilSize();
        }
    }

    [System.Serializable]
    public class LabelsGenerator : ConstructionGenerator {
        public override void Generate(List<IGeometryValue> arguments) {
            Figure figure = Get<Figure>(arguments, 0);
            Color color = Get<Color>(arguments, 1);
            float multiplier = Get<float>(arguments, 2);
            foreach (GeometryPoint point in figure.Points) {
                Board.Instance.Instantiate<PointLabel>().Init(figure, point, color, new CentroidOffset(figure, point, multiplier));
            }
        }
    }
}