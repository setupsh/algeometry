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
            if (Board.Instance.CollectIndicables().Count == 0) {
                PopupCaster.Error("Nothing to show");
                return;
            }
            PointerEventData pointerEventData = (PointerEventData) eventData;
            if (pointerEventData.button == PointerEventData.InputButton.Left) {
                _container.GenerateMenu(Board.Instance.CollectIndicables(), _indicatorsList);
            } else if (pointerEventData.button == PointerEventData.InputButton.Right) {
                _container.DestroyBoardMenu();
            }
        }
    }
}
