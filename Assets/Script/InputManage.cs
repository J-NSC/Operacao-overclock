using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManage : MonoBehaviour
{
    PlayerInput playerInput;
    
    InputAction touchPositionAction;
    InputAction touchPressAction;

    void Awake()
    {
        // playerInput = GetComponent<PlayerInput>();
        // touchPressAction = playerInput.actions["TouchPress"];
        // touchPositionAction = playerInput.actions["TouchPosition"];
    }

    void OnEnable()
    {
        
        touchPressAction.performed += TouchPressed;
    }

    void OnDisable()
    {
        touchPressAction.performed -= TouchPressed;
    }
    
    void TouchPressed(InputAction.CallbackContext context)
    {
       
    }
}
