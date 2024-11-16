using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraSwitcher : InteractableObject
{
    public CinemachineVirtualCamera defaultCamera; // 默认场景相机
    public CinemachineVirtualCamera closeUpCamera; // 特写相机
    public GameObject keyboardUI; // 键盘UI
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !haveInteracted) // 确保是玩家进入范围
        {
            Interact();
            haveInteracted = true;
        }
    }
    protected override void Interact()
    {
        // 切换到特写相机
        defaultCamera.Priority = 0;
        closeUpCamera.Priority = 11;
        // 显示键盘UI
        keyboardUI.SetActive(true);
    }
}
