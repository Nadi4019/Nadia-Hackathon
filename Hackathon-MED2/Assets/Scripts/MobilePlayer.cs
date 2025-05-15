using UnityEngine;
using UnityEngine.InputSystem;

public class MobilePlayer : BasePlayer
{
    public VirtualJoystick joystickMove;
    public VirtualJoystick joystickLook;

    [SerializeField] private bool usingGyro;
    private UnityEngine.Gyroscope gyro;
    private Quaternion rotationFix;
    private Quaternion baseRotation;

    private void Start()
    {

        if (SystemInfo.supportsGyroscope && usingGyro)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            // Adjustment for device orientation and right-to-left hand conversion
            rotationFix = new Quaternion(0, 0, 0, 1);
            baseRotation = Quaternion.Euler(90f, 0f, 0f); // Fix for device alignment
        }

    }

    protected override void HandleMovement()
    {
        Vector2 input = joystickMove.Direction;
        Vector3 move = cameraTransform.forward * input.y + cameraTransform.right * input.x;
        move.y = 0;
        controller.SimpleMove(move.normalized * moveSpeed);
    }

    protected override void HandleLook()
    {
        if (usingGyro)
        {
            // Convert the gyroscope attitude to Unity's coordinate system
            Quaternion deviceRotation = gyro.attitude * rotationFix;

            // Compensate for device orientation
            Quaternion adjustedRotation = baseRotation * new Quaternion(-deviceRotation.x, -deviceRotation.y, deviceRotation.z, deviceRotation.w);

            // Apply rotation to the camera's parent to maintain consistency
            cameraTransform.localRotation = adjustedRotation;
        }
        else
        {
            Vector2 input = joystickLook.Direction * sensitivity;

            playerBody.Rotate(Vector3.up * input.x);

            xRotation -= input.y;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }
}