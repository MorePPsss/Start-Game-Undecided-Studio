using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class PlayerController : MonoBehaviour
{
    private NavMeshAgent playerAgent; // ��ҽ�ɫ�� NavMesh Agent
    // Start is called before the first frame update
    void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>(); //������GetComponent
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))// ������������
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ������������λ�÷�������
            RaycastHit hit; // ����һ�� RaycastHit ������׼���洢���߽��
            bool isCollide = Physics.Raycast(ray, out hit);
            if (isCollide)
            {
                playerAgent.SetDestination(hit.point);//����SetDestination��������������ƶ�Ŀ�ĵ�
            }
        }
    }
}
