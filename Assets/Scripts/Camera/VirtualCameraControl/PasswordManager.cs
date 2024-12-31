using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
using System.Collections.Generic; // 如果使用TextMeshPro需要引入
using System.Collections;

public class PasswordManager : MonoBehaviour
{
    public TMP_Text passwordDisplay; // 用于显示用户输入的密码（或普通 Text）
    public string correctPassword = "1008"; // 正确的密码
    public GameObject door; // 门对象
    public GameObject keyboardUI; // 键盘UI的父对象
    private string currentPassword = ""; // 当前输入的密码
    private Animator doorAnimator; // 门的动画控制器
    public GameObject wallHasKeyboard;
    public CinemachineVirtualCamera defaultCamera; // 默认场景相机
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
        // 更新当前密码
        if (currentPassword.Length < 10) // 限制输入长度，避免超出范围
        {
            currentPassword += number;
            UpdatePasswordDisplay();
        }
    }

    public void OnClearButtonClick()
    {
        // 清空当前密码
        currentPassword = "";
        UpdatePasswordDisplay();
    }

    public void OnConfirmButtonClick()
    {
        // 验证密码
        if (currentPassword == correctPassword)
        {
            InteractableObject interactableObject = wallHasKeyboard.GetComponent<InteractableObject>();
            interactableObject.haveInteracted = true;
            Debug.Log("密码正确！门已打开。");
            OnCloseButton();
            OpenDoor();
        }
        else
        {
            Debug.Log("密码错误！");
        }
        // 清空输入
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
