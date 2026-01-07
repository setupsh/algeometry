using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class BoolParameterUI : GeometryParameterUI {
        [SerializeField] private TMPro.TextMeshProUGUI _caption;
        [SerializeField] private Toggle _toggle;
        private bool value;
        public override void SetCaption(string caption) {
            _caption.text = caption;
        }

        public void UpdateValue() {
            value = _toggle.isOn;
        }

        public override IGeometryValue GetValue() {
            return new BoolValue(value);
        }
    }
    
    public class BoolValue : IGeometryValue<bool> {
        public bool Value { get; }

        public BoolValue(bool value) {
            Value = value;
        }
    }
}