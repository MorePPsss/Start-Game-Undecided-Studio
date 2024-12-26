using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetectPlayerEnter : MonoBehaviour
{
    public bool Setzone;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            CameraSetPos(Setzone);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            CameraSetPos(Setzone);
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
}
