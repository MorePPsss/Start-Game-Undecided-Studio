using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*The base class of interactive logic -By Kehao*/
public class InteractableObject : MonoBehaviour
{
    public NavMeshAgent playerAgent;
    public bool haveInteracted = false;
    private bool isMovingToInteract = false;
    public void OnClick(NavMeshAgent playerAgent)
    {
        this.playerAgent = playerAgent;
        playerAgent.stoppingDistance = 2;
        isMovingToInteract = true;
        haveInteracted = false;

        /*Take two steps to get nearby+interact -By Kehao*/
        //S1 nearby
        playerAgent.SetDestination(transform.position);
        //S2 interact
        //TODO Interaction with the environment!
    }

    void Update()
    {
        if (playerAgent != null && isMovingToInteract && !haveInteracted && !playerAgent.pathPending)
        {
            // �ж������Ƿ�ӽ�����
            if (playerAgent.remainingDistance <= playerAgent.stoppingDistance)
            {
                Interact();
                isMovingToInteract = false; // ��ɽ�����ȡ�������ƶ�״̬
            }
        }

        // ����ҵ�������ط���Ŀ��ı�ʱ��ȡ������
        if (playerAgent != null && playerAgent.remainingDistance > playerAgent.stoppingDistance && isMovingToInteract)
        {
            isMovingToInteract = false; // ȡ�������ƶ�״̬
        }
    }
    /*For subclasses, interaction should be rewritable for different types of items: 
     installable items, objects that can affect the scene -By Kehao
    */
    protected virtual void Interact()
    {
        Debug.Log("Interactor with Player��");
    }
}
