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

    public Label CreateLabel(string folderName) {
        Transform folder = transform.Find(folderName);
        if (folder == null) {
            folder = new GameObject(folderName).transform;
            folder.SetParent(transform);
        }
        return Instantiate(_labelReference, folder);
    }
}
