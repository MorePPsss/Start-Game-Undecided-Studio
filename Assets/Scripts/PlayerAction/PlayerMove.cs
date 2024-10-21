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
    void HandleOffMeshLink()
    {
        // 在玩家通过offmeshlink后直接传送回原位
        Vector3 originalPosition = playerAgent.transform.position;
        playerAgent.CompleteOffMeshLink();
        playerAgent.Warp(originalPosition);
    }

    void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        playerAnimator = GetComponent<Animator>();
        playerAnimator.enabled = false; //Disable the player's animation components at the beginning of the game to avoid movement failure!by-kehao
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))// Detect left mouse click -By Kehao
        {
            Camera currentCamera = CamerasControl.Instance.GetCurrentCamera();
            Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit; // Declare a RaycastHit variable to store ray results
            bool isCollide = Physics.Raycast(ray, out hit);
            isSpring();
            if (isCollide)
            {
                if(hit.collider.tag == Tag.GROUND || hit.collider.tag == Tag.BUTTON)
                {
                    playerAgent.SetDestination(hit.point);//Call the SetDestination method to set the player's destination for movement -By Kehao
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
