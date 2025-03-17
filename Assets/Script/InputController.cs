using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction MoveAction;

    private Vector2 CurrentMovementVector;
    private Vector2 TargetMovementVector;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        MoveAction = playerInput.actions.FindActionMap("Move").FindAction("Move");
        MoveAction.performed += MoveAction_performed;
        MoveAction.canceled += MoveAction_canceled;
    }
    private void MoveAction_canceled(InputAction.CallbackContext callbackContext)
    {
        CurrentMovementVector = Vector2.zero;
        TargetMovementVector = Vector2.zero;
    }

    private void MoveAction_performed(InputAction.CallbackContext callbackContext)
    {
        TargetMovementVector = callbackContext.ReadValue<Vector2>();
      //   Debug.Log($"Target Movement [{TargetMovementVector}]");
    }
    void FixedUpdate()
    {
        CurrentMovementVector = Vector2.Lerp(CurrentMovementVector, TargetMovementVector, 0.5f);
        transform.position += new Vector3(CurrentMovementVector.x, CurrentMovementVector.y);
        
    }

}
