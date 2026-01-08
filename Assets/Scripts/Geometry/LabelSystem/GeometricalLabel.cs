using UnityEngine;
using Geometry;
public class GeometricalLabelSystem : MonoBehaviour {
    [SerializeField] private Label _labelReference;
    public static GeometricalLabelSystem Instance {get; private set;}
    private void Awake() {
        if (Instance != null) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }

    public Label CreateLabel(Transform parent, int renderOrder) {
        Label label = Instantiate(_labelReference, parent);
        label.TextMeshPro.color = Color.black;
        label.TextMeshPro.sortingOrder = renderOrder;
        return label;
    }
}
