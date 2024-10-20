using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public float radius = 0.25f;
    public int numGearTeeth = 12;
    private float remRadius = 0.25f;
    private List<Gear> connectGears = new List<Gear>();
    private float torque;
    private float linearSpeed;//齿速：每秒齿数
    private float maxGearRadius = 5;
    private Gear powerbyGear = null;
    public float linSpeed
    {
        get { return linearSpeed; }
    }
    private void Update()
    {
        if(linearSpeed != 0)
        {
            float speed = 360 * Time.deltaTime * linearSpeed / numGearTeeth;
            if(speed == Mathf.Infinity|| numGearTeeth == 0) { speed = 0; }
            transform.rotation *= Quaternion.Euler(0f, speed , 0f);
        }
        NearbyGear();
        if(powerbyGear != null)
        {
            float maxDistance = powerbyGear.radius + radius + 0.15f;
            if ((powerbyGear.transform.position - transform.position).magnitude > maxDistance)
            {
                GearDepart();
            }
            else if (powerbyGear.linSpeed == 0)
            {
                UpdateLinearSpeed(0, null);
            }
        }
        if(radius != remRadius)
        {
            ResizeGear();
            remRadius = radius;
        }
    }
    //根据radius改变齿轮大小
    private void ResizeGear()
    {
        transform.localScale = new Vector3(2 * radius, 0.05f, 2 * radius);
    }
    //链式更新所有（相邻）齿轮线速度，添加齿轮的时候调用。
    public void UpdateLinearSpeed(float speed, Gear powerGear)
    {
        linearSpeed = speed;
        powerbyGear = powerGear;
        for(int i = 0; i < connectGears.Count; i++)
        {
            if (connectGears[i].linSpeed == 0 && speed != 0 && connectGears[i] != powerbyGear)
            {
                connectGears[i].UpdateLinearSpeed(-speed, this);
            }
        }
    }
    //外部添加相邻齿轮数据
    public void AddConnectGear(Gear gearConnectTo)
    {
        bool isConnect = false;
        foreach(Gear connectGear in connectGears)
        {
            if(connectGear == gearConnectTo) { isConnect = true;  break; }
        }
        if(!isConnect)
        { 
            connectGears.Add(gearConnectTo); 
            if(gearConnectTo.linSpeed != 0 && powerbyGear != gearConnectTo)
            {
                UpdateLinearSpeed(-gearConnectTo.linSpeed, gearConnectTo);
            }
        }
    }
    private void NearbyGear()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2 * maxGearRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.tag == Tag.GEAR)
            {
                Gear nearbyGear = collider.gameObject.GetComponent<Gear>();
                if (nearbyGear == this) { break; }
                float minDistance = nearbyGear.radius + radius + 0.05f;
                if((collider.transform.position - transform.position).magnitude < minDistance)
                {
                    collider.transform.position = (collider.transform.position - transform.position).normalized * (minDistance + 0.01f) + transform.position;
                    AddConnectGear(nearbyGear);
                    nearbyGear.AddConnectGear(this);
                }
            }
        }
    }
    public void GearDepart()
    {
        powerbyGear.connectGears.Remove(this);
        connectGears.Remove(powerbyGear);
        UpdateLinearSpeed(0, null);
    }
}
