using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;


public class PlayerController : MonoBehaviour
{
    private NavMeshAgent playerAgent; // ��ҽ�ɫ�� NavMesh Agent
    private Animator playerAnimator; //��ҽ�ɫ��Animator
    private OffMeshLink[] offMeshLinks; // ���������е�offMesh link
    public Vector3 Getposition()
    {
        return this.transform.position;
    }
    [SerializeField] private bool haveSpring = false; // TODO ����Ƿ�װ������
    ItemUI item;
    // Start is called before the first frame update

    void HandleOffMeshLink()
    {
        // �����ͨ��offmeshlink��ֱ�Ӵ��ͻ�ԭλ
        Vector3 originalPosition = playerAgent.transform.position;
        playerAgent.CompleteOffMeshLink();
        playerAgent.Warp(originalPosition);
    }

    void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>(); //���NavMeshAgent���
        playerAnimator = GetComponent<Animator>();
        playerAnimator.enabled = false; //����Ϸ��ʼ�׶ν�����ҵĶ�������������ƶ�ʧЧ��by-kehao
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))// ������������ by-kehao
        {
            Camera currentCamera = CamerasControl.Instance.GetCurrentCamera();// ��ȡ��ǰ�������ʵ��
            Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit; // ����һ�� RaycastHit ������׼���洢���߽��
            bool isCollide = Physics.Raycast(ray, out hit);
            isSpring();
            if (isCollide)
            {
                if(hit.collider.tag == Tag.GROUND)
                {
                    playerAgent.SetDestination(hit.point);//����SetDestination��������������ƶ�Ŀ�ĵ� by-kehao
                }
                else if(hit.collider.tag == Tag.INTERACTABLE)
                {
                    hit.collider.GetComponent<InteractableObject>().OnClick(playerAgent);
                }
            }
        }

       


        void isSpring()
        {
            if (InventoryManager.Instance.equipmentData.itemList[1].itemData != null)
                haveSpring = true;
            else
            haveSpring = false;
            //Debug.Log(InventoryManager.Instance.equipmentData.itemList[0].itemData);
        }

        // ���δװ���������Զ���OffMeshLink��Ϊ
        if (!haveSpring)
        {
            playerAgent.autoTraverseOffMeshLink = false;
            if (playerAgent.isOnOffMeshLink)
            {
                HandleOffMeshLink();
            }
        }
        else
        {
            playerAgent.autoTraverseOffMeshLink = true;
        }
    }
}
