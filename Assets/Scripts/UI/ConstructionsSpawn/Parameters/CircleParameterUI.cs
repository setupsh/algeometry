using System;
using UnityEngine;
using Geometry;
using TMPro;
using System.Collections.Generic;
using Geometry.Realisations;
using NaughtyAttributes;

namespace UI {
    public class CircleParameterUI : GeometryParameterUI {
        [SerializeField] private TextMeshProUGUI _caption;
        [SerializeField] private TMP_Dropdown _dropdown;
        private Dictionary<string, Circle> options =  new Dictionary<string, Circle>(); 
        private Circle circle = null;
        public override void SetCaption(string caption) {
            _caption.text = caption;
        }

        public void SetFigureByDropdown() {
            circle = options[_dropdown.options[_dropdown.value].text];
        }

        private void Start() {
            foreach (Circle circle in Board.Instance.Collect<Circle>()) {
                options[circle.GetBoardMenuCaption()] = circle;
                _dropdown.options.Add(new TMP_Dropdown.OptionData(circle.GetBoardMenuCaption()));
            }
            _dropdown.captionText.text = String.Empty;
        }

        public override IGeometryValue GetValue() {
            if (circle == null) {
                return null;
            }
            return new CircleValue(circle);
        }
    }
    public class CircleValue : IGeometryValue<Circle> {
        public Circle Value { get; }

        public CircleValue(Circle value) {
            Value = value;
        }
    }
}