using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : CoreComponent
{
    public InputActionAsset inputActions;

    private InputAction moveAction;
    public Vector2 move;

    private InputAction throwAction;
    public bool throwHeld;
    public bool throwPressed;
    public bool throwReleased;

    public override void StartComponent()
    {
        moveAction = inputActions.FindAction("move");
        throwAction = inputActions.FindAction("throw");
    }

    public override void UpdateComponent()
    {
        move = moveAction.ReadValue<Vector2>();
        throwPressed = throwAction.IsPressed() && !throwHeld;
        throwReleased = !throwAction.IsPressed() && throwHeld;
        throwHeld = throwAction.IsPressed();
    }
}
