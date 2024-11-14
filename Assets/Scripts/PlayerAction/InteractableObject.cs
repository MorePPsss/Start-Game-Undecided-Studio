using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*The base class of interactive logic -By Kehao*/
public class InteractableObject : MonoBehaviour
{
    public NavMeshAgent playerAgent;
    public bool haveInteracted = false;
    public static int baitNum;// Using keyword'static', all object which need to modify this variable share a same count.
    public void OnClick(NavMeshAgent playerAgent)
    {
        this.playerAgent = playerAgent;
        playerAgent.stoppingDistance = 1;
        haveInteracted = false;

        /*Take two steps to get nearby+interact -By Kehao*/
        //S1 nearby
        playerAgent.SetDestination(transform.position);
        //S2 interact
        //TODO Interaction with the environment!
    }

    void Update()
    {
        if (playerAgent != null && !haveInteracted && !playerAgent.pathPending)
        {
            // 判断人物是否接近物体
            if (Vector3.Distance(playerAgent.transform.position, transform.position) <= playerAgent.stoppingDistance)
            {
                Interact();
            }
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
