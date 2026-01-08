using UnityEngine;
using TMPro;

namespace Geometry {
    public class Label : MonoBehaviour {
        [SerializeField] private TextMeshPro _text;
        public TextMeshPro TextMeshPro { get { return _text; } }
        public Renderer Renderer {get; private set;}
        private void Awake() {
            Renderer = GetComponent<Renderer>();
        }
        public void SetPosition(Vector2 position) {
            //transform.position = FieldCamera.Instance.Camera.WorldToScreenPoint(position);
            transform.position = position;
        }
    }
}