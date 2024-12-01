using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera[] cameras; // �������
    private int currentCameraIndex = 0;        // ��ǰ�������������
    public bool isRobotModeActive = false;    // �Ƿ�������������ӽ�

    void Start()
    {
        // ȷ��ֻ�е�һ��������ã��������
        ActivateCamera(currentCameraIndex);
    }

    void Update()
    {
        if (!isRobotModeActive) // ���δ���������ģʽ�������л����
        {
            // ���� C ��ʱ�л����
            if (Input.GetKeyDown(KeyCode.C))
            {
                // �л�����һ�����
                currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;
                ActivateCamera(currentCameraIndex);
            }
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
    void ActivateCamera(CinemachineVirtualCamera targetCamera)
    {
        foreach (var camera in cameras)
        {
            camera.Priority = (camera == targetCamera) ? 10 : 0; // �л���Ŀ�����
        }
    }
    void ActivateCamera(CinemachineFreeLook targetCamera)
    {
        foreach (var camera in cameras)
        {
            camera.Priority = (camera == targetCamera) ? 10 : 0; // �л���Ŀ�����
        }
    }

    public void ActivateRobotMode(CinemachineVirtualCamera robotCamera)
    {
        isRobotModeActive = true; // ����Ϊ������ģʽ
        ActivateCamera(robotCamera); // �л���������������
    }

    public void ActivateRobotMode(CinemachineFreeLook robotCamera)
    {
        isRobotModeActive = true; // ����Ϊ������ģʽ
        ActivateCamera(robotCamera); // �л���������������
    }
}
