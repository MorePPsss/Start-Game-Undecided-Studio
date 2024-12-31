using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
using System.Collections.Generic; // ���ʹ��TextMeshPro��Ҫ����
using System.Collections;

public class PasswordManager : MonoBehaviour
{
    public TMP_Text passwordDisplay; // ������ʾ�û���������루����ͨ Text��
    public string correctPassword = "1008"; // ��ȷ������
    public GameObject door; // �Ŷ���
    public GameObject keyboardUI; // ����UI�ĸ�����
    private string currentPassword = ""; // ��ǰ���������
    private Animator doorAnimator; // �ŵĶ���������
    public GameObject wallHasKeyboard;
    public CinemachineVirtualCamera defaultCamera; // Ĭ�ϳ������
    public CinemachineFreeLook followCamera;

    void Start()
    {
        if (door != null)
        {
            doorAnimator = door.GetComponent<Animator>();
        }
    }

    public void OnNumberButtonClick(string number)
    {
        // ���µ�ǰ����
        if (currentPassword.Length < 10) // �������볤�ȣ����ⳬ����Χ
        {
            currentPassword += number;
            UpdatePasswordDisplay();
        }
    }

    public void OnClearButtonClick()
    {
        // ��յ�ǰ����
        currentPassword = "";
        UpdatePasswordDisplay();
    }

    public void OnConfirmButtonClick()
    {
        // ��֤����
        if (currentPassword == correctPassword)
        {
            InteractableObject interactableObject = wallHasKeyboard.GetComponent<InteractableObject>();
            interactableObject.haveInteracted = true;
            Debug.Log("������ȷ�����Ѵ򿪡�");
            OnCloseButton();
            OpenDoor();
        }
        else
        {
            Debug.Log("�������");
        }
        // �������
        currentPassword = "";
        UpdatePasswordDisplay();
    }

    public void OnCloseButton()
    {
        CameraSwitcher switcher = FindObjectOfType<CameraSwitcher>();
        if(switcher.isRobotModeActive)
        {
            CineCameraSwitchManager.Instance.SwitchCamera(followCamera);
        }
        else
        {
            CineCameraSwitchManager.Instance.SwitchCamera(defaultCamera);
        }
        
        keyboardUI.SetActive(false);
    }

    private void UpdatePasswordDisplay()
    {
        passwordDisplay.text = new string('*', currentPassword.Length);
    }

    private void OpenDoor()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetBool("PwdRight", true);
        }
        else
        {
            Debug.LogWarning("Door Animator not set or missing.");
        }
        keyboardUI.SetActive(false);
    }
}
