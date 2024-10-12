using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class PlayerMove : MonoBehaviour
{
    public float jumpForce = 10f; // 控制跳跃的力度
    public Transform targetJumpPoint; // 跳跃的目标位置
    public float waitTimeBeforeJump = 2f; // 等待时间

    private NavMeshAgent playerAgent; // 玩家角色的 NavMesh Agent
    private bool isJumping = false; // 是否正在跳跃
    private Rigidbody rb; // 用于物理跳跃

    // Start is called before the first frame update
    void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>(); //获得NavMeshAgent组件
        rb = GetComponent<Rigidbody>(); // 获得 Rigidbody 组件
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isJumping)// 检测鼠标左键点击
        {
            // 重新启用 NavMeshAgent，并恢复正常移动
            playerAgent.enabled = true;
            playerAgent.isStopped = false;

            Camera currentCamera = CamerasControl.Instance.GetCurrentCamera();// 获取当前的摄像机实例
            Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit; // 声明一个 RaycastHit 变量，准备存储射线结果
            bool isCollide = Physics.Raycast(ray, out hit);
            if (isCollide)
            {
                playerAgent.SetDestination(hit.point);//调用SetDestination方法，设置玩家移动目的地
            }
        }
    }

    // 检测玩家进入跳跃区域时触发跳跃
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("JumpZone") && !isJumping) // 检查是否是跳跃区域，并防止重复跳跃
        {
            StartCoroutine(JumpToTarget()); // 开始执行跳跃协程
        }
    }

    // 跳跃到目标位置的协程
    private IEnumerator JumpToTarget()
    {
        isJumping = true;

        // 禁用 NavMeshAgent，并清空它的路径以防止自动移动
        playerAgent.isStopped = true;
        playerAgent.ResetPath();
        playerAgent.enabled = false;

        // 模拟跳跃，使用 Rigidbody 添加向上的力
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // 等待一段时间来模拟跳跃过程
        yield return new WaitForSeconds(1.0f);

        isJumping = false;
    }
}
