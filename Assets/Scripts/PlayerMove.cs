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
        playerAgent = GetComponent<NavMeshAgent>(); //���NavMeshAgent���
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))// ������������
        {
            Camera currentCamera = CamerasControl.Instance.GetCurrentCamera();// ��ȡ��ǰ�������ʵ��
            Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit; // ����һ�� RaycastHit ������׼���洢���߽��
            bool isCollide = Physics.Raycast(ray, out hit);
            if (isCollide)
            {
                if(hit.collider.tag == Tag.GROUND)
                {
                    playerAgent.SetDestination(hit.point);//����SetDestination��������������ƶ�Ŀ�ĵ�
                }else if(hit.collider.tag == Tag.INTERACTABLE)
                {
                    hit.collider.GetComponent<InteractableObject>().OnClick(playerAgent);
                }
                
            }
        }
    }
}
