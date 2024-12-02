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
            Debug.Log("玩家被灼烧！");
            isBurning = true;
            StartCoroutine(BurnPlayer(other.gameObject));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tag.PLAYER))
        {
            isBurning = false;
            StopAllCoroutines(); // 玩家离开火焰区域，停止灼烧
        }
    }

    IEnumerator BurnPlayer(GameObject player)
    {
        float burnTime = 1f; // 持续灼烧时间
        float elapsed = 0f;
        while (elapsed < burnTime)
        {
            elapsed += Time.deltaTime;
            Debug.Log("Player is burning..."); // （可选）添加实时反馈
            yield return null;
        }
        if (isBurning) // 确保玩家仍在火焰区域内
        {
            GameManager.Instance.GameOver(DeadType.Burned);
        }
    }
}
