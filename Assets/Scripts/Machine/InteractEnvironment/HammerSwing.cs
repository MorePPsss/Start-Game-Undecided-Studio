using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerSwing : MonoBehaviour
{

    public float swingAngle = 45f; // ���ڶ��Ƕ�
    public float speed = 2f;       // �ڶ��ٶ�
    private float startAngle;
    // Start is called before the first frame update
    void Start()
    {
        startAngle = transform.localRotation.eulerAngles.z; // ��¼��ʼ�Ƕ�
    }

    // Update is called once per frame
    void Update()
    {
        float angle = startAngle + Mathf.Sin(Time.time * speed) * swingAngle; // ���㵱ǰ�Ƕ�
        transform.localRotation = Quaternion.Euler(0, 0, angle);             // ��ת֧��
    }
}
