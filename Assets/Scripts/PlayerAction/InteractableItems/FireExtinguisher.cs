using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : InteractableObject
{
    public FireManager fireManager;
    public int fireAreaIndex;
    public bool haveInteractedFlag = false;
    public bool isFakeValve = false;
    protected override void Interact()
    {
        if (!isFakeValve)
        {
            if (!haveInteractedFlag)
            {
                fireManager.ActivateExtinguisher(fireAreaIndex);
            }
        }else
        {
            UIManager.Instance.ShowTipUI("Pay attention to observing that the color of the fire extinguisher corresponds to the valve");
        }

    }
}
