using UnityEngine;
using UnityEngine.AI;

public class PlatformAttachAndMove : MonoBehaviour
{
    public PanelR platformMoveScript; // 引用平台移动脚本
    private Transform playerTransform = null;  // 存储玩家的 Transform
    private NavMeshAgent playerNavMeshAgent = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform;
            playerNavMeshAgent = other.GetComponent<NavMeshAgent>();

            if (playerNavMeshAgent != null)
            {
                playerNavMeshAgent.enabled = false; // 暂时禁用 NavMeshAgent
            }

            // 将玩家设为平台的子对象
            playerTransform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerNavMeshAgent != null)
            {
                playerNavMeshAgent.enabled = true; // 恢复 NavMeshAgent
            }

            // 解除父子关系
            playerTransform.SetParent(null);
            playerTransform = null;
            playerNavMeshAgent = null;
        }
    }

    public void StartPlatformMove()
    {
        if (platformMoveScript != null)
        {
            platformMoveScript.StartMoving(); // 启动平台移动
        }
    }
}
