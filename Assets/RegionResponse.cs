using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionResponse : MonoBehaviour
{
    public string tipContent;
    public bool ifPopTip;
    public bool ifSwitchCamera;

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
        }
    }
}
