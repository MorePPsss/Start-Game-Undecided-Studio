using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraCloseUp : InteractableObject
{
    public CinemachineVirtualCamera closeUpCamera; // 特写相机
    public GameObject keyboardUI; // 键盘UI
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !haveInteracted) // 确保是玩家进入范围
        {
            Interact();
        }
    }
    protected override void Interact()
    {
        // 切换到特写相机
        CineCameraSwitchManager.Instance.SwitchCamera(closeUpCamera);
        // 显示键盘UI
        if (keyboardUI != null)
        {
            keyboardUI.SetActive(true);
        }
    }
}
