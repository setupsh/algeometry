using UnityEngine;

namespace UI {
    public class BoardMenuElement : MonoBehaviour {
        [SerializeField] private TMPro.TextMeshProUGUI _text;
        [SerializeField] private RectTransform _rectTransform;
        public RectTransform rectTransform => _rectTransform;
        private IndicatorInfo _indicatorInfo;
        private IndicatorsList _getter;
        
        public void Init(IndicatorInfo indicatorInfo, string caption, IndicatorsList getter) {
            _indicatorInfo = indicatorInfo;
            _text.SetText(caption);
            _getter = getter;
        }

        public void Send() {
            _getter.AddIndicator(_indicatorInfo);
        }
    }
}