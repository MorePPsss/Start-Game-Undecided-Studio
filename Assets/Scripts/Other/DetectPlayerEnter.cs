using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerEnter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            CameraFollowPlayer(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            CameraFollowPlayer(false);
        }
    }
    private void CameraFollowPlayer(bool follow)
    {
        Camera camera = GameObject.Find("Camera").GetComponent<Camera>();
        camera.GetComponent<CameraFlatMove>().follow = follow;
    }
}
