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
        playerAgent = GetComponent<NavMeshAgent>(); //获得组件GetComponent
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))// 检测鼠标左键点击
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 从相机向鼠标点击位置发射射线
            RaycastHit hit; // 声明一个 RaycastHit 变量，准备存储射线结果
            bool isCollide = Physics.Raycast(ray, out hit);
            if (isCollide)
            {
                playerAgent.SetDestination(hit.point);//调用SetDestination方法，设置玩家移动目的地
            }
        }
    }
}
