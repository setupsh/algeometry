using Geometry;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace UI {
    public class IndicatorAdder : MonoBehaviour {
        [SerializeField] private BoardMenu _boardMenuPrefab;
        [SerializeField] private RectTransform _rectTransform;
        public RectTransform RectTransform => _rectTransform;
        private IndicatorsList _indicatorsList;
        private BoardMenuContainer _container;

        public void SetIndicatorsList(IndicatorsList indicatorsList, BoardMenuContainer container) {
            _container = container;
            _indicatorsList = indicatorsList;
        }

        public void ShowMenu(BaseEventData eventData) {
            PointerEventData pointerEventData = (PointerEventData) eventData;
            Debug.Log(Board.Instance);
            if (pointerEventData.button == PointerEventData.InputButton.Left) {
                _container.GenerateBoardMenu(Board.Instance.CollectIndicables(), _indicatorsList);
            } else if (pointerEventData.button == PointerEventData.InputButton.Right) {
                _container.DestroyBoardMenu();
            }
        }
    }
}
