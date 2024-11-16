using UnityEngine;
using UnityEngine.UI;
using TMPro; // ���ʹ��TextMeshPro��Ҫ����

public class PasswordManager : MonoBehaviour
{
    public TMP_Text passwordDisplay; // ������ʾ�û���������루����ͨ Text��
    public string correctPassword = "1008"; // ��ȷ������
    public GameObject door; // �Ŷ���
    public GameObject keyboardUI; // ����UI�ĸ�����
    private string currentPassword = ""; // ��ǰ���������
    private Animator doorAnimator; // �ŵĶ���������

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
            Debug.Log("������ȷ�����Ѵ򿪡�");
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

    private void UpdatePasswordDisplay()
    {
        // ����������ʾ����ʾʵ���������ݻ��� '*' �����
        passwordDisplay.text = new string('*', currentPassword.Length);
    }

    private void OpenDoor()
    {
        // �����Ŵ򿪣������滻Ϊ���Լ���ʵ���߼���
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("PushButton"); // ���趯������һ����Ϊ"Open"�Ĵ�����
        }
        else
        {
            Debug.LogWarning("Door Animator not set or missing.");
        }

        // ���ؼ���UI
        keyboardUI.SetActive(false);
    }
}
