using UnityEngine;
using UnityEngine.InputSystem;

public class InputEvent
{
    TouchAction playerInputActions;

    public event System.Action<Vector2> OnTouchEvent;

    public InputEvent()
    {
        playerInputActions = new TouchAction();
        playerInputActions.Enable();
        playerInputActions.Touch.TouchPosition.performed += TouchPress;
    }

    void TouchPress(InputAction.CallbackContext context)
    {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            OnTouchEvent?.Invoke(touchPosition);
    }
}
