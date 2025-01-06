using UnityEngine;
using Cinemachine;


public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera[] cameras; // �������
    private int currentCameraIndex = 0;        // ��ǰ�������������
    public bool isRobotModeActive = false;    // �Ƿ�������������ӽ�
    public int CameraIndex { get; private set; }

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
                CameraIndex = currentCameraIndex;
                ActivateCamera(currentCameraIndex);
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                currentCameraIndex--;
                CameraIndex = currentCameraIndex;
                if (currentCameraIndex < 0)
                {
                    // �������ͷ�ˣ��ͻص����һ�����
                    currentCameraIndex = cameras.Length - 1;
                    CameraIndex = currentCameraIndex;
                }
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

    public int GetCurrentCameraIndex()
    {
        return CameraIndex;
    }

    public void AddVirtualCamera(CinemachineVirtualCamera newCam)
    {
        // 1. �½�һ������Ϊ cameras.Length + 1 ����ʱ����
        CinemachineVirtualCamera[] newArray = new CinemachineVirtualCamera[cameras.Length + 1];

        // 2. ����ԭ�������������
        for (int i = 0; i < cameras.Length; i++)
        {
            newArray[i] = cameras[i];
        }

        // 3. �����һ��λ�÷����µ����
        newArray[cameras.Length] = newCam;

        // 4. ����ʱ���鸳�� cameras
        cameras = newArray;

        // 5. �л�������ӵ����
        ActivateCamera(cameras.Length - 1);
        CameraIndex = cameras.Length - 1;
        Debug.Log("�����µ������" + (cameras.Length - 1));
    }
}
