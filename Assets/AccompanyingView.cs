using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccompanyingView : InteractableObject
{
    public CinemachineFreeLook followCamera; // 特写相机
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !haveInteracted) // 确保是玩家进入范围
        {
            Interact();
        }
    }
    protected override void Interact()
    {
        CameraSwitcher switcher = FindObjectOfType<CameraSwitcher>();
        if (switcher != null)
        {
            switcher.ActivateRobotMode(followCamera); // 激活机器人模式
            CineCameraSwitchManager.Instance.SwitchCamera(followCamera);
        }
        Destroy(gameObject);
    }
}
