using UnityEngine;
using Geometry;
using TMPro;
using System.Collections.Generic;
namespace UI {
    public class FigureParameterUI : GeometryParameterUI {
        [SerializeField] private TextMeshProUGUI _caption;
        [SerializeField] private TMP_Dropdown _dropdown;
        private Dictionary<string, Figure> options =  new Dictionary<string, Figure>(); 
        [HideInInspector] public Figure figure;
        public override void SetCaption(string caption) {
            _caption.text = caption;
        }

        public void SetFigureByIndex(int index) {
            figure = options[_dropdown.options[index].text];
        }

        private void Start() {
            foreach (Figure figure in Board.Instance.Collect<Figure>()) {
                options[figure.GetCaption()] = figure;
                _dropdown.options.Add(new TMP_Dropdown.OptionData(figure.GetCaption()));
            }
        }

        public override IGeometryValue GetValue() {
            return new FigureValue(figure);
        }
    }
}
public class FigureValue : IGeometryValue<Figure> {
    public Figure Value { get; }

    public FigureValue(Figure value) {
        Value = value;
    }
}