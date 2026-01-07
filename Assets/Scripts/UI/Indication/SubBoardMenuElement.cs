using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace UI {
    public class SubBoardMenuElement : MonoBehaviour {
        [SerializeField] private TMPro.TextMeshProUGUI _text;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private BoardMenu _boardMenuPrefab;
        public RectTransform rectTransform => _rectTransform;
        private List<IIndicable> _indicables;
        private IndicatorsList _getter;
        
        public void Init(List<IIndicable> indicables, string caption, IndicatorsList getter) {
            _indicables = indicables;
            _text.SetText(caption);
            _getter = getter;
        }
        
        public void OpenMenu() {
            _getter.BoardMenuContainer.GenerateBoardMenu(_indicables, _getter);
        }
    }
}