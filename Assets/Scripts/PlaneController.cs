using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
public class PlaneController : MonoBehaviour
{
    public float moveSpeed = 60;
    private float thrustInput;

    public float horizontalSpeed = 30;
    public float verticalSpeed = 15;
    private Vector3 steeringInput;

    public float leanAmount_X = 90;
    public float leanAmount_Y = 30;

    public float steeringSmoothing = 1.5f;
    private Vector3 rawInputSteering;
    private Vector3 smoothInputSteering;

    public float thrustSmoothing = 2;
    private float rawInputThrust;
    private float smoothInputThrust;
    private Rigidbody rb;
    public Transform model;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        InputManager.instance.OnInputSpace += InputSpaceHandler;
        InputManager.instance.OnInputHorizontalOrVertical += InputHorizontalOrVertical;
    }

    private void Update()
    {
        InputSmoothing();
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void OnDisable()
    {
        InputManager.instance.OnInputSpace -= InputSpaceHandler;
        InputManager.instance.OnInputHorizontalOrVertical -= InputHorizontalOrVertical;
    }

    private void OnDestroy()
    {
        InputManager.instance.OnInputSpace -= InputSpaceHandler;
        InputManager.instance.OnInputHorizontalOrVertical -= InputHorizontalOrVertical;
    }

    private void InputHorizontalOrVertical(float arg1, float arg2)
    {
        Vector2 rawInput = new Vector2(arg1, arg2);
        rawInputSteering = new Vector3(rawInput.y, 0, -rawInput.x);
    }

    private void InputSpaceHandler(float space)
    {
        rawInputThrust = space;
    }

    private void InputSmoothing()
    {
        smoothInputSteering = Vector3.Lerp(smoothInputSteering, rawInputSteering, Time.deltaTime * steeringSmoothing);
        steeringInput = smoothInputSteering;

        smoothInputThrust = Mathf.Lerp(smoothInputThrust, rawInputThrust, Time.deltaTime *thrustSmoothing);
        thrustInput = smoothInputThrust;
    }

    #region move
    private void Move()
    {
        rb.velocity = transform.forward * moveSpeed; // set rigidbody speed
    }
    #endregion
    #region Turn
    private void Turn()
    {
        Vector3 newTorque = new Vector3(steeringInput.x * horizontalSpeed, -steeringInput.z * verticalSpeed, 0); //set new Torque
        rb.AddRelativeTorque(newTorque);
        rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.Euler(new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0)), 0.5f);
        TurnModel();
    }

    private void TurnModel()
    {
        model.localEulerAngles = new Vector3(steeringInput.x * leanAmount_Y, model.localEulerAngles.y, steeringInput.z * leanAmount_X);
    }
    #endregion
}

