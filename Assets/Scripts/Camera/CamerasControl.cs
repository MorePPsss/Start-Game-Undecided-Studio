using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This script controls the camera switching logic and is mounted under the CameraSwitcher game object -By Kehao*/
public class CamerasControl : MonoBehaviour
{
    public static CamerasControl Instance { get; private set; }  // Camera singleton mode

    public Camera[] cameras;
    private int currentCameraIndex = 0;
    private Camera currentCamera;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(i == currentCameraIndex);
        }
        currentCamera = cameras[currentCameraIndex];
    }

    void Update()
    {
        if(InputManager.instance != null )//Add one more if judgement to avoid always reporting errors if we don't enter program from Menu Scene -By Kehao
        {
            // Switch camera when the "C" key is pressed
            if (InputManager.instance.cameraSwitchAction.triggered)
            {
                SwitchCamera();
            }
        }
        else
        {
            Debug.Log("Please Check InputManager(A gameObject in Menu scene) is added into this scene! -By Kehao");
        }
    }
    void SwitchCamera()
    {
        // Disable the current camera
        cameras[currentCameraIndex].gameObject.SetActive(false);

        // Update camera index
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

        // Enable new camera
        cameras[currentCameraIndex].gameObject.SetActive(true);

        // Update the current camera reference
        currentCamera = cameras[currentCameraIndex];
    }

    // Provide an interface to obtain the current camera
    public Camera GetCurrentCamera()
    {
        return currentCamera;
    }
}
