using Geometry;
using UnityEngine;

namespace UI {
    public class IndicatorAdder : MonoBehaviour {
        [SerializeField] private BoardMenu _boardMenuPrefab;
        [SerializeField] private RectTransform _rectTransform;
        public RectTransform RectTransform => _rectTransform;
        private IndicatorsList _indicatorsList;

        public void SetIndicatorsList(IndicatorsList indicatorsList) {
            _indicatorsList = indicatorsList;
        }

        public void ShowMenu() {
            BoardMenu boardMenu = Instantiate(_boardMenuPrefab, _rectTransform);
            boardMenu.Setup(Board.Instance.CollectIndicables(), _indicatorsList);
        }
    }
}
