using Geometry;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public abstract class IndicatorInfo {
        public abstract Vector2 GetSize();
        public abstract string GetString();
    }

    public class TextInfo : IndicatorInfo {
        private string _text;
        public TextInfo(string text) {
            _text = text;
        }

        public override Vector2 GetSize() {
            return new Vector2(0f, Parameters.Instance.DefaultIndicatorSizeY);
        }

        public override string GetString() {
            return _text;
        }
    }
    [RequireComponent(typeof(RectTransform))]
    public class Indicator : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private RectTransform _rectTransform;
        public void SetInfo(IndicatorInfo info) {
            _label.text = info.GetString();
        }
        public void Delete() {
            Destroy(gameObject);
        }
        public RectTransform RectTransform => _rectTransform;
    }
}
