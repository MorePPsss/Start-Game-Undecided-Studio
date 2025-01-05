using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Set Animation of ControlledDoor -By Kehao*/
public class DoorControlSteam : MonoBehaviour
{
    private Animator animator;
    public void OpenDoor()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("PushButton", true);
    }
}
