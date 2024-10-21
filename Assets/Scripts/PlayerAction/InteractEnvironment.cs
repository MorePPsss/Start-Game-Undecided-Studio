using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*�ýű�Ŀǰ���� Ϊ����Һͻ����еĻ��ػ�����������һϵ�� �߼�+���� -By Kehao
  TODO �ⲿ���߼�����Ӧ�ý��з�װ
 */
public class PlayerInteract : InteractableObject
{
    private Animator leverAnimator;
    private Animator platformAnimator;
    private Animator playerAnimator;
    private bool isLeverPulled = false;

    public GameObject platformObject; // ƽ̨����
    public GameObject playerObject; // ��Ҷ���
    public float playerBoomJumpWaitTime;// �ñ�������ʹ������ըʱ��������ҵ��䶯������ͬ�� -By Kehao
    //public ExplodeBlocks explodeBlocks;

    protected override void Interact()
    {
        leverAnimator = GetComponent<Animator>();
        platformAnimator = platformObject.GetComponent<Animator>();
        playerAnimator = playerObject.GetComponent<Animator>();
        playerAgent = playerObject.GetComponent<NavMeshAgent>();

        // �������ˣ��������˶���
        leverAnimator.SetBool("Pull", true);
        if (!isLeverPulled)
        {
            StartCoroutine(WaitForLeverAnimation());
            isLeverPulled = true;
        }
    }
    /*Э��WaitForLeverAnimation()���ã�
        Ϊ��ȷ�����˶��������� �������ܿ�ʼ���Ŷ������� -By Kehao
     */
    IEnumerator WaitForLeverAnimation()
    {
        // �ȴ����˶������ŵ�"PullBar"״̬
        AnimatorStateInfo leverStateInfo = leverAnimator.GetCurrentAnimatorStateInfo(0);
        while (!leverStateInfo.IsName("PullBar"))
        {
            leverStateInfo = leverAnimator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }

        // �ȴ����˶������Ž���
        while (leverStateInfo.normalizedTime < 1.0f || leverAnimator.IsInTransition(0))
        {
            leverStateInfo = leverAnimator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }

        // ���˶��������󣬲���ƽ̨����
        platformAnimator.SetBool("Shake", true);

        // �ȴ�ƽ̨�������ŵ�"SteamMachineShake"״̬
        AnimatorStateInfo platformStateInfo = platformAnimator.GetCurrentAnimatorStateInfo(0);
        while (!platformStateInfo.IsName("SteamMachineShake"))
        {
            platformStateInfo = platformAnimator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }

        // �ȴ�ƽ̨�������Ž���
        while (platformStateInfo.normalizedTime < 1.0f || platformAnimator.IsInTransition(0))
        {
            platformStateInfo = platformAnimator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }
        /*������ҳ���
         Ϊ��ʹ�����Ϊ������ը����������������ȷ -By Kehao
         */
        Quaternion currentRotation = playerObject.transform.rotation;// ��ȡ��ҵ�ǰ����
        // ������ҳ���Ϊ�����ĳ�ʼ���򣨼��趯���Ǵ�ǰ�����ŵģ�
        playerObject.transform.rotation = Quaternion.LookRotation(Vector3.forward); // �趨����Ϊ����ǰ��

        playerAgent.enabled = false;// ��ʱ���� NavMeshAgent �Է�ֹ�����Ŷ���

        playerAnimator.enabled = true;// ���� Animator �����ò��Ŷ���
        playerAnimator.SetBool("BoomJump", true);

        yield return new WaitForSeconds(playerBoomJumpWaitTime);//������ը�Ͷ���ͬ��
        Destroy(platformObject);

        Debug.Log("ƽ̨�����٣���ҵ��䶯����ʼ��");
        //explodeBlocks.Explode();
    }
}
