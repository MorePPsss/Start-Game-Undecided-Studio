using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    private Animator animator;
    public void OpenDoor()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("PushButton");
        Debug.Log("Open the door!");
    }
}
