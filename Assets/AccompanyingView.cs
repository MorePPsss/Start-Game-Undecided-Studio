using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccompanyingView : InteractableObject
{
    public CinemachineFreeLook followCamera; // ��д���
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
        }
        Destroy(gameObject);
    }
}
