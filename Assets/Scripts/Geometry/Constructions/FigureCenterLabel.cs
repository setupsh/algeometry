using UnityEngine;

using UnityEngine;
using Geometry;

public class FigureCenterLabel : Construction {
    private Figure figure;
    private Label label;
    private Color color;
    private string caption;

    public void Init(Figure parent, string caption, Color color) {
        this.caption = caption;
        this.color = color;
        base.Init(parent);
        parent.OnFigureChanged += UpdateConstruction;
        FieldCamera.OnCameraZoom += UpdateTextSize;
    }

    private void OnDisable() {
        Parent.OnFigureChanged -= UpdateConstruction;
        FieldCamera.OnCameraZoom -= UpdateTextSize;
    }

    public void SetCaption(string newCaption) {
        caption = newCaption;
        label.TextMeshPro.text = caption;
    }

    private void UpdateTextSize() {
        label.TextMeshPro.fontSize = Parameters.DefaultLabelSize * FieldCamera.Instance.CeilSize();
    }

    public override void UpdateConstruction() {
        label.SetPosition(Parent.GetCenter());
    }

    protected override void CreateConstruction() {
        label = GeometricalLabelSystem.Instance.CreateLabel(transform, Parameters.LabelSortingOrder);
        label.TextMeshPro.text = caption;
        label.TextMeshPro.color = color;
        label.TextMeshPro.alignment = TMPro.TextAlignmentOptions.Center;
        UpdateTextSize();
        UpdateConstruction();
    }
}