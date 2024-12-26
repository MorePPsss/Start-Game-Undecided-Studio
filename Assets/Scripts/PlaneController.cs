using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
public class PlaneController : MonoBehaviour
{
    public float moveSpeed = 0; // Initial speed of the plane
    private float thrustInput; // Input value for thrust (forward/backward movement)

    public float horizontalSpeed = 30; // Speed of horizontal rotation
    public float verticalSpeed = 15; // Speed of vertical rotation
    private Vector3 steeringInput; // Input values for steering (pitch, roll)

    public float leanAmount_X = 90; // Visual tilt amount on the X-axis
    public float leanAmount_Y = 30; // Visual tilt amount on the Y-axis

    public float steeringSmoothing = 1.5f; // Smoothing factor for steering input
    private Vector3 rawInputSteering; // Raw steering input from the player
    private Vector3 smoothInputSteering; // Smoothed steering input

    private Rigidbody rb; // Rigidbody component for physics-based movement
    public Transform model; // Visual representation of the plane

    private void Awake()
    {
        // Initialize the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Subscribe to toggle speed event from InputManager
        if (InputManager.instance != null)
        {
            InputManager.instance.OnToggleSpeed += ToggleSpeed;
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from toggle speed event to prevent memory leaks
        if (InputManager.instance != null)
        {
            InputManager.instance.OnToggleSpeed -= ToggleSpeed;
        }
    }

    private void OnEnable()
    {
        // Subscribe to input events from InputManager
        if (InputManager.instance != null)
        {
            InputManager.instance.OnInputHorizontalOrVertical += HandleSteeringInput;
            InputManager.instance.OnInputSpace += HandleThrustInput;
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from input events
        if (InputManager.instance != null)
        {
            InputManager.instance.OnInputHorizontalOrVertical -= HandleSteeringInput;
            InputManager.instance.OnInputSpace -= HandleThrustInput;
        }
    }

    private void Update()
    {
        // Smooth out input values for better control
        SmoothInputs();
    }

    private void FixedUpdate()
    {
        // Handle movement and turning physics
        Move();
        Turn();
    }

    private void HandleSteeringInput(float horizontal, float vertical)
    {
        // Convert horizontal and vertical inputs to a steering vector
        rawInputSteering = new Vector3(vertical, 0, -horizontal);
    }

    private void HandleThrustInput(float thrust)
    {
        // Assign raw thrust input
        thrustInput = thrust;
    }

    private void SmoothInputs()
    {
        // Smooth the steering input using Lerp
        smoothInputSteering = Vector3.Lerp(smoothInputSteering, rawInputSteering, Time.deltaTime * steeringSmoothing);
        steeringInput = smoothInputSteering;
    }

    private void Move()
    {
        // Apply forward velocity based on thrust input and move speed
        Vector3 moveDirection = transform.forward * moveSpeed;
        rb.velocity = moveDirection;
    }

    private void Turn()
    {
        // Calculate torque based on steering input
        Vector3 torque = new Vector3(steeringInput.x * horizontalSpeed, -steeringInput.z * verticalSpeed, 0);
        rb.AddRelativeTorque(torque);

        // Smoothly correct the plane's roll (Z-axis rotation)
        rb.rotation = Quaternion.Slerp(
            rb.rotation,
            Quaternion.Euler(new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0)),
            Time.fixedDeltaTime * 2 // Adjust this factor for smoother roll correction
        );

        // Adjust the model's tilt for visual feedback
        AdjustModelTilt();
    }

    private void AdjustModelTilt()
    {
        // Tilt the plane model visually based on input
        model.localEulerAngles = new Vector3(
            steeringInput.x * leanAmount_Y,
            model.localEulerAngles.y,
            steeringInput.z * leanAmount_X
        );
    }

    private void ToggleSpeed()
    {
        // Toggle move speed between 10 and 0
        moveSpeed = moveSpeed == 0 ? 10 : 0;
        Debug.Log($"Move Speed Toggled: {moveSpeed}");
    }
}
