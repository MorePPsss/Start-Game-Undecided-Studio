using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1GearSolve : MonoBehaviour
{
    public GameObject powerGear;
    public GameObject handle;
    public GameObject EventArea;
    public GameObject sparks;
    private HandleObject handleobj;
    private bool solve = false;

    private void Start()
    {
        handleobj = handle.GetComponent<HandleObject>();
        GameObject[] gears = GameObject.FindGameObjectsWithTag("Gear");
        foreach (GameObject gear in gears)
        {
            if (gear.GetComponent<SimpleGearMovement>() != null)
            {
                gear.GetComponent<SimpleGearMovement>().enabled = false;
            }
        }
    }
    private void Update()
    {
        if(transform.position.x - powerGear.transform.position.x < 2.48 && !solve)
        {
            GearSetSolve();
            solve = true;
        }
        if (solve)
        {
            transform.rotation = Quaternion.Euler(-powerGear.transform.rotation.eulerAngles);
        }
    }
    private void GearSetSolve()
    {
        if(sparks != null) { sparks.SetActive(true); }
        handleobj.HandleInterct();
        handleobj.canInteract = false;
        EventArea.SetActive(false);
        transform.rotation = powerGear.transform.rotation;
        enableGearRotate();
    }
    private void enableGearRotate()
    {
        GameObject[] gears = GameObject.FindGameObjectsWithTag("Gear");
        foreach (GameObject gear in gears)
        {
            if(gear.GetComponent<SimpleGearMovement>()!= null)
            {
                gear.GetComponent<SimpleGearMovement>().enabled = true;
            }
        }
    }
}
