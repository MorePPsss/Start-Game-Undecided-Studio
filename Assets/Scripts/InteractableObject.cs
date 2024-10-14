using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InteractableObject : MonoBehaviour
{
    private NavMeshAgent playerAgent;
    private bool haveInteracted = false;
    public void OnClick(NavMeshAgent playerAgent)
    {
        this.playerAgent = playerAgent;
        /*������ �������� + ����*/
        //S1 �ƶ�
        playerAgent.SetDestination(transform.position);
        //S2 ����

        //TODO �������Ļ�����
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAgent != null && haveInteracted == false && playerAgent.pathPending == false)
        {
            if (playerAgent.remainingDistance <= 1)
            {
                Interact();
                haveInteracted = true;
            }
        }
    }
    /*��������������˵��Ҫ������д�� ��Բ�ͬ����Ʒ���ͣ��ɰ�װ���͵���Ʒ���������ڳ���������*/
    protected virtual void Interact()
    {
        Debug.Log("��Ҳ���������");
    }
}
