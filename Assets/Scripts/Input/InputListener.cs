using Mono.Cecil.Cil;
using UnityEngine;

public class InputListener : MonoBehaviour {
    public static InputListener Instance { get; private set; }
    public static UserInputActions InputActions { get; private set; }
    
    public static Vector2 MousePosition =>  InputActions.UI.MousePosition.ReadValue<Vector2>();
    public static Vector2 MouseDelta =>  InputActions.UI.MouseDelta.ReadValue<Vector2>();
    
    public static float Scroll => InputActions.UI.ScrollWheel.ReadValue<Vector2>().normalized.y;
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
        }
    }

}
