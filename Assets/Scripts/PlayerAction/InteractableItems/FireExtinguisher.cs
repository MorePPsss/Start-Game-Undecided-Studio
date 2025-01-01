using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : InteractableObject
{
    public FireManager fireManager;
    public int fireAreaIndex;
    public bool haveInteractedFlag = false;
    protected override void Interact()
    {
        if (!haveInteractedFlag)
        {
            fireManager.ActivateExtinguisher(fireAreaIndex);
        }
    }
}
