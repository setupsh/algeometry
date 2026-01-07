using TMPro;
using UnityEngine;

namespace UI {
    public class Popup : MonoBehaviour {
        [SerializeField] private TMP_Text _text;
        
        public void SetText(string text) {
            _text.text = text;
        }
        
        public void Close() {
            Destroy(this.gameObject);
        }
    }
}