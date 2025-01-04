using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public bool autoConnect = false;
    public bool isBlock = false;
    public bool clockwise;
    public bool removing = false;

    public float radius = 0.25f;
    public int numGearTeeth = 12;
    public float linearSpeed;              //Gear teeth speed: number of teeth per second\

    //public GearAxis axis = null;
    public bool isPowerGear;
    public Vector3 axisDir;
    [SerializeField]
    private List<Gear> connectGears = new List<Gear>();
    [SerializeField]
    private List<Gear> axisConnectGears = new List<Gear>();

    private float remRadius = 0.25f;
    private float torque;
    private float maxGearRadius = 5;
    private Gear powerbyGear = null;
    public float linSpeed
    {
        get { return linearSpeed; }
    }
    public List<Gear> conGears
    {
        get { return connectGears; }
    }
    private void Update()
    {
        if (linearSpeed != 0)
        {
            float speed = 360 * Time.deltaTime * linearSpeed / numGearTeeth;
            if(clockwise) { speed *= -1; }
            if(speed == Mathf.Infinity|| numGearTeeth == 0) { speed = 0; }
            transform.rotation *= Quaternion.Euler(axisDir * speed);
        }
        if (autoConnect)
        {
            NearbyGear();
            if(powerbyGear != null)
            {
                float maxDistance = powerbyGear.radius + radius + 0.15f;
                if (GearFlatVector(powerbyGear.transform, transform).magnitude > maxDistance || Mathf.Abs(powerbyGear.transform.position.y - transform.position.y) > 0.13f)
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
    }
    //change gear scale according to radius(abandon)
    private void ResizeGear()
    {
        transform.localScale = new Vector3(2 * radius, 0.05f, 2 * radius);
    }
    //UpdateLinearSpeed and check block
    public bool UpdateGearLinearSpeed()
    {
        if (isBlock)
        {
            return false;
        }
        foreach (Gear gear in connectGears) 
        {
            if(gear.linearSpeed == linearSpeed && gear.clockwise == !clockwise ){ continue; }
            //else if (gear.linearSpeed != 0) { return false; }
            if (gear.removing) { continue; }
            gear.linearSpeed = linearSpeed;
            gear.clockwise = !clockwise;
            if(!gear.UpdateGearLinearSpeed())
            {
                return false;
            }
        }
        foreach(Gear gear in axisConnectGears)
        {
            gear.linearSpeed = linearSpeed / numGearTeeth * gear.numGearTeeth;
            gear.clockwise = clockwise;
            if (!gear.UpdateGearLinearSpeed())
            {
                return false;
            }
        }
        return true;
    }

    //chain update all nearby gear linearSpeed, called when gears update (abandon)
    public void UpdateLinearSpeed(float speed, Gear powerGear)
    {
        linearSpeed = speed;
        if(speed != 0 && powerbyGear == null) 
        { 
            powerbyGear = powerGear;
        }else if(speed == 0) 
        { 
            powerbyGear = null;
        }
        for(int i = 0; i < connectGears.Count; i++)
        {
            if (connectGears[i].linSpeed != speed && speed != 0 && connectGears[i] != powerbyGear)
            {
                connectGears[i].UpdateLinearSpeed(-speed, this);
            }
        }
    }
    //add nearby gear data
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
            if(gearConnectTo.linSpeed != 0 && powerbyGear != gearConnectTo && !isPowerGear)
            {
                UpdateLinearSpeed(-gearConnectTo.linSpeed, gearConnectTo);
            }
        }
    }
    //gear custom collision and connect nearby gears.
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
                if(GearFlatVector(collider.transform, transform).magnitude <= minDistance && Mathf.Abs(collider.transform.position.y - transform.position.y) < 0.13f)
                {
                    Vector2 gearMove = GearFlatVector(collider.transform, transform).normalized * (minDistance  - GearFlatVector(collider.transform, transform).magnitude);
                    collider.transform.position = collider.transform.position + new Vector3(gearMove.x, 0, gearMove.y);
                    AddConnectGear(nearbyGear);
                    nearbyGear.AddConnectGear(this);
                }
                else if (GearFlatVector(collider.transform, transform).magnitude <= minDistance + 0.05f && Mathf.Abs(collider.transform.position.y - transform.position.y) < 0.13f)
                {
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
    public Vector2 GearFlatVector(Transform gearA, Transform gearB)
    {
        Vector2 gear2DDistance = new Vector2(gearA.position.x - gearB.position.x, gearA.position.z - gearB.position.z);
        return gear2DDistance;
    }
}
