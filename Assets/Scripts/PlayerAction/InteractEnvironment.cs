using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*The script currently serves as a series of logic and animations generated for the interaction between players and the mechanisms in the environment -By Kehao
  TODO This part of the logic should be encapsulated later on
 */
public class InteractEnvironment : InteractableObject
{
    private Animator leverAnimator;
    private Animator platformAnimator;
    private Animator playerAnimator;
    private bool isLeverPulled = false;

    public GameObject EnvironmentObject;
    public GameObject playerObject;
    public NavMeshAgent playerAgent;
    public float playerBoomJumpWaitTime;// This variable also tries to synchronize with the player's ejection animation playback as much as possible when the machine explodes -By Kehao
    //public ExplodeBlocks explodeBlocks;

    protected override void Interact()
    {
        leverAnimator = GetComponent<Animator>();
        platformAnimator = EnvironmentObject.GetComponent<Animator>();
        playerAnimator = playerObject.GetComponent<Animator>();
        playerAgent = playerObject.GetComponent<NavMeshAgent>();

        leverAnimator.SetBool("Pull", true);
        if (!isLeverPulled)
        {
            Debug.Log("À­ÏÂÀ­¸Ë");
            StartCoroutine(WaitForLeverAnimation());
            isLeverPulled = true;
        }
    }
    /*The function of WaitForLeverAnimation()£º
        To ensure that the machine can only start playing the shaking animation after the end of the pull rod animation -By Kehao
     */
    IEnumerator WaitForLeverAnimation()
    {
        // Waiting for the scrolling animation to play to the "PullBar" state
        AnimatorStateInfo leverStateInfo = leverAnimator.GetCurrentAnimatorStateInfo(0);
        while (!leverStateInfo.IsName("PullBar"))
        {
            leverStateInfo = leverAnimator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }

        // Waiting for the end of the scrolling animation playback
        while (leverStateInfo.normalizedTime < 1.0f || leverAnimator.IsInTransition(0))
        {
            leverStateInfo = leverAnimator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }

        // After the scrolling animation ends, play the platform animation
        platformAnimator.SetBool("Shake", true);

        // Waiting for the platform animation to play to the 'SteamMechanicShake' state
        AnimatorStateInfo platformStateInfo = platformAnimator.GetCurrentAnimatorStateInfo(0);
        while (!platformStateInfo.IsName("SteamMachineShake"))
        {
            platformStateInfo = platformAnimator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }

        // Waiting for the end of the platform animation playback
        while (platformStateInfo.normalizedTime < 1.0f || platformAnimator.IsInTransition(0))
        {
            platformStateInfo = platformAnimator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }
        /*Adjust player orientation£º
         In order to ensure that the landing point of the player's ejection due to the explosion of the machine is correct -By Kehao
         */
        playerObject.transform.rotation = Quaternion.LookRotation(Vector3.forward);
        if (playerObject.transform.position.y > 1)
        {
            playerAgent.enabled = false;// Temporarily disable NavMeshAgent to prevent it from interfering with animations
            playerAnimator.enabled = true;
            playerAnimator.SetBool("BoomJump", true);
        }
        yield return new WaitForSeconds(playerBoomJumpWaitTime);//machine explosion and animation synchronization
        Destroy(EnvironmentObject);
        playerAgent.enabled = true;
        Debug.Log("Plane was destroied£¡");
        //explodeBlocks.Explode();
    }
}
