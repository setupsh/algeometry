using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class ColorParameterUI : GeometryParameterUI {
        [SerializeField] private TMP_Text _caption;
        [SerializeField] private TMP_InputField _colorInputR, _colorInputG, _colorInputB;
        [SerializeField] private Image _colorPreview;
        private Color color;
        public override void SetCaption(string caption) {
            _caption.text = caption;
        }

        public void UpdateColor() {
            FixInputFields(_colorInputR, _colorInputG, _colorInputB);
            color = new Color(TextToFloat(_colorInputR.text), TextToFloat(_colorInputG.text), TextToFloat(_colorInputB.text));
            _colorPreview.color = color;
        }

        private void Start() {
            _colorInputR.text = Random.Range(0, 255).ToString();
            _colorInputG.text = Random.Range(0, 255).ToString();
            _colorInputB.text = Random.Range(0, 255).ToString();
            UpdateColor();
        }

        public override IGeometryValue GetValue() {
            return new ColorValue(color);
        }

        private float TextToFloat(string text) {
            if (float.TryParse(text, out float result)) {
                return result / 255f;
            }
            return 0f;
        }

        private void FixInputFields(params TMP_InputField[] fields) {
            foreach (TMP_InputField inputField in fields) {
                int value =  int.Parse(inputField.text);
                if (value < 0 || value > 255) {
                    inputField.text = Mathf.Clamp(value, 0, 255).ToString();
                }
            }
        }
    }

    public class ColorValue : IGeometryValue<Color> {
        public Color Value { get; }

        public ColorValue(Color value) {
            Value = value;
        }
    }
    
}
