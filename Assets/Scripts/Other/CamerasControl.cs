using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasControl : MonoBehaviour
{
    public static CamerasControl Instance { get; private set; }  // 相机单例实例

    public Camera[] cameras;
    private int currentCameraIndex = 0;
    private Camera currentCamera;  // 当前激活的摄像机

    void Awake()
    {
        // 确保只有一个实例存在
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(i == currentCameraIndex);
        }
        currentCamera = cameras[currentCameraIndex];
    }

    // Update is called once per frame
    void Update()
    {
        // Switch camera when the "C" key is pressed
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchCamera();
        }
    }
    void SwitchCamera()
    {
        // 禁用当前摄像机
        cameras[currentCameraIndex].gameObject.SetActive(false);

        // 更新摄像机索引
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

        // 启用新的摄像机
        cameras[currentCameraIndex].gameObject.SetActive(true);

        // 更新当前摄像机引用
        currentCamera = cameras[currentCameraIndex];
    }

    // 提供获取当前摄像机的接口
    public Camera GetCurrentCamera()
    {
        return currentCamera;
    }
}
