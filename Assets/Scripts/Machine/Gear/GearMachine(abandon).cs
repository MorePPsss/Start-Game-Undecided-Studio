using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
//-----------------------------------abandon--------------------------------
public class GearMachineControl : MonoBehaviour
{
/*    public Gear blockGear;
    public Gear powerSauseGear;
    public Gear detectGear;
    public GameObject machine;
    public GameObject switchGear;
    public GameObject handGear;
    public GameObject handGearSwitch;

    private Gear pickedGear;
    private Vector3 pickedGearPosition;
    private float pickingCoolDown = 0.8f;
    private void Update()
    {
        //Debug.Log(FindBlockGear(null, powerSauseGear));
        if (FindBlockGear(null, powerSauseGear))
        {
            powerSauseGear.GetComponent<GearPowerSource>().linearSpeed = 0.1f;
        }
        else
        {
            powerSauseGear.GetComponent<GearPowerSource>().linearSpeed = 10;
        }
        if(pickingCoolDown >= 0) { pickingCoolDown -= Time.deltaTime; }
        
        if(detectGear.linSpeed > 4 && machine.transform.position.x < 4)
        {
            machine.transform.position += new Vector3(Time.deltaTime, 0, 0);
        }
    }
    public bool FindBlockGear(Gear startGear, Gear targetGear)
    {
        bool found = false;
        foreach(Gear gear in targetGear.conGears)
        {
            if(gear == blockGear) { return true; } 
            if(gear == startGear) { continue; }
            if(gear.conGears.Count > 0)
            {
                found = found || FindBlockGear(targetGear, gear);
            }
            if(gear.axis != null)
            {
                foreach(Gear axisgear in gear.axis.sharedAxisGears)
                {
                    if(axisgear == gear) { continue; }
                    if(axisgear == blockGear) { return true;}
                    found = found || FindBlockGear(null, axisgear);
                }
            }
        }
        return found;
    }
    //Pick up gears, switch with the gears behind and place gear
    public int PickUpGear(Transform gearTransform)
    {
        if(pickingCoolDown > 0)
        {
            return 0;
        }
        pickingCoolDown = 0.8f;
        GameObject player = GameObject.Find("Player");
        if(pickedGear != null)
        {
            if (gearTransform.parent.name == "SwitchGear" && (player.transform.position - gearTransform.position).magnitude < 1)
            {
                pickedGear.transform.position = gearTransform.position;
                pickedGear.transform.parent = switchGear.transform;
                if(pickedGear.axis != null)
                {
                    CheckGearAxis(gearTransform.GetComponent<Gear>());
                }
                gearTransform.parent = machine.transform;
                gearTransform.position = new Vector3(0, -10, 0);
                pickedGear = gearTransform.GetComponent<Gear>();
            }
            else if ((pickedGearPosition - gearTransform.position).magnitude < 1.5f)
            {
                pickedGear.transform.position = pickedGearPosition;
                pickedGear = null;
            }
        }
        else if((player.transform.position - gearTransform.position).magnitude < 1.8f)
        {
            if(gearTransform.parent.name != "SwitchGear") 
            {
                pickedGear = gearTransform.GetComponent<Gear>();
                pickedGearPosition = gearTransform.position;
                gearTransform.position = new Vector3(0, -10, 0);
            }
        }

        //take gear on hand
        if (pickedGear != null)
        {
            float radius = pickedGear.radius;
            if (pickedGear.transform.name != "SwitchGear")
            {
                handGear.transform.localScale = new Vector3(2 * radius, 0.05f, 2 * radius);
                handGear.transform.parent = player.transform;
                handGear.transform.localPosition = new Vector3(1f, 0.5f, 0);
            }
            else
            {
                handGear.transform.parent = null;
                handGear.transform.position = new Vector3(0.1f, -10f, 0);
                handGearSwitch.transform.localScale = new Vector3(2 * radius, 0.05f, 2 * radius);
                handGearSwitch.transform.parent = player.transform;
                handGearSwitch.transform.localPosition = new Vector3(1f, 0.5f, 0);
            }
        }
        else
        {
            handGear.transform.parent = null;
            handGear.transform.position = new Vector3(0.1f, -10f, 0);
            handGearSwitch.transform.parent = null;
            handGearSwitch.transform.position = new Vector3(0.1f, -10f, 0);
        }
        return 1;
    }
    private void CheckGearAxis(Gear clickGear)
    {
        GearAxis axis = pickedGear.axis;
        axis.RemoveGearFromAxis(pickedGear);
        pickedGear.UpdateLinearSpeed(0, null);
        if (axis.Powergear == pickedGear)
        {
            axis.Powergear = clickGear;
            axis.sharedAxisGears.Add(clickGear);
            clickGear.axis = axis;
        }
        else
        {
            axis.sharedAxisGears.Add(clickGear);
            clickGear.axis = axis;
        }
    }*/
}
