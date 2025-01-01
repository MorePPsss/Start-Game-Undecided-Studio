using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AccompanyingView : InteractableObject
{
    public CinemachineFreeLook followCamera; // 特写相机
    public GameObject door; // 门对象
    private Animator doorAnimator; // 门的动画控制器
    void Start()
    {
        if (door != null)
        {
            doorAnimator = door.GetComponent<Animator>();
        }
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
            CineCameraSwitchManager.Instance.SwitchCamera(followCamera);
            OpenDoor();
        }
        Destroy(gameObject);
    }
    private void OpenDoor()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("PushButton");
        }
        else
        {
            Debug.LogWarning("Door Animator not set or missing.");
        }
    }
}
