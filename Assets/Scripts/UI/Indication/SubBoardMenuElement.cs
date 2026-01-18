using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace UI {
    public class SubBoardMenuElement : MonoBehaviour {
        [SerializeField] private TMPro.TextMeshProUGUI _text;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private BoardMenu _boardMenuPrefab;
        public RectTransform rectTransform => _rectTransform;
        private List<IIndicable> indicables;
        private IndicatorsList getter;
        
        public void Init(List<IIndicable> indicables, string caption, IndicatorsList getter) {
            this.indicables = indicables;
            _text.SetText(caption);
            this.getter = getter;
        }
        
        public void OpenMenu() {
            getter.BoardMenuContainer.GenerateBoardMenu(indicables, getter);
        }
    }
}