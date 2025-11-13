using UnityEngine;
using TMPro;

namespace Geometry {
    public class Label : MonoBehaviour {
        [SerializeField] private TextMeshPro  _text;
        private Renderer  _renderer;

        private void Awake() {
            _renderer = GetComponent<Renderer>();
        }
        public void SetPosition(Vector2 position) {
            //transform.position = FieldCamera.Instance.Camera.WorldToScreenPoint(position);
            transform.position = position;
        }
        public void SetScreenPosition(Vector2 screenPosition) {
            transform.position = screenPosition;
        }
        
        public void SetRotation(Vector2 rotation) {
            transform.up = rotation;
        }

        public void SetText(string text) {
            _text.text = text;
        }

        public void SetSize(float size) {
            _text.fontSize = size;
        }

        public void SetColor(Color color) {
            _text.color = color;
        }
        public float GetFontSize() {
            return _text.fontSize;
        }
        public Renderer  GetRenderer() {
            return GetComponent<Renderer>();
        }
    }
}