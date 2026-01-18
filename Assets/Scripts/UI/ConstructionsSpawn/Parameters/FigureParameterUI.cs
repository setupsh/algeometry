using System;
using UnityEngine;
using Geometry;
using TMPro;
using System.Collections.Generic;
using Geometry.Realisations;
using NaughtyAttributes;

namespace UI {
    public class FigureParameterUI : GeometryParameterUI {
        [SerializeField] private TextMeshProUGUI _caption;
        [SerializeField] private TMP_Dropdown _dropdown;
        private Dictionary<string, Figure> options =  new Dictionary<string, Figure>(); 
        private Figure figure;
        public override void SetCaption(string caption) {
            _caption.text = caption;
        }

        public void SetFigureByDropdown() {
            figure = options[_dropdown.options[_dropdown.value].text];
        }

        private void Start() {
            foreach (Figure figure in Board.Instance.Collect<Figure>()) {
                options[figure.GetBoardMenuCaption()] = figure;
                _dropdown.options.Add(new TMP_Dropdown.OptionData(figure.GetBoardMenuCaption()));
            }
            _dropdown.captionText.text = String.Empty;
        }

        public override IGeometryValue GetValue() {
            if (figure == null) {
                return null;
            }
            return new FigureValue(figure);
        }
    }
    public class FigureValue : IGeometryValue<Figure> {
        public Figure Value { get; }

        public FigureValue(Figure value) {
            Value = value;
        }
    }
}
