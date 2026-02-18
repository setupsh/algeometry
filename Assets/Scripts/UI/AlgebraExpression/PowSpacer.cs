using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class PowSpacer : MonoBehaviour {
        [SerializeField] private LayoutElement _layoutElement;

        public void SetHeight(float height) {
            _layoutElement.minHeight = height;
            _layoutElement.preferredHeight = height;
        }
    }
}