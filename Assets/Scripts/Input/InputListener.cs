using Geometry;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputListener : MonoBehaviour {
    public static InputListener Instance { get; private set; }
    public static UserInputActions InputActions { get; private set; }
    
    public static Vector2 MousePosition =>  InputActions.Player.MousePosition.ReadValue<Vector2>();
    public static Vector2 MouseDelta =>  InputActions.Player.MouseDelta.ReadValue<Vector2>();
    public static bool MiddleButtonPressed => InputActions.Player.MiddleButton.IsPressed();
    public static bool ShiftPressed => InputActions.Player.Shift.IsPressed();
    public static InputAction Escape => InputActions.Player.Escape;
    
    //public static float MouseScroll => InputActions.UI.ScrollWheel.;
    private void Awake() {
        if (Instance) {
            Destroy(this);
        }
        else {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            InputActions = new UserInputActions();
            InputActions.Enable();
        }
    }
}
