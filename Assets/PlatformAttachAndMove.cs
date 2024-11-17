using UnityEngine;
using UnityEngine.AI;

public class PlatformAttachAndMove : MonoBehaviour
{
    public PanelR platformMoveScript; // ����ƽ̨�ƶ��ű�
    private Transform playerTransform = null;  // �洢��ҵ� Transform
    private NavMeshAgent playerNavMeshAgent = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform;
            playerNavMeshAgent = other.GetComponent<NavMeshAgent>();

            if (playerNavMeshAgent != null)
            {
                playerNavMeshAgent.enabled = false; // ��ʱ���� NavMeshAgent
            }

            // �������Ϊƽ̨���Ӷ���
            playerTransform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerNavMeshAgent != null)
            {
                playerNavMeshAgent.enabled = true; // �ָ� NavMeshAgent
            }

            // ������ӹ�ϵ
            playerTransform.SetParent(null);
            playerTransform = null;
            playerNavMeshAgent = null;
        }
    }

    public void StartPlatformMove()
    {
        if (platformMoveScript != null)
        {
            platformMoveScript.StartMoving(); // ����ƽ̨�ƶ�
        }
    }
}
