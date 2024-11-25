using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class JetpackagePhysic : MonoBehaviour
{
    public float jetpackWorkingTime = 0.0f;
    public Vector3 force;
    public Vector3 Position;
    public ForceMode forceMode = ForceMode.Force;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Position = rb.centerOfMass;
    }

    void FixedUpdate()
    {
        if(jetpackWorkingTime > 0)
        {
            Vector3 position = transform.GetChild(0).position;
            rb.AddForceAtPosition(force, position, forceMode);
            jetpackWorkingTime -= Time.deltaTime;
        }
        else
        {
            rb.AddForceAtPosition(Vector3.zero, Position, forceMode);
        }
    }
    private void OnMouseDown()
    {
        jetpackWorkingTime = 2;
    }
}
