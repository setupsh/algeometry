using UnityEngine;
using System.Collections.Generic;
namespace UI {
    public class BoardMenu : MonoBehaviour {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private BoardMenuElement _boardMenuElementPrefab;
        [SerializeField] private SubBoardMenuElement _subBoardMenuPrefab;
        
        public void Setup(List<IIndicable> indecables, IndicatorsList getter) {
            foreach (var indecable in indecables) {
                if (indecable.GetChildrenIndicators() == null) {
                    foreach (var indicator in indecable.GetIndicatorInfos()) {
                        BoardMenuElement boardMenuElement = Instantiate(_boardMenuElementPrefab, _rectTransform);
                        boardMenuElement.Init(indicator, indicator.BoardMenuCaption, getter, this);
                    }
                }
                else {
                    SubBoardMenuElement subBoardMenuElement = Instantiate(_subBoardMenuPrefab, _rectTransform);
                    subBoardMenuElement.Init(indecable.GetChildrenIndicators(), indecable.GetBoardMenuCaption(), getter);
                }
            }
        }
    }
}