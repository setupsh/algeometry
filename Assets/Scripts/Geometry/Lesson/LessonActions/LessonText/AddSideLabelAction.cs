using Geometry;
using UnityEngine;

public class AddSideLabelAction : ReturnLessonAction<SideLabel> {
    [SerializeField] private ReturnLessonAction<Figure> _figure;
    [SerializeField] private int _sideIndex;
    [SerializeField] private string _caption;
    [SerializeField] private Color _color;
    public override SideLabel Value { get; protected set; }
    protected override void Enter() {
        SideLabel sideLabel = Board.Instance.Instantiate<SideLabel>();
        sideLabel.Init(_figure.Value, _figure.Value.Sides[_sideIndex], _caption, _color);
        _figure.Value.AddConstruction(sideLabel);
        Value = sideLabel;
        Complete();
    }

}
