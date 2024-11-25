using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraCloseUp : InteractableObject
{
    public CinemachineVirtualCamera defaultCamera; // Ĭ�ϳ������
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
        defaultCamera.Priority = 0;
        closeUpCamera.Priority = 11;
        // ��ʾ����UI
        keyboardUI.SetActive(true);
    }
}
