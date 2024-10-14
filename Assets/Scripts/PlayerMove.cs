using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class PlayerController : MonoBehaviour
{
    private NavMeshAgent playerAgent; // 玩家角色的 NavMesh Agent
    // Start is called before the first frame update
    void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>(); //获得NavMeshAgent组件
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))// 检测鼠标左键点击
        {
            Camera currentCamera = CamerasControl.Instance.GetCurrentCamera();// 获取当前的摄像机实例
            Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit; // 声明一个 RaycastHit 变量，准备存储射线结果
            bool isCollide = Physics.Raycast(ray, out hit);
            if (isCollide)
            {
                if(hit.collider.tag == Tag.GROUND)
                {
                    playerAgent.SetDestination(hit.point);//调用SetDestination方法，设置玩家移动目的地
                }else if(hit.collider.tag == Tag.INTERACTABLE)
                {
                    hit.collider.GetComponent<InteractableObject>().OnClick(playerAgent);
                }
                
            }
        }
    }
}
