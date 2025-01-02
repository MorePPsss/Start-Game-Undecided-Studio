using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Searcher.Searcher.AnalyticsEvent;

public class DetectPlayerEnter : MonoBehaviour
{
    public Canvas canvas;
    public GameObject handle;
    public enum EventList
    {
        CameraUpdate,
        EInteractiveObject,
        SpaceInteractiveObject,
        MouseInteractiveObject
    }
    public bool Setzone;

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
                CameraSetPos(Setzone);
                break;
            case EventList.SpaceInteractiveObject:
                
                break;
            case EventList.EInteractiveObject:
                EInteractEvent(true);
                break;
            case EventList.MouseInteractiveObject:
                MouseInteractiveObject(true);
                break;
        }
    }
    private void exitEvent()
    {
        switch (eventlist)
        {
            case EventList.CameraUpdate:
                CameraSetPos(Setzone);
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
            canvas.gameObject.SetActive(inside);
        }
        else
        {
            Debug.Log("Canvas is not assigned.");
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
}
