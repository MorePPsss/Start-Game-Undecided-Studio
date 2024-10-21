using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*Set controller of button -By Kehao*/
public class ButtonControl : MonoBehaviour
{
    private NavMeshObstacle navMeshObstacle;
    public DoorControl doorControl;

    // Triggered when the player steps on the pedal
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Push the Button!");
            doorControl.OpenDoor();
        }
    }
    // Triggered when the player leaves the pedal
    void OnTriggerExit(Collider other)
    {
        Debug.Log("Player Leave Button");
    }
}
