using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace UI {
    public class DescriptionInvoker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        [SerializeField] private string _descriptionText;
        private static Description description = null;

        private void Awake() {
            if (description == null) {
                description = Instantiate(Geometry.Resources.DescriptionPrefab, transform.parent);
            }
        }

        public void OnPointerEnter(PointerEventData eventData) {
            description.gameObject.SetActive(true);
            description.SetText(_descriptionText);
        }

        public void OnPointerExit(PointerEventData eventData) {
            description.gameObject.SetActive(false);
        }
    }
}