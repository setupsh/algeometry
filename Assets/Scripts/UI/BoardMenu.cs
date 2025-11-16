using UnityEngine;
using System.Collections.Generic;
namespace UI {
    public class BoardMenu : MonoBehaviour {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private BoardMenuElement _boardMenuElementPrefab;
        private List<BoardMenuElement> _boardMenuElements;
        
        public void Init(List<IIndicable> indicators) {
            float sum = 0;
            foreach (var indicable in indicators) {
                BoardMenuElement boardMenuElement = Instantiate(_boardMenuElementPrefab, _rectTransform);
                boardMenuElement.rectTransform.anchoredPosition = new Vector2(0, sum);
                boardMenuElement.Init(indicable.GetIndicatorInfo(), indicable.GetIndicatorInfo().GetString());
                _boardMenuElements.Add(boardMenuElement);
                sum += indicable.GetIndicatorInfo().GetSize().y;
            }
        }
    }
}