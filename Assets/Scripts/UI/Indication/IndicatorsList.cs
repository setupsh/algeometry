using System;
using UnityEngine;
using System.Collections.Generic;
using Geometry;

namespace UI {
    public class IndicatorsList : MonoBehaviour {
        [SerializeField] private RectTransform _content;
        [SerializeField] private Indicator _prefab;
        [SerializeField] private IndicatorAdder _addIndicatorButtonPrefab;
        [SerializeField] private BoardMenuContainer _boardMenuContainer;
        [SerializeField] private Animator _animator;
        public BoardMenuContainer BoardMenuContainer => _boardMenuContainer;
        private List<Indicator> _indicators = new List<Indicator>();
        private IndicatorAdder _indicatorAdderButton;

        private void Start() {
            if (_addIndicatorButtonPrefab != null) {
                _indicatorAdderButton = Instantiate(_addIndicatorButtonPrefab, _content);
                _indicatorAdderButton.SetIndicatorsList(this, _boardMenuContainer);
            }
            Board.OnUpdate += UpdateIndicators;
        }

        private void OnDestroy() {
            Board.OnUpdate -= UpdateIndicators;
        }

        public void AddIndicator(IndicatorInfo info) {
            Indicator indicator = Instantiate(_prefab, _content);
            Debug.Log(indicator.name);
            indicator.SetInfo(info);
            _indicators.Add(indicator);
            _animator.SetTrigger(AnimatorKeys.Open);
        }

        public void UpdateIndicators() {
            foreach (Indicator indicator in _indicators) {
                indicator.UpdateInfo();
            }
        }

        public void ClearAll() {
            _animator.SetTrigger(AnimatorKeys.Close);
            foreach (Indicator indicator in _indicators) {
                indicator.Delete();
            }
        }
    }
}
