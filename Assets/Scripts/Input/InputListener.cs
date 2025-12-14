using Geometry;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputListener : MonoBehaviour {
    public static InputListener Instance { get; private set; }
    public static UserInputActions InputActions { get; private set; }
    
    public static Vector2 MousePosition =>  InputActions.UI.MousePosition.ReadValue<Vector2>();
    public static Vector2 MouseDelta =>  InputActions.UI.MouseDelta.ReadValue<Vector2>();
    public static bool MiddleButtonPressed => InputActions.UI.ScrollButton.IsPressed();
    
    //public static float MouseScroll => InputActions.UI.ScrollWheel.;
    private void Awake() {
        if (Instance) {
            Destroy(this);
        }
        else {
            Instance = this;
            InputActions = new UserInputActions();
            InputActions.Enable();
            InputActions.UI.ScrollWheel.performed += (InputAction.CallbackContext obj) => ZoomCamera(obj.ReadValue<Vector2>().normalized.y);
        }
    }

    private void Update() {
        if (MiddleButtonPressed) {
            MoveCamera();
        }
    }

    private void MoveCamera() {
        FieldCamera.Instance.MoveCamera(MouseDelta);
    }

    private void ZoomCamera(float scroll) {
        Debug.Log(scroll);
        FieldCamera.Instance.ZoomCamera((int) scroll);
    }

}
