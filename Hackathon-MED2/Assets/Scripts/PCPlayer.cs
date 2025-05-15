using UnityEngine;
using UnityEngine.InputSystem;

public class PCPlayer : BasePlayer
{
    private InputSystem_Actions inputActions;

    protected override void Awake()
    {
        base.Awake();
        inputActions = new InputSystem_Actions();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        inputActions.Player.Look.performed += ctx => Look(ctx.ReadValue<Vector2>());
        inputActions.Player.Jump.performed += _ => Jump();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    protected override void HandleMovement()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * currentSpeed * Time.deltaTime);
    }

    protected override void HandleLook()
    {
        // Look handled separately in callback
    }

    private void Look(Vector2 input)
    {
        if (!freeLook || Time.timeScale == 0f) return;

        float mouseX = input.x * sensitivity;
        float mouseY = input.y * sensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

}