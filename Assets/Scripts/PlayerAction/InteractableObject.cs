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
            // 判断人物是否接近物体
            if (playerAgent.remainingDistance <= playerAgent.stoppingDistance)
            {
                Interact();
                isMovingToInteract = false; // 完成交互后，取消交互移动状态
            }
        }

        // 当玩家点击其他地方，目标改变时，取消交互
        if (playerAgent != null && playerAgent.remainingDistance > playerAgent.stoppingDistance && isMovingToInteract)
        {
            isMovingToInteract = false; // 取消交互移动状态
        }
    }
    /*For subclasses, interaction should be rewritable for different types of items: 
     installable items, objects that can affect the scene -By Kehao
    */
    protected virtual void Interact()
    {
        Debug.Log("Interactor with Player！");
    }
}
