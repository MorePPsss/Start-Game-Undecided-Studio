using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlink : MonoBehaviour
{
    public float maxIntensity = 2f;      // �������
    public float speed = 1f;            // ��˸�ٶ�

    private Light pointLight;

    void Start()
    {
        pointLight = GetComponent<Light>();
    }

    void Update()
    {
        // �� light.intensity �� 0 �� maxIntensity ֮������
        float pingPongValue = Mathf.PingPong(Time.time * speed, maxIntensity);
        pointLight.intensity = pingPongValue;
    }
}
