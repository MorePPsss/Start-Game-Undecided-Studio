using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using static UnityEngine.Rendering.DebugUI;

using TMPro;

public class PlayerWASDMovement : MonoBehaviour
{
    [Header("PlayerSpeed")]
    public float moveSpeed = 5;
    public float rotateSpeed = 1;
    public bool canTurn = true;
    public Vector3 holdDir = new Vector3(1, 0, 0);
    public Vector3 moveDir
    {
        get { return Velocity; }
    }

    [SerializeField]
    private Animator animator;
    private Rigidbody rb;
    private Vector3 previousPos;
    private Vector3 Velocity;
    private Vector3 inputMoveDir = Vector3.zero;
    //private PlayerInput playerInput;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        
        inputMoveDir = new Vector3(Input.GetAxis("Horizontal"), -0.0f, Input.GetAxis("Vertical")).normalized;
        Quaternion faceTargDir = Quaternion.FromToRotation(transform.forward, inputMoveDir) * transform.rotation;
        
        if (!canTurn)               // calling when robot hold handle
        {
            faceTargDir = Quaternion.FromToRotation(transform.forward, holdDir) * transform.rotation;
        }
        if (faceTargDir.x != 0)      //-----Avoid 180 degree rolling 
        {
            faceTargDir.y = faceTargDir.x;
            faceTargDir.w = faceTargDir.z;
            faceTargDir.x = 0;
            faceTargDir.z = 0;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, faceTargDir, Math.Min(0.9f, rotateSpeed * Time.deltaTime));

    }

    private void FixedUpdate()
    {
        Move();
        Velocity = (rb.position - previousPos) / Time.fixedDeltaTime;
        previousPos = rb.position;
    }
    private void Move()
    {
        Vector3 moveDir = inputMoveDir;
        moveDir.y = 0.0f;
        rb.MovePosition(rb.position + moveDir * moveSpeed * Time.deltaTime);
    }
}