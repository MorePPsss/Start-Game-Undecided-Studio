using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasControl : MonoBehaviour
{
    public static CamerasControl Instance { get; private set; }  // �������ʵ��

    public Camera[] cameras;
    private int currentCameraIndex = 0;
    private Camera currentCamera;  // ��ǰ����������

    void Awake()
    {
        // ȷ��ֻ��һ��ʵ������
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
        // ���õ�ǰ�����
        cameras[currentCameraIndex].gameObject.SetActive(false);

        // �������������
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

        // �����µ������
        cameras[currentCameraIndex].gameObject.SetActive(true);

        // ���µ�ǰ���������
        currentCamera = cameras[currentCameraIndex];
    }

    // �ṩ��ȡ��ǰ������Ľӿ�
    public Camera GetCurrentCamera()
    {
        return currentCamera;
    }
}
