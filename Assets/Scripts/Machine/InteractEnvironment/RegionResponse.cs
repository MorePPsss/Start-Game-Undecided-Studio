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
    public bool firstArrive = false;

    public void OnTriggerEnter(Collider other)
    {
        PopSwitch();
    }
    public void PopSwitch()
    {
        if (ifPopTip)
        {
            if (!firstArrive)
            {
                Debug.Log("PopWindow");
                UIManager.Instance.ShowTipUI(tipContent);
                firstArrive = true;
            }
        }
        if (ifSwitchCamera)
        { 
            if (cameraSwitcher != null && newVirtualCamera != null && !addCameraFlag)
            {
                Debug.Log("SwitchCamera");
                cameraSwitcher.AddVirtualCamera(newVirtualCamera);
                addCameraFlag = true;
            }
        }
    }

}
