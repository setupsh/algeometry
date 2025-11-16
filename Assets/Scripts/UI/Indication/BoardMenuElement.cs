using UnityEngine;

namespace UI {
    public class BoardMenuElement : MonoBehaviour {
        [SerializeField] private TMPro.TextMeshProUGUI _text;
        [SerializeField] private RectTransform _rectTransform;
        public RectTransform rectTransform => _rectTransform;
        private IndicatorInfo _indicatorInfo;
        
        public void Init(IndicatorInfo indicatorInfo, string caption) {
            _indicatorInfo = indicatorInfo;
            _text.SetText(caption);
        }
        
        public IndicatorInfo GetIndicatorInfo() {
            return _indicatorInfo;
        }
    }
}