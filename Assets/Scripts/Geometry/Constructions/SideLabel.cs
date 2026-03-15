using UnityEngine;
using Geometry;

public class SideLabel : Construction {
    private Side side;
    private string caption;
    private Label label;
    private Color color;
    private float distance;
        
    public void Init(Figure parent, Side side, string caption, Color color) {
        this.side = side;
        this.color = color;
        this.caption = caption;
        base.Init(parent);
        parent.OnFigureChanged += UpdateConstruction;
        FieldCamera.OnCameraZoom += UpdateTextSize;
    }

    private void OnDisable() {
        Parent.OnFigureChanged -= UpdateConstruction;
        FieldCamera.OnCameraZoom -= UpdateTextSize;
    }

    private void UpdateTextSize() {
        label.TextMeshPro.fontSize = (Parameters.DefaultLabelSize * 1.5f)  * FieldCamera.Instance.CeilSize();
    }
    public override void UpdateConstruction() {
        Vector2 sideVector = side.Start.transform.position - side.End.transform.position;
        Vector2 perpendicular = new Vector2(-sideVector.y, sideVector.x).normalized;
        Vector2 toCenter = (Parent.GetCenter() - side.GetMiddle()).normalized;
        if (Vector2.Dot(perpendicular, toCenter) > 0f) {
            perpendicular = -perpendicular;
        }
        label.SetPosition(side.GetMiddle());
        float pivotX = (perpendicular.x > 0.1f) ? 0f : (perpendicular.x < -0.1f ? 1f : 0.5f);
        float pivotY = (perpendicular.y > 0.1f) ? 0f : (perpendicular.y < -0.1f ? 1f : 0.5f);
        label.TextMeshPro.rectTransform.pivot = new Vector2(pivotX, pivotY);
    }

    public void SetLabel(string newCaption) {
        caption = newCaption;
        UpdateConstruction();
    }

    protected override void CreateConstruction() {
        label = GeometricalLabelSystem.Instance.CreateLabel(transform, Parameters.LabelSortingOrder);
        label.TextMeshPro.text = Utilities.TransparentMinus + caption + Utilities.TransparentMinus;
        label.TextMeshPro.color = color;
        UpdateTextSize();
        UpdateConstruction();
    }
}
