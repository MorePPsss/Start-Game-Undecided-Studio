using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float maxcameraRotateSpeed = 2;
    public float mincameraRotateSpeed = 0.05f;
    private Camera _camera;
    private CamerasControl camerasControl;
    private void Start()
    {
        camerasControl = GetComponent<CamerasControl>();
        
    }
    private void Update()
    {
        _camera = camerasControl.GetCurrentCamera();
        CameraFollowPlayer();
    }
    private void CameraFollowPlayer()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            Vector3 cameraTargetDirection =  player.transform.position -_camera.transform.position;
            Vector3 cameraCurrentDirection = _camera.transform.forward;
            float cameraRotateSpeed = Mathf.Clamp((cameraTargetDirection - cameraCurrentDirection).magnitude, mincameraRotateSpeed, maxcameraRotateSpeed);
            _camera.transform.forward = (cameraTargetDirection - cameraCurrentDirection).normalized * cameraRotateSpeed * Time.deltaTime + cameraCurrentDirection;
            //_camera.transform.rotation = Quaternion.LookRotation(cameraTargetDirection);
        }
    }
}
    