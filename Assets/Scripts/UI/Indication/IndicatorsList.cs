using UnityEngine;
using System.Collections.Generic;

namespace UI {
    public class IndicatorsList : MonoBehaviour {
        [SerializeField] private RectTransform _content;
        [SerializeField] private Indicator _prefab;
        [SerializeField] private GameObject _addIndicatorButtonPrefab;
        [SerializeField] private string _text;
        private List<Indicator> _indicators = new List<Indicator>();
        private RectTransform _indicatorButton;

        private void Start() {
            foreach (RectTransform child in _content) {
                if (child.TryGetComponent<Indicator>(out Indicator indicator)) {
                    _indicators.Add(indicator);
                }
            }
            if (_indicators.Count > 0) {
                RefreshPositions();
            }
            _indicatorButton = Instantiate(_addIndicatorButtonPrefab, _content).GetComponent<RectTransform>();
            UpdateAddButtonPosition();
        }

        public void AddIndicator(IndicatorInfo info) {
            Indicator indicator = Instantiate(_prefab, _content);
            indicator.SetInfo(info);
            indicator.RectTransform.anchoredPosition = new Vector2(0, CalculateYPosition(_indicators.Count));
            _indicators.Add(indicator);
            UpdateAddButtonPosition();
        }

        public void RefreshPositions() {
            foreach (Indicator indicator in _indicators) {
                indicator.RectTransform.anchoredPosition = new Vector2(0, CalculateYPosition(_indicators.Count));
            }
            UpdateAddButtonPosition();
        }

        [ContextMenu("Add Indicator")]
        public void AddIndicator() {
            AddIndicator(new TextInfo(_text));
        }

        private void UpdateAddButtonPosition() {
            _indicatorButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, CalculateYPosition(_indicators.Count));
        }

        private float CalculateYPosition(int index) {
            float sum = 0f;
            for (int i = 0; i < index; i++) {
                sum += _indicators[i].RectTransform.sizeDelta.y;
            }
            return -sum;
            float height = -_prefab.RectTransform.sizeDelta.y;
            float y;
            Debug.Log(_content.sizeDelta.y);
            if (_indicators.Count == 0) {
                y = height / 2f;
            }
            else {
                y = height / 2f + (height * _indicators.Count);
            }
            return y;
        }
    }
    
}
