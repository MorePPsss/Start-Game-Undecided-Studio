using UnityEngine;
using UnityEngine.UI;
using TMPro; // 如果使用TextMeshPro需要引入

public class PasswordManager : MonoBehaviour
{
    public TMP_Text passwordDisplay; // 用于显示用户输入的密码（或普通 Text）
    public string correctPassword = "1008"; // 正确的密码
    public GameObject door; // 门对象
    public GameObject keyboardUI; // 键盘UI的父对象
    private string currentPassword = ""; // 当前输入的密码
    private Animator doorAnimator; // 门的动画控制器

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
            Debug.Log("密码正确！门已打开。");
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

    private void UpdatePasswordDisplay()
    {
        // 更新密码显示（显示实际输入内容或用 '*' 替代）
        passwordDisplay.text = new string('*', currentPassword.Length);
    }

    private void OpenDoor()
    {
        // 控制门打开（可以替换为你自己的实现逻辑）
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("PushButton"); // 假设动画中有一个名为"Open"的触发器
        }
        else
        {
            Debug.LogWarning("Door Animator not set or missing.");
        }

        // 隐藏键盘UI
        keyboardUI.SetActive(false);
    }
}
