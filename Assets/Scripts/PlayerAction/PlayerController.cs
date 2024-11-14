using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    private NavMeshAgent playerAgent;
    private Animator playerAnimator;
    private OffMeshLink[] offMeshLinks;
    private InteractableObject interactableObject;
    public GameObject baitPrefab;
    private Vector3 hitPoint;

    public Vector3 Getposition()
    {
        return this.transform.position;
    }
    [SerializeField] private bool haveSpring = false;
    ItemUI item;
    void HandleOffMeshLink()
    {
        Vector3 originalPosition = playerAgent.transform.position;
        playerAgent.CompleteOffMeshLink();
        playerAgent.Warp(originalPosition);
    }

    void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        playerAnimator = GetComponent<Animator>();
        interactableObject = GetComponent<InteractableObject>();
        playerAnimator.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(AnimationControl_WalktoStand())
        {
            playerAnimator.SetBool("isWalk", false);
        }
        if (Mouse.current.leftButton.isPressed)// Detect left mouse click -By Kehao
        {
            if (InteractWithUI()) return;
            Camera currentCamera = CamerasControl.Instance.GetCurrentCamera();
            Ray ray = currentCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit; // Declare a RaycastHit variable to store ray results
            bool isCollide = Physics.Raycast(ray, out hit);
            isSpring();
            if (isCollide)
            {
                hitPoint = hit.point;
                playerAnimator.SetBool("isWalk", true);
                if (hit.collider.tag == Tag.GROUND || hit.collider.tag == Tag.BUTTON)
                {
                    playerAgent.SetDestination(hit.point);//Call the SetDestination method to set the player's destination for movement -By Kehao
                }
                else if(hit.collider.tag == Tag.INTERACTABLE || hit.collider.tag == Tag.BAITITEM)
                {
                    hit.collider.GetComponent<InteractableObject>().OnClick(playerAgent);
                }
                else if (hit.collider.tag == Tag.GEAR)
                {
                    hit.collider.GetComponentInParent<GearMachineControl>().PickUpGear(hit.transform);
                }
            }
        }
        else if (InputManager.instance.putBait.triggered && InputManager.instance!=null)
        {
            if (InteractableObject.baitNum > 0)
            {
                Debug.Log("Put Bait��");
                InteractableObject.baitNum -= 1;
                Vector3 baitPosition = transform.position + transform.forward; // 2�Ǿ�����ҵ�ƫ����
                Instantiate(baitPrefab, baitPosition, Quaternion.identity);
                //TODO:instantiate Bait Object
            }
            else
            {
                Debug.Log("Can not put Bait��");
            }
            
        }

            bool InteractWithUI()// is current click is on UI
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                return true;
            }
            else return false;
        }


        void isSpring() // is spring equiped
        {
            if (InventoryManager.Instance.equipmentData.itemList[1].itemData != null)
                haveSpring = true;
            else
            haveSpring = false;
            //Debug.Log(InventoryManager.Instance.equipmentData.itemList[0].itemData);
        }

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

    bool AnimationControl_WalktoStand()
    {
        if (Vector3.Distance(playerAgent.transform.position, hitPoint) < 1)
        {
            return true;
        }
        return false;
    }
}
