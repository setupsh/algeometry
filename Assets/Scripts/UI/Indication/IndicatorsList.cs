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
        public BoardMenuContainer BoardMenuContainer => _boardMenuContainer;
        private List<Indicator> _indicators = new List<Indicator>();
        private IndicatorAdder _indicatorAdderButton;

        private void Start() {
            _indicatorAdderButton = Instantiate(_addIndicatorButtonPrefab, _content);
            _indicatorAdderButton.SetIndicatorsList(this, _boardMenuContainer);
            Board.OnUpdate += UpdateIndicators;
            //UpdateAddButtonPosition();
        }

        public void AddIndicator(IndicatorInfo info) {
            Indicator indicator = Instantiate(_prefab, _content);
            indicator.SetInfo(info);
            _indicators.Add(indicator);
        }

        public void UpdateIndicators() {
            foreach (Indicator indicator in _indicators) {
                indicator.UpdateInfo();
            }
        }
    }
    
}
