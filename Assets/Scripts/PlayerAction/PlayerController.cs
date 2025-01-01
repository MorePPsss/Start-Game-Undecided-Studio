using Cinemachine;
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
    public Camera currentCamera; //For using VirtualCamera -By Kehao
    private const string IsWalk = "isWalk";
    private const string Jump = "Jump";
    private const string Landing = "Landing";

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
        GameManager.Instance.SetPlayer(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (AnimationControl_WalktoStand())
        {
            playerAnimator.SetBool(IsWalk, false);
        }

        if (Mouse.current.leftButton.isPressed)// Detect left mouse click -By Kehao
        {
            if (InteractWithUI()) return;
            //Camera currentCamera = CamerasControl.Instance.GetCurrentCamera();
            Ray ray = currentCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit; // Declare a RaycastHit variable to store ray results
            bool isCollide = Physics.Raycast(ray, out hit);
            isSpring();
            if (isCollide)
            {
                hitPoint = hit.point;
                playerAnimator.SetBool(IsWalk, true);
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
                Debug.Log("Put Bait！");
                InteractableObject.baitNum -= 1;
                Vector3 baitPosition = transform.position + transform.forward; // 2是距离玩家的偏移量
                Instantiate(baitPrefab, baitPosition, Quaternion.identity);
                //TODO:instantiate Bait Object
            }
            else
            {
                UIManager.Instance.ShowTipUI("Insufficient bait available!z");
            }
            
        }
        if (playerAgent.isOnOffMeshLink)
        {
            //TODO: Jumping Animation
            //StartCoroutine(HandleJump());
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
        // 检查路径是否已经计算完成，以及剩余距离是否小于停止范围
        if (!playerAgent.pathPending && playerAgent.remainingDistance <= playerAgent.stoppingDistance)
        {
            // 如果有剩余距离但角色接近目标位置，判断角色是否已经停止
            if (!playerAgent.hasPath || playerAgent.velocity.sqrMagnitude == 0f)
            {
                return true;
            }
        }
        return false;
    }

    private IEnumerator HandleJump()
    {
        // 暂停 NavMeshAgent
        playerAgent.isStopped = true;
        playerAgent.updatePosition = false; // 禁用 NavMeshAgent 对位置的更新
        playerAgent.updateRotation = false; // 禁用 NavMeshAgent 对旋转的更新

        // 获取跳跃起点和终点
        Vector3 startPosition = playerAgent.transform.position;
        Vector3 endPosition = playerAgent.currentOffMeshLinkData.endPos + Vector3.up * playerAgent.baseOffset;
        Quaternion lookRotation = Quaternion.LookRotation(endPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

        // 播放跳跃动画
        playerAnimator.SetTrigger(Jump);

        // 假设跳跃动画持续 1 秒
        float jumpDuration = 0.5f;
        float elapsedTime = 0f;

        // 跳跃过程
        while (elapsedTime < jumpDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / jumpDuration;

            // 平滑插值位置
            Vector3 currentPosition = Vector3.Lerp(startPosition, endPosition, t);

            // 添加抛物线高度
            float height = Mathf.Sin(Mathf.PI * t) * 0.5f; // 调整跳跃高度
            currentPosition.y += height;

            // 更新角色位置
            playerAgent.transform.position = currentPosition;

            yield return null;
        }
        playerAnimator.SetTrigger(Landing);
        // 确保跳跃完成时角色到达目标位置
        playerAgent.transform.position = endPosition;

        // 恢复 NavMeshAgent 的控制
        playerAgent.CompleteOffMeshLink();
        playerAgent.isStopped = false;
        playerAgent.updatePosition = true;  // 恢复位置更新
        playerAgent.updateRotation = true; // 恢复旋转更新
        playerAnimator.SetBool(IsWalk, false);
    }

}
