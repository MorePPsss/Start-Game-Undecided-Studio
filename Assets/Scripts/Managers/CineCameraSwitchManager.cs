using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CineCameraSwitchManager : Singleton<CineCameraSwitchManager>
{
    private CinemachineVirtualCamera activeCamera;// ��ǰ������
                                                  
    public void SwitchCamera(CinemachineVirtualCamera newCamera)
    {
        if (activeCamera != null)
        {
            activeCamera.Priority = 0;
        }

        activeCamera = newCamera;
        activeCamera.Priority = 10;
    }

    /*�ر�������������ص�Ĭ���������*/
    public void ResetCamera()
    {
        if (activeCamera != null)
        {
            activeCamera.Priority = 0;
            activeCamera = null;
        }
    }
}
