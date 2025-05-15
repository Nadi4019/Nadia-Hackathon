using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public abstract class BasePlayer : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    protected float currentSpeed;
    protected Vector3 velocity;
    protected Vector2 moveInput;

    [Header("Look Settings")]
    public float sensitivity = 0.5f;
    public bool freeLook = true;

    [Header("References")]
    public CharacterController controller;
    public Transform cameraTransform;
    public Transform playerBody;

    protected float xRotation = 0f;

    protected virtual void Awake()
    {
        controller = GetComponent<CharacterController>();
        currentSpeed = moveSpeed;
    }

    protected virtual void Update()
    {
        if (!freeLook) return;

        HandleMovement();
        HandleLook();
        ApplyGravity();
    }

    protected abstract void HandleMovement();
    protected abstract void HandleLook();
    protected virtual void Jump()
    {
        if (controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    protected virtual void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}