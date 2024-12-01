using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraCloseUp : InteractableObject
{
    public CinemachineVirtualCamera closeUpCamera; // ��д���
    public GameObject keyboardUI; // ����UI
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !haveInteracted) // ȷ������ҽ��뷶Χ
        {
            Interact();
        }
    }
    protected override void Interact()
    {
        // �л�����д���
        CineCameraSwitchManager.Instance.SwitchCamera(closeUpCamera);
        // ��ʾ����UI
        if (keyboardUI != null)
        {
            keyboardUI.SetActive(true);
        }
    }
}
