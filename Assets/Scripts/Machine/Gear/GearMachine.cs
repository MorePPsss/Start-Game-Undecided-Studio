using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class GearMachine : MonoBehaviour
{
    public Gear powerGear;
    public Gear blockedGear;
    public Gear[] gears;
    public Transform[] gearLayers;
    public LayerMask layerMask;
    public Camera mainCamera;
    public Vector3 layerDrift;
    public TrainRod trainRod;

    private float layerMoveTime;
    private bool exchangingGear;
    private bool layerMoving;
    private int currentGearLayer;
    private int nextGearLayer;
    private PlayerInput playerInput;
    private GameObject pickedGear;
    private Vector3 targetPosition;
    private Vector3 previousPosition;

    private void Start()
    {
        exchangingGear = false;
        layerMoving = false;
        currentGearLayer = 0;
        pickedGear = null;
        gears = FindObjectsByType<Gear>(FindObjectsSortMode.None);
        UpdateGearSetLinearSpeed();
        InvokeRepeating("UpdateGearSetLinearSpeed", 0.1f, 1f);
        playerInput = new PlayerInput();
        playerInput.Enable();
        playerInput.Player.Inetract.performed += ctx =>
        {
            if(pickedGear != null)
            {
                if (pickedGear.transform.localPosition.y - previousPosition.y < 0.02f)
                {
                    pickedGear.transform.localPosition = previousPosition;
                    pickedGear = null;
                }
                else
                {
                    targetPosition = new Vector3(-4, 2, 0);
                    exchangingGear = true;
                }
            }
        };
        playerInput.UI.Click.performed += ctx =>
        {
            if(pickedGear != null && !exchangingGear)
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
                    PickingGear(hit.collider.gameObject);
                }
            }
        };
    }
    private void Update()
    {
        if(pickedGear != null) 
        {
            pickedGear.transform.localPosition = Vector3.Lerp(pickedGear.transform.localPosition, targetPosition, 2 * Time.deltaTime);
            if (exchangingGear)
            {
                ExchangeGear();
            }
        }
        if (layerMoving)
        {
            MoveGearLayer();
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
        if (!blockedGear.isBlock)
        {
            trainRod.powered = true;
        }
    }
    private void PickingGear(GameObject gearObj)
    {
        if(gearObj.GetComponent<Gear>() != null)
        {
            gearObj.GetComponent<Gear>().removing = true;
            pickedGear = gearObj;
            previousPosition = pickedGear.transform.localPosition;
            targetPosition = gearObj.transform.localPosition + new Vector3(0 , 0.6f, 0);
        }
    }
    public void LayerChange(float direction)
    {
        int nextCalLayer = (int)(currentGearLayer + direction);
        if (nextCalLayer >= 0 && nextCalLayer < 3 && !layerMoving) 
        {
            layerMoving = true;
            gearLayers[nextCalLayer].localPosition = gearLayers[currentGearLayer].localPosition + direction * layerDrift;
            nextGearLayer = nextCalLayer;
            for(int i = 0; i < gearLayers[currentGearLayer].childCount; i++)
            {
                if(gearLayers[currentGearLayer].GetChild(i).gameObject.name == "UpGear")
                {
                    gearLayers[currentGearLayer].GetChild(i).gameObject.GetComponent<MeshRenderer>().enabled = false;
                }
            }
        }
    }
    private void MoveGearLayer()
    {
        Transform cgl = gearLayers[currentGearLayer];
        Transform ngl = gearLayers[nextGearLayer];
        Vector3 targetPos = layerDrift * (currentGearLayer - nextGearLayer);
        cgl.localPosition = Vector3.Lerp(cgl.localPosition, targetPos, 3 * Time.deltaTime);
        targetPos = Vector3.zero;
        ngl.localPosition = Vector3.Lerp(ngl.localPosition, targetPos, 3 * Time.deltaTime);
        layerMoveTime += Time.deltaTime;
        if(layerMoveTime > 1.5)
        {
            layerMoveTime = 0;
            cgl.localPosition = new Vector3(0, 40, 0);
            ngl.localPosition = Vector3.zero;
            currentGearLayer = nextGearLayer;
            layerMoving = false;

            for (int i = 0; i < gearLayers[currentGearLayer].childCount; i++)
            {
                if (gearLayers[currentGearLayer].GetChild(i).gameObject.name == "UpGear")
                {
                    gearLayers[currentGearLayer].GetChild(i).gameObject.GetComponent<MeshRenderer>().enabled = true;
                }
            }
        }
    }
    private void ExchangeGear()
    {
        if ((pickedGear.transform.localPosition - previousPosition).magnitude < 0.02f)
        {
            pickedGear.transform.localPosition = previousPosition;
            pickedGear.GetComponent<Gear>().removing = false;
            pickedGear.GetComponent<Gear>().isBlock = false;
            pickedGear = null;
            exchangingGear = false;
        }
        else if ((pickedGear.transform.localPosition - targetPosition).magnitude < 0.2f)
        {
            targetPosition = previousPosition;
        }
    }
}
