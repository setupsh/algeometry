using Geometry;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
namespace UI {
    public abstract class IndicatorInfo {
        public abstract Vector2 GetSize();
        public abstract string GetString();
        public abstract string GetCaption();
    }

    public class TextInfo : IndicatorInfo {
        private Func<string> _text, _caption;
        public TextInfo(Func<string> text, Func<string> caption) {
            _text = text;
            _caption = caption;
        }

        public override Vector2 GetSize() {
            return new Vector2(0f, Parameters.Instance.DefaultIndicatorSizeY);
        }

        public override string GetString() {
            return _text();
        }

        public override string GetCaption() {
            return _caption();
        }
    }
    [RequireComponent(typeof(RectTransform))]
    public class Indicator : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private RectTransform _rectTransform;
        private IndicatorInfo _indicatorInfo;
        public void SetInfo(IndicatorInfo info) {
            _indicatorInfo = info;
            UpdateInfo();
        }
        public void UpdateInfo() {
            _label.text = _indicatorInfo.GetString();
        }
        public void Delete() {
            Destroy(gameObject);
        }
        public RectTransform RectTransform => _rectTransform;
    }
}
