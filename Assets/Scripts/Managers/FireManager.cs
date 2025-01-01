using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    public FireArea[] fireAreas;
    public FireExtinguisher[] fireExtinguisher;
    public ParticleSystem[] extinguisherEffects;//to control smoke effects
    public void Start()
    {
        for (int i = 0; i < extinguisherEffects.Length; i++)
        {
            extinguisherEffects[i].Stop();
        }
    }
    public void ActivateExtinguisher(int index)
    {
        // 1. ���������Ȼָ�ȼ��
        for (int i = 0; i < fireAreas.Length; i++)
        {
            fireAreas[i].SetFireActive(true);
            fireExtinguisher[i].haveInteractedFlag = false;
        }
        for (int i = 0; i < extinguisherEffects.Length; i++)
        {
            extinguisherEffects[i].Stop();
        }
        // 2. ֻ�� index ��Ӧ�Ļ�������Ϩ��
        extinguisherEffects[index].Play();
        fireAreas[index].SetFireActive(false);
        fireExtinguisher[index].haveInteractedFlag = true;
    }
}
