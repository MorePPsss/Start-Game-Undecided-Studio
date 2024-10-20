using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearPowerSource : MonoBehaviour
{
    public float linearSpeed = 10;
    private float recordSpeed;
    public float torque;
    private void Start()
    {
        GetComponent<Gear>().UpdateLinearSpeed(linearSpeed, null);
        recordSpeed = linearSpeed;
    }
    private void Update()
    {
        if (GetComponent<Gear>().linSpeed != linearSpeed)
        {
            GetComponent<Gear>().UpdateLinearSpeed(linearSpeed, null);
            recordSpeed = linearSpeed;
        }
    }
}
