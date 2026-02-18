using UnityEngine;
using System.Collections.Generic;
namespace UI {
    public class BoardMenu : MonoBehaviour {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private GenericMenuButton _buttonPrefab; 

        public void Init(List<IMenuEntry> entries, IndicatorsList getter, BoardMenuContainer container) {
            foreach (var entry in entries) {
                var btn = Instantiate(_buttonPrefab, _rectTransform);
                btn.Setup(entry, getter, container);
            }
        }
    }
}