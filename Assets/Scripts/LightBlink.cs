using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlink : MonoBehaviour
{
    public float maxIntensity = 2f;      // 最大亮度
    public float speed = 1f;            // 闪烁速度

    private Light pointLight;

    void Start()
    {
        pointLight = GetComponent<Light>();
    }

    void Update()
    {
        // 让 light.intensity 在 0 和 maxIntensity 之间往返
        float pingPongValue = Mathf.PingPong(Time.time * speed, maxIntensity);
        pointLight.intensity = pingPongValue;
    }
}
