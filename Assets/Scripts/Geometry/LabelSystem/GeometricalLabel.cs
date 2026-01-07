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

    public Label CreateLabel(Transform parent) {
        Label label = Instantiate(_labelReference, parent);
        label.SetColor(Color.black);
        return Instantiate(_labelReference, parent);
    }
}
