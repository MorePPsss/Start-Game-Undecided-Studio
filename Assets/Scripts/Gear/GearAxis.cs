using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GearAxis : MonoBehaviour
{
    public float velAngular = 0; //velAngular = linearSpeed / numGearTeeth;
    public Gear Powergear = null;
    public List<Gear> sharedAxisGears = new List<Gear>();
    private void Update()
    {
        if (Powergear != null)
        {
            if (Powergear.linSpeed == 0)    //gear still on axis and power cut off
            {
                UpdateGearsSpeed();
            }
        }
        UpdateGearsSpeed();
        if(velAngular != 0)                 //axis rotation
        {
            float speed = 360 * Time.deltaTime * velAngular;
            transform.rotation *= Quaternion.Euler(0f, speed, 0f);
        }
    }
    public void RemoveGearFromAxis(Gear removedGear)
    {
        if(removedGear == Powergear)
        {
            velAngular = 0;
            UpdateGearsSpeed();
            Powergear = null;
        }
        sharedAxisGears.Remove(removedGear);
        removedGear.axis = null;
    }
    public void AddGearToAxis(Gear addedGear)
    {
        sharedAxisGears.Add(addedGear);
        UpdateGearsSpeed(addedGear);
        addedGear.axis = this;
    }
    public void AddGearToAxis(Gear addedGear, int index)
    {
        sharedAxisGears.Insert(index, addedGear);
        UpdateGearsSpeed(addedGear);
        addedGear.axis = this;
    }
    //update all gears on the axis
    public void UpdateGearsSpeed()
    {
        if(Powergear != null)
        {
            velAngular = Powergear.linSpeed / Powergear.numGearTeeth;
        }
        foreach (Gear gear in sharedAxisGears)
        {
            if (Powergear == null) { break; }
            if (gear == Powergear) { continue; }
            float speed = velAngular * gear.numGearTeeth;
            gear.UpdateLinearSpeed(speed, null);
        }
        //if (Powergear == null)
        //{
        //    foreach (Gear gear in sharedAxisGears)
        //    {
        //        if (gear.linSpeed != 0)
        //        {
        //            Powergear = gear;
        //            UpdateGearsSpeed();
        //            velAngular = gear.linSpeed / gear.numGearTeeth;
        //        }
        //    }
        //}
    }
    //update speed when new gear is added 
    private void UpdateGearsSpeed(Gear addedGear)
    {
        addedGear.UpdateLinearSpeed(velAngular * addedGear.numGearTeeth, null);
    }
}
