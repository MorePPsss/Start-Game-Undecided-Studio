using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InteractableObject : MonoBehaviour
{
    public NavMeshAgent playerAgent;
    private bool haveInteracted = false;
    public void OnClick(NavMeshAgent playerAgent)
    {
        this.playerAgent = playerAgent;
        /*分两步 来到附近 + 交互*/
        //S1 移动
        playerAgent.SetDestination(transform.position);
        //S2 交互

        //TODO 跟环境的互动！
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
    /*互动对于子类来说是要可以重写的 针对不同的物品类型：可安装类型的物品、可作用于场景的物体*/
    protected virtual void Interact()
    {
        Debug.Log("玩家产生互动！");
    }
}
