using System.Globalization;
using Geometry;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class FloatParameterUI : GeometryParameterUI {
        [SerializeField] private TMPro.TextMeshProUGUI _caption;
        [SerializeField] private TMPro.TextMeshProUGUI _value;
        [SerializeField] private Slider _slider;
        private float value = 1f;
        public override void SetCaption(string caption) {
            _caption.text = caption;
        }

        public void UpdateValue() {
            value = _slider.value;
            _value.text = value.ToStringDecimalPlaces(2);
        }

        public override IGeometryValue GetValue() {
            return new FloatValue(value);
        }
    }

    public class FloatValue : IGeometryValue<float> {
        public float Value { get; }

        public FloatValue(float value) {
            Value = value;
        }
    }
}