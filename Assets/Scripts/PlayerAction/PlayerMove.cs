using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;


public class PlayerController : MonoBehaviour
{
    private NavMeshAgent playerAgent; // 玩家角色的 NavMesh Agent
    private Animator playerAnimator; //玩家角色的Animator
    private OffMeshLink[] offMeshLinks; // 场景中所有的offMesh link
    public Vector3 Getposition()
    {
        return this.transform.position;
    }
    [SerializeField] private bool haveSpring = false; // TODO 玩家是否装备弹簧
    ItemUI item;
    // Start is called before the first frame update

    void HandleOffMeshLink()
    {
        // 在玩家通过offmeshlink后直接传送回原位
        Vector3 originalPosition = playerAgent.transform.position;
        playerAgent.CompleteOffMeshLink();
        playerAgent.Warp(originalPosition);
    }

    void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>(); //获得NavMeshAgent组件
        playerAnimator = GetComponent<Animator>();
        playerAnimator.enabled = false; //在游戏开始阶段禁用玩家的动画组件，避免移动失效！by-kehao
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))// 检测鼠标左键点击 by-kehao
        {
            Camera currentCamera = CamerasControl.Instance.GetCurrentCamera();// 获取当前的摄像机实例
            Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit; // 声明一个 RaycastHit 变量，准备存储射线结果
            bool isCollide = Physics.Raycast(ray, out hit);
            isSpring();
            if (isCollide)
            {
                if(hit.collider.tag == Tag.GROUND)
                {
                    playerAgent.SetDestination(hit.point);//调用SetDestination方法，设置玩家移动目的地 by-kehao
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

        // 玩家未装备弹簧则自定义OffMeshLink行为
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
