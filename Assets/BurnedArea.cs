using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnedArea : MonoBehaviour
{
    private bool isBurning = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.PLAYER))
        {
            Debug.Log("��ұ����գ�");
            isBurning = true;
            StartCoroutine(BurnPlayer(other.gameObject));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tag.PLAYER))
        {
            isBurning = false;
            StopAllCoroutines(); // ����뿪��������ֹͣ����
        }
    }

    IEnumerator BurnPlayer(GameObject player)
    {
        float burnTime = 1f; // ��������ʱ��
        float elapsed = 0f;
        while (elapsed < burnTime)
        {
            elapsed += Time.deltaTime;
            Debug.Log("Player is burning..."); // ����ѡ�����ʵʱ����
            yield return null;
        }
        if (isBurning) // ȷ��������ڻ���������
        {
            GameManager.Instance.GameOver(DeadType.Burned);
        }
    }
}
