using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*该脚本目前作用 为了玩家和环境中的机关互动而产生的一系列 逻辑+动画 -By Kehao
  TODO 这部分逻辑后期应该进行封装
 */
public class PlayerInteract : InteractableObject
{
    private Animator leverAnimator;
    private Animator platformAnimator;
    private Animator playerAnimator;
    private bool isLeverPulled = false;

    public GameObject platformObject; // 平台对象
    public GameObject playerObject; // 玩家对象
    public float playerBoomJumpWaitTime;// 该变量亦在使机器爆炸时尽量和玩家弹射动画播放同步 -By Kehao
    //public ExplodeBlocks explodeBlocks;

    protected override void Interact()
    {
        leverAnimator = GetComponent<Animator>();
        platformAnimator = platformObject.GetComponent<Animator>();
        playerAnimator = playerObject.GetComponent<Animator>();
        playerAgent = playerObject.GetComponent<NavMeshAgent>();

        // 拉下拉杆，播放拉杆动画
        leverAnimator.SetBool("Pull", true);
        if (!isLeverPulled)
        {
            StartCoroutine(WaitForLeverAnimation());
            isLeverPulled = true;
        }
    }
    /*协程WaitForLeverAnimation()作用：
        为了确保拉杆动画结束后 机器才能开始播放抖动动画 -By Kehao
     */
    IEnumerator WaitForLeverAnimation()
    {
        // 等待拉杆动画播放到"PullBar"状态
        AnimatorStateInfo leverStateInfo = leverAnimator.GetCurrentAnimatorStateInfo(0);
        while (!leverStateInfo.IsName("PullBar"))
        {
            leverStateInfo = leverAnimator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }

        // 等待拉杆动画播放结束
        while (leverStateInfo.normalizedTime < 1.0f || leverAnimator.IsInTransition(0))
        {
            leverStateInfo = leverAnimator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }

        // 拉杆动画结束后，播放平台动画
        platformAnimator.SetBool("Shake", true);

        // 等待平台动画播放到"SteamMachineShake"状态
        AnimatorStateInfo platformStateInfo = platformAnimator.GetCurrentAnimatorStateInfo(0);
        while (!platformStateInfo.IsName("SteamMachineShake"))
        {
            platformStateInfo = platformAnimator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }

        // 等待平台动画播放结束
        while (platformStateInfo.normalizedTime < 1.0f || platformAnimator.IsInTransition(0))
        {
            platformStateInfo = platformAnimator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }
        /*调整玩家朝向：
         为了使玩家因为机器爆炸而弹射而出的落点正确 -By Kehao
         */
        Quaternion currentRotation = playerObject.transform.rotation;// 获取玩家当前朝向
        // 重置玩家朝向为动画的初始方向（假设动画是从前方播放的）
        playerObject.transform.rotation = Quaternion.LookRotation(Vector3.forward); // 设定动画为面向前方

        playerAgent.enabled = false;// 暂时禁用 NavMeshAgent 以防止它干扰动画

        playerAnimator.enabled = true;// 启用 Animator 并设置播放动画
        playerAnimator.SetBool("BoomJump", true);

        yield return new WaitForSeconds(playerBoomJumpWaitTime);//机器爆炸和动画同步
        Destroy(platformObject);

        Debug.Log("平台已销毁，玩家弹射动画开始！");
        //explodeBlocks.Explode();
    }
}
