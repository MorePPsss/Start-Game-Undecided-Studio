using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float maxcameraRotateSpeed = 2;
    public float mincameraRotateSpeed = 0.02f;
    private Camera _camera;
    private CamerasControl camerasControl;
    private bool followOn = false;
    private float cameraRotateSpeed;
    private float cameraRotateAcc;
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
        if (player != null && !followOn)
        {
            Vector3 cameraTargetDirection = (player.transform.position - _camera.transform.position).normalized;
            if((cameraTargetDirection - _camera.transform.forward).magnitude > 0.2)
            {
                followOn = true;
            }
        }
        else if(player != null && followOn) 
        {
            Vector3 cameraTargetDirection =  (player.transform.position -_camera.transform.position).normalized;
            Vector3 cameraCurrentDirection = _camera.transform.forward;
            cameraRotateSpeed = Mathf.Clamp((cameraTargetDirection - cameraCurrentDirection).magnitude * 2f, mincameraRotateSpeed, maxcameraRotateSpeed);
            _camera.transform.forward = (cameraTargetDirection - cameraCurrentDirection).normalized * cameraRotateSpeed * Time.deltaTime + cameraCurrentDirection;
            if((cameraTargetDirection - cameraCurrentDirection).magnitude < 0.0004f)
            {
                _camera.transform.forward = cameraTargetDirection;
                followOn = false;
            }
        }
    }
}
    