using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera[] cameras; // �������
    private int currentCameraIndex = 0;        // ��ǰ�������������

    void Start()
    {
        // ȷ��ֻ�е�һ��������ã��������
        ActivateCamera(currentCameraIndex);
    }

    void Update()
    {
        // ���� C ��ʱ�л����
        if (Input.GetKeyDown(KeyCode.C))
        {
            // �л�����һ�����
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;
            ActivateCamera(currentCameraIndex);
        }
    }

    // ����ָ������������������
    void ActivateCamera(int index)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].Priority = (i == index) ? 10 : 0; // ��ǰ������ȼ�����Ϊ�ߣ��������Ϊ��
        }
    }
}
