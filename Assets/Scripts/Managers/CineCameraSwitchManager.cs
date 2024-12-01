using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CineCameraSwitchManager : Singleton<CineCameraSwitchManager>
{
    private CinemachineVirtualCamera activeCamera;// 当前活动摄像机
                                                  
    public void SwitchCamera(CinemachineVirtualCamera newCamera)
    {
        if (activeCamera != null)
        {
            activeCamera.Priority = 0;
        }

        activeCamera = newCamera;
        activeCamera.Priority = 10;
    }

    /*关闭所有摄像机（回到默认摄像机）*/
    public void ResetCamera()
    {
        if (activeCamera != null)
        {
            activeCamera.Priority = 0;
            activeCamera = null;
        }
    }
}
