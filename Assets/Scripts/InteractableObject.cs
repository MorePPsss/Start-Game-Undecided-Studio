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
        /*������ �������� + ����*/
        //S1 �ƶ�
        playerAgent.SetDestination(transform.position);
        //S2 ����
        //TODO
        Destroy(this.gameObject);
        Debug.Log("��Ҽ�����Ʒ��");
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
