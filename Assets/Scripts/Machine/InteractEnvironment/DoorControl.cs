using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Set Animation of ControlledDoor -By Kehao*/
public class DoorControl : MonoBehaviour
{
    private Animator animator;
    public void OpenDoor()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("PushButton");
        Debug.Log("Open the ControlledDoor!");
    }
}
