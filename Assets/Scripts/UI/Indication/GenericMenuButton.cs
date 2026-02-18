using UnityEngine;

namespace UI {
    public class GenericMenuButton : MonoBehaviour {
        [SerializeField] private TMPro.TextMeshProUGUI _text;
        private IMenuEntry entry;
        private IndicatorsList getter;
        private BoardMenuContainer container;

        public void Setup(IMenuEntry entry, IndicatorsList getter, BoardMenuContainer container) {
            this.entry = entry;
            this.getter = getter;
            this.container = container;
            _text.SetText(entry.GetTitle());
        }

        public void OnClick() {
            entry.OnClick(container, getter);
        }
    }
}