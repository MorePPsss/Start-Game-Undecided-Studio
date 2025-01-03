using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionPoint : MonoBehaviour
{
    public enum TransitionType
    {
        SameScene, DifferentScene
    }

    [Header("Transition Info")]
    public string sceneName;
    public TransitionType transitionType;
    public TransitionDestination.DestinationTag destinationTag;
    public bool canTrans;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T) && canTrans)
        {
            Debug.Log("Starting teleportation£¡");
            SceneController.Instance.TransitionToDestination(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        UIManager.Instance.ShowTipUI("Press'  t  'to start teleportation!");
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Tag.PLAYER))
        {
            canTrans = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tag.PLAYER))
        {
            canTrans = false;
        }
    }
}
