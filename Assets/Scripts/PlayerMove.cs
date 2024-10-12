using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class PlayerMove : MonoBehaviour
{
    public float jumpForce = 10f; // ������Ծ������
    public Transform targetJumpPoint; // ��Ծ��Ŀ��λ��
    public float waitTimeBeforeJump = 2f; // �ȴ�ʱ��

    private NavMeshAgent playerAgent; // ��ҽ�ɫ�� NavMesh Agent
    private bool isJumping = false; // �Ƿ�������Ծ
    private Rigidbody rb; // ����������Ծ

    // Start is called before the first frame update
    void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>(); //���NavMeshAgent���
        rb = GetComponent<Rigidbody>(); // ��� Rigidbody ���
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isJumping)// ������������
        {
            // �������� NavMeshAgent�����ָ������ƶ�
            playerAgent.enabled = true;
            playerAgent.isStopped = false;

            Camera currentCamera = CamerasControl.Instance.GetCurrentCamera();// ��ȡ��ǰ�������ʵ��
            Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit; // ����һ�� RaycastHit ������׼���洢���߽��
            bool isCollide = Physics.Raycast(ray, out hit);
            if (isCollide)
            {
                playerAgent.SetDestination(hit.point);//����SetDestination��������������ƶ�Ŀ�ĵ�
            }
        }
    }

    // �����ҽ�����Ծ����ʱ������Ծ
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("JumpZone") && !isJumping) // ����Ƿ�����Ծ���򣬲���ֹ�ظ���Ծ
        {
            StartCoroutine(JumpToTarget()); // ��ʼִ����ԾЭ��
        }
    }

    // ��Ծ��Ŀ��λ�õ�Э��
    private IEnumerator JumpToTarget()
    {
        isJumping = true;

        // ���� NavMeshAgent�����������·���Է�ֹ�Զ��ƶ�
        playerAgent.isStopped = true;
        playerAgent.ResetPath();
        playerAgent.enabled = false;

        // ģ����Ծ��ʹ�� Rigidbody ������ϵ���
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // �ȴ�һ��ʱ����ģ����Ծ����
        yield return new WaitForSeconds(1.0f);

        isJumping = false;
    }
}
