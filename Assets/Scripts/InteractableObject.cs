using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InteractableObject : MonoBehaviour
{
    private NavMeshAgent playerAgent;
    public void OnClick(NavMeshAgent playerAgent)
    {
        this.playerAgent = playerAgent;
        playerAgent.stoppingDistance = 2;
        /*分两步 来到附近 + 交互*/
        //S1 移动
        playerAgent.SetDestination(transform.position);
        //S2 交互
        //TODO
        Destroy(this.gameObject);
        Debug.Log("玩家捡起物品！");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
