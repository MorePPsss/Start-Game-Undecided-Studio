using UnityEngine;
using Cinemachine;


public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera[] cameras; // 相机数组
    private int currentCameraIndex = 0;        // 当前激活相机的索引
    public bool isRobotModeActive = false;    // 是否进入伴随机器人视角
    public int CameraIndex { get; private set; }

    void Start()
    {
        // 确保只有第一个相机启用，其余禁用
        ActivateCamera(currentCameraIndex);
    }

    void Update()
    {
        if (!isRobotModeActive) // 如果未进入机器人模式，允许切换相机
        {
            // 按下 C 键时切换相机
            if (Input.GetKeyDown(KeyCode.C))
            {
                // 切换到下一个相机
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
                    // 如果减过头了，就回到最后一个相机
                    currentCameraIndex = cameras.Length - 1;
                    CameraIndex = currentCameraIndex;
                }
                ActivateCamera(currentCameraIndex);
            }
        }
    }

    // 激活指定相机并禁用其他相机
    void ActivateCamera(int index)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].Priority = (i == index) ? 10 : 0; // 当前相机优先级设置为高，其他相机为低
        }
    }
    void ActivateCamera(CinemachineVirtualCamera targetCamera)
    {
        foreach (var camera in cameras)
        {
            camera.Priority = (camera == targetCamera) ? 10 : 0; // 切换到目标相机
        }
    }
    void ActivateCamera(CinemachineFreeLook targetCamera)
    {
        foreach (var camera in cameras)
        {
            camera.Priority = (camera == targetCamera) ? 10 : 0; // 切换到目标相机
        }
    }

    public void ActivateRobotMode(CinemachineVirtualCamera robotCamera)
    {
        isRobotModeActive = true; // 设置为机器人模式
        ActivateCamera(robotCamera); // 切换到伴随机器人相机
    }

    public void ActivateRobotMode(CinemachineFreeLook robotCamera)
    {
        isRobotModeActive = true; // 设置为机器人模式
        ActivateCamera(robotCamera); // 切换到伴随机器人相机
    }

    public int GetCurrentCameraIndex()
    {
        return CameraIndex;
    }

    public void AddVirtualCamera(CinemachineVirtualCamera newCam)
    {
        // 1. 新建一个长度为 cameras.Length + 1 的临时数组
        CinemachineVirtualCamera[] newArray = new CinemachineVirtualCamera[cameras.Length + 1];

        // 2. 拷贝原有相机到新数组
        for (int i = 0; i < cameras.Length; i++)
        {
            newArray[i] = cameras[i];
        }

        // 3. 在最后一个位置放置新的相机
        newArray[cameras.Length] = newCam;

        // 4. 将临时数组赋回 cameras
        cameras = newArray;

        // 5. 切换到新添加的相机
        ActivateCamera(cameras.Length - 1);
        CameraIndex = cameras.Length - 1;
        Debug.Log("加入新的相机！" + (cameras.Length - 1));
    }
}
