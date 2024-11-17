using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerSwing : MonoBehaviour
{

    public float swingAngle = 45f; // 最大摆动角度
    public float speed = 2f;       // 摆动速度
    private float startAngle;
    // Start is called before the first frame update
    void Start()
    {
        startAngle = transform.localRotation.eulerAngles.z; // 记录初始角度
    }

    // Update is called once per frame
    void Update()
    {
        float angle = startAngle + Mathf.Sin(Time.time * speed) * swingAngle; // 计算当前角度
        transform.localRotation = Quaternion.Euler(0, 0, angle);             // 旋转支点
    }
}
