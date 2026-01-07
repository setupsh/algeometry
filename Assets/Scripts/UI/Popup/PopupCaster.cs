using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public enum PopupType {Info, Warning, Error, }
    public class PopupCaster : MonoBehaviour {
        [SerializeField] private Popup _errorPopupPrefab, _warningPopupPrefab, _infoPopupPrefab;
        public static PopupCaster Instance { get; private set; }
        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(this);
            }
        }

        private void Cast(PopupType type, string text) {
            Popup popup;
            switch (type) {
                case PopupType.Warning: popup = Instantiate(_warningPopupPrefab, transform); break;
                case PopupType.Error: popup = Instantiate(_errorPopupPrefab, transform); break;
                default: popup = Instantiate(_infoPopupPrefab, transform); break;
            }
            popup.SetText(text);
        }

        public static void Info(string text) {
            Instance.Cast(PopupType.Info, text);
        }

        public static void Warning(string text) {
            Instance.Cast(PopupType.Warning, text);
        }

        public static void Error(string text) {
            Instance.Cast(PopupType.Error, text);
        }
    }
}
