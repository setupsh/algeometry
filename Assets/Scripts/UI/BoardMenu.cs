using UnityEngine;
using System.Collections.Generic;
namespace UI {
    public class BoardMenu : MonoBehaviour {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private BoardMenuElement _boardMenuElementPrefab;
        [SerializeField] private SubBoardMenuElement _subBoardMenuPrefab;
        private List<SubBoardMenuElement> _subBoardMenuElements = new List<SubBoardMenuElement>();
        private List<BoardMenuElement> _boardMenuElements = new List<BoardMenuElement>();
        
        public void Setup(List<IIndicable> indecables, IndicatorsList getter) {
            float y = 0;
            foreach (var indecable in indecables) {
                if (indecable.GetChildrenIndicators() == null) {
                    foreach (var indicator in indecable.GetIndicatorInfos()) {
                        BoardMenuElement boardMenuElement = Instantiate(_boardMenuElementPrefab, _rectTransform);
                        boardMenuElement.rectTransform.anchoredPosition = new Vector2(0, y);
                        boardMenuElement.Init(indicator, indicator.GetCaption(), getter);
                        _boardMenuElements.Add(boardMenuElement);
                        y -= boardMenuElement.rectTransform.sizeDelta.y;
                    }
                }
                else {
                    SubBoardMenuElement subBoardMenuElement = Instantiate(_subBoardMenuPrefab, _rectTransform);
                    subBoardMenuElement.rectTransform.anchoredPosition = new Vector2(0, y);
                    subBoardMenuElement.Init(indecable.GetChildrenIndicators(), indecable.GetCaption(), getter);
                    _subBoardMenuElements.Add(subBoardMenuElement);
                    y -= subBoardMenuElement.rectTransform.sizeDelta.y;
                }
            }
        }
    }
}