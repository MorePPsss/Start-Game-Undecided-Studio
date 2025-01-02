using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AccompanyingView : InteractableObject
{
    public CinemachineFreeLook followCamera; // 特写相机
    public GameObject[] doorsToDestroy;
    void Start()
    {
        
    }
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
            GameManager.ifEnterAccompanyingMode = true;
            CineCameraSwitchManager.Instance.SwitchCamera(followCamera);
            UIManager.Instance.ShowTipUI("Now it's in companion robot mode, trying to control the camera to turn left and right using the 'a' and'd 'keys, and adjusting the angle of view using the mouse wheel. (It seems that you already have permission to open the portal)");
            OpenDoor();
        }
        Destroy(gameObject);
    }
    private void OpenDoor()
    {
        for (int i = 0; i < doorsToDestroy.Length; i++)
        {
            if (doorsToDestroy[i] != null)
            {
                Destroy(doorsToDestroy[i]);
            }
        }
    }
}
