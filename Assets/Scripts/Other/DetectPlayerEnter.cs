using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Searcher.Searcher.AnalyticsEvent;
//for all requirement to detect player enter certain area cause event
//use eventlist to change event mode
public class DetectPlayerEnter : MonoBehaviour
{
    public Canvas canvas;
    public GameObject handle;
    public GameObject gearMachine;
    public Vector3 presetPos;
    public enum EventList
    {
        CameraUpdate,
        EInteractiveObject,
        SpaceInteractiveObject,
        MouseInteractiveObject,
        EnterInteractive,
    }
    public bool centerZone;

    [SerializeField]
    private EventList eventlist;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            enterEvent();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            exitEvent();
        }
    }
    private void enterEvent()
    {
        switch (eventlist)
        {
            case EventList.CameraUpdate:
                CameraSetPos(centerZone);
                break;
            case EventList.SpaceInteractiveObject:
                
                break;
            case EventList.EInteractiveObject:
                EInteractEvent(true);
                break;
            case EventList.MouseInteractiveObject:
                MouseInteractiveObject(true);
                break;
            case EventList.EnterInteractive:
                EnterInteractive();
                break;
        }
    }
    private void exitEvent()
    {
        switch (eventlist)
        {
            case EventList.CameraUpdate:
                CameraSetPos(centerZone);
                break;
            case EventList.SpaceInteractiveObject:

                break;
            case EventList.EInteractiveObject:
                EInteractEvent(false);
                break;
            case EventList.MouseInteractiveObject:
                MouseInteractiveObject(false);
                break;
        }
    }

    private void CameraSetPos(bool Inside)
    {
        Camera camera = GameObject.Find("Camera").GetComponent<Camera>();
        camera.GetComponent<CameraFlatMove>().inZone = Inside;
        camera.GetComponent<CameraFlatMove>().presetPos = presetPos;
    }
    private void CameraFollowPlayer(bool follow)
    {
        Camera camera = GameObject.Find("Camera").GetComponent<Camera>();
        camera.GetComponent<CameraFlatMove>().follow = follow;
    }
    public void EInteractEvent(bool inside)
    {
        if (canvas != null)
        {
            //let canvas disappear(by using animation to set transparent)
            canvas.gameObject.transform.GetChild(0).gameObject.SetActive(inside);
            canvas.gameObject.transform.GetChild(1).gameObject.SetActive(!inside);
        }
        else
        {
            Debug.Log("Canvas is not assigned.");
        }
        if(gearMachine != null)
        {
            gearMachine.GetComponent<GearMachineEInteract>().canInteract = true;
        }
        if(handle != null)
        {
            handle.GetComponent<HandleObject>().canInteract = inside;
        }
    }
    public void MouseInteractiveObject(bool inside)
    {
        if (handle.GetComponent<Rod>().powered)
        {
            canvas.gameObject.SetActive(inside);
            handle.GetComponent<Rod>().interactable = inside;
        }
    }
    public void EnterInteractive()
    {
        if (canvas != null)
        {
            //word panel UI
            canvas.gameObject.SetActive(true);
        }
    }
}
