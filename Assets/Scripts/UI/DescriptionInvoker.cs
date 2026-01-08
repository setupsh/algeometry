using Geometry;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace UI {
    public class DescriptionInvoker : MonoBehaviour, IPointerExitHandler, IPointerMoveHandler {
        [SerializeField] private string _descriptionText;
        private static Description description = null;
        private static Vector2 offset = new Vector2(-30, 30);

        private void Awake() {
            if (description == null) {
                description = Instantiate(Geometry.Resources.DescriptionPrefab, Board.MainCanvas.transform);
            }
        }

        public void OnPointerMove(PointerEventData eventData) {
            description.gameObject.SetActive(true);
            description.transform.position = eventData.position - offset;
            description.SetText(_descriptionText);
        }

        public void OnPointerExit(PointerEventData eventData) {
            description.gameObject.SetActive(false);
        }
    }
}