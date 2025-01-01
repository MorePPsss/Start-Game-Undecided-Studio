using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AccompanyingView : InteractableObject
{
    public CinemachineFreeLook followCamera; // ��д���
    public GameObject door; // �Ŷ���
    private Animator doorAnimator; // �ŵĶ���������
    void Start()
    {
        if (door != null)
        {
            doorAnimator = door.GetComponent<Animator>();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !haveInteracted) // ȷ������ҽ��뷶Χ
        {
            Interact();
        }
    }
    protected override void Interact()
    {
        CameraSwitcher switcher = FindObjectOfType<CameraSwitcher>();
        if (switcher != null)
        {
            switcher.ActivateRobotMode(followCamera); // ���������ģʽ
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
