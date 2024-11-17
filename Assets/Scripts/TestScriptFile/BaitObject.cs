using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitObject : InteractableObject
{

    protected override void Interact()
    {
        Destroy(this.gameObject);
        baitNum += 1;
        Debug.Log(baitNum);
    }
}
