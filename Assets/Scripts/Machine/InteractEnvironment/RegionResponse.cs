using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RegionResponse : MonoBehaviour
{
    public string tipContent;
    public bool ifPopTip;
    public bool ifSwitchCamera;
    public CameraSwitcher cameraSwitcher;
    public Cinemachine.CinemachineVirtualCamera newVirtualCamera;
    public bool addCameraFlag = false;

    public void OnTriggerEnter(Collider other)
    {
        PopSwitch();
    }
    public void PopSwitch()
    {
        if (ifPopTip)
        {
            Debug.Log("µ¯´°");
            UIManager.Instance.ShowTipUI(tipContent);
        }
        if (ifSwitchCamera)
        {
            Debug.Log("ÇÐ»»Ïà»ú");
            if (cameraSwitcher != null && newVirtualCamera != null && !addCameraFlag)
            {
                cameraSwitcher.AddVirtualCamera(newVirtualCamera);
                addCameraFlag = true;
            }
        }
    }

}
