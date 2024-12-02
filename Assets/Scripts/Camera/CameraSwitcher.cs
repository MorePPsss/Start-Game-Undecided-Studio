using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera[] cameras; // 相机数组
    private int currentCameraIndex = 0;        // 当前激活相机的索引
    public bool isRobotModeActive = false;    // 是否进入伴随机器人视角

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
}
