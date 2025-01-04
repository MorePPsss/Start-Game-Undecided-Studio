using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GearMachine : MonoBehaviour
{
    public Gear powerGear;
    public Gear[] gears;
    public Transform[] gearLayers;
    public LayerMask layerMask;
    public Camera mainCamera;

    private PlayerInput playerInput;
    private GameObject pickedGear;
    private Vector3 targetPosition;
    private Vector3 previousPosition;

    private void Start()
    {
        pickedGear = null;
        gears = FindObjectsByType<Gear>(FindObjectsSortMode.None);
        UpdateGearSetLinearSpeed();
        InvokeRepeating("UpdateGearSetLinearSpeed", 0.1f, 1f);
        playerInput = new PlayerInput();
        playerInput.Enable();
        playerInput.UI.Click.performed += ctx =>
        {
            if(pickedGear != null)
            {
                pickedGear.GetComponent<Gear>().removing = false;
                targetPosition = previousPosition;
                if (pickedGear.transform.localPosition.y - previousPosition.y < 0.02f)
                {
                    pickedGear.transform.localPosition = previousPosition;
                    pickedGear = null;
                }
            }
            else
            {
                Vector2 mousePosition = Mouse.current.position.ReadValue();
                Ray ray = mainCamera.ScreenPointToRay(mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
                {
                    RaiseGear(hit.collider.gameObject);
                }
            }
        };
    }
    private void Update()
    {
        if(pickedGear != null) 
        {
            pickedGear.transform.localPosition = Vector3.Lerp(pickedGear.transform.localPosition, targetPosition, 2 * Time.deltaTime);
        }
    }
    public void UpdateGearSetLinearSpeed()
    {
        foreach (Gear gear in gears)
        {
            gear.linearSpeed = 0;
        }
        powerGear.linearSpeed = 4;
        if (!powerGear.UpdateGearLinearSpeed())
        {
            foreach (Gear gear in gears)
            {
                gear.linearSpeed = 0;
            }
        }
    }
    private void RaiseGear(GameObject gearObj)
    {
        if(gearObj.GetComponent<Gear>() != null)
        {
            gearObj.GetComponent<Gear>().removing = true;
            pickedGear = gearObj;
            previousPosition = pickedGear.transform.localPosition;
            targetPosition = gearObj.transform.localPosition + new Vector3(0 , 0.6f, 0);
        }
    }
}
